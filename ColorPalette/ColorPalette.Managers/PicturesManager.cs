using ColorPalette.Managers.Interfaces;
using ColorPalette.Objects;
using ColorPalette.Objects.DTOs;
using ColorPalette.Repositories.Interfaces;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ColorPalette.Managers
{
    public class PicturesManager : IPicturesManager
    {
        private readonly IPicturesRepository _picturesRepository;

        public PicturesManager(IPicturesRepository picturesRepository)
        {
            _picturesRepository = picturesRepository;
        }

        /// <summary>
        /// Returns all pictures in the DB
        /// </summary>
        /// <returns>All pictures in DB</returns>
        public async Task<List<PictureDTO>> GetAllPictures()
        {
            return await _picturesRepository.GetAllAsync();
        }

        /// <summary>
        /// Returns a specific picture in the DB by ID
        /// </summary>
        /// <param name="id">Unique identifier of photo to return</param>
        /// <returns>Picture DTO corresponding to identifier provided</returns>
        public async Task<PictureDTO> GetPicture(int id)
        {
            return await _picturesRepository.GetAsync(id);
        }

        /// <summary>
        /// Adds a picture to the DB, generating color swaths beforehand
        /// </summary>
        /// <param name="picture"></param>
        /// <returns>Picture DTO object representing the object created in DB</returns>
        public async Task<PictureDTO> AddPicture(PictureDTO picture)
        {
            // Read the image from stream, convert it to Bitmap, and generate the swaths with it
            // in one using simply so that both the image and MemoryStream will dispose after being used
            using (var image = (Bitmap)Image.FromStream(new MemoryStream(picture.Contents)))
                picture.ColorSwaths = GenerateColorSwaths(image);

            var result = await _picturesRepository.AddAsync(picture);

            return result;
        }

        /// <summary>
        /// Remove a picture from the DB based on the unique identifier
        /// </summary>
        /// <param name="id">Unique identifier of picture to delete</param>
        /// <returns>Boolean value representing success of deletion</returns>
        public async Task<bool> DeletePicture(int id)
        {
            return await _picturesRepository.DeleteAsync(id);
        }

        #region Helper Methods

        /// <summary>
        /// Generates a set of swathes based on an image passed in; default number of swathes is 7
        /// </summary>
        /// <param name="image">Bitmap of image that we want to generate swatches </param>
        /// <returns>Array of SwathDTOs (essentially int arrays of RGB values) representing 7 colors picked based on whatever algorithm we use</returns>
        private SwathDTO[] GenerateColorSwaths(Bitmap image)
        {
            // set up our variables: the pixel count and the area of the bitmap for easy reference
            const int PIXEL_COUNT = 3;
            BitmapData bmpData = null;
            var bmpArea = image.Width * image.Height;

            // create the array that will hold the rgb values as well as the one that will hold the hsv values
            var pixelBois = new byte[bmpArea * PIXEL_COUNT];
            var hsvValues = new Hsv[bmpArea];

            try
            {
                bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                    image.PixelFormat); // or PixelFormat.Format24bppRgb 

                // copy the bytes into a byte array the length of the image's dimensions * 3 (for each R G B value)
                Marshal.Copy(bmpData.Scan0, pixelBois, 0, pixelBois.Length);
            }
            finally
            {
                image.UnlockBits(bmpData);
            }

            for (int i = 0; i < pixelBois.Length; i += PIXEL_COUNT)
            {
                // create a color object with the RGB values reversed as the storage from the int* pointer goes BGR
                // Shout out to github.com/programmingthomas for helping resolve this problem
                var c = Color.FromArgb(pixelBois[i + 2], pixelBois[i + 1], pixelBois[i]);

                // convert it to HSV and store it in the corresponding HSV array (divided by 3 since each represents a whole pixel value)
                hsvValues[i / PIXEL_COUNT] = new Hsv(c);
            }

            var hsvSwaths = SortByHueAndFormatHsvValues(hsvValues.ToList());
            var rgbSwaths = hsvSwaths.Select(hsv => new SwathDTO(hsv.ToRGB())).ToArray();

            return rgbSwaths;
        }

        /// <summary>
        /// One algorithmic method of finding the colors that we return from our GenerateColorSwaths(Bitmap) method; sorts
        /// the hues of a picture, separates the array into 7 equal parts, and finds the median of each part to return
        /// </summary>
        /// <param name="values">Unsorted array of HSV values from a picture</param>
        /// <returns>List of representative pixels in HSV form from the picture</returns>
        private List<Hsv> SortByHueAndFormatHsvValues(List<Hsv> values)
        {
            const int NUMBER_OF_SWATHS = 7;
            List<Hsv> hsvSwaths = new List<Hsv>();

            if (values == null || values.Count == 1)
                return null;

            // sort HSV values by Hue first off
            values.Sort((a,b) => a.Hue.CompareTo(b.Hue));

            var entriesPerSwath = values.Count / NUMBER_OF_SWATHS;

            for (int i = 0; i < NUMBER_OF_SWATHS; i++)
            {
                // separate the entire value pool into individual parts
                var sample = values.Skip(entriesPerSwath * i).Take(entriesPerSwath).ToList();

                // find median of each value
                var medianHue = sample[entriesPerSwath / 2].Hue;
                var normalizedSaturation = sample[entriesPerSwath / 2].Saturation;
                var normalizedValue = sample[entriesPerSwath / 2].Value;

                hsvSwaths.Add(new Hsv(medianHue, normalizedSaturation, normalizedValue));
            }

            return hsvSwaths;
        }

        #endregion
    }
}