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

        public async Task<List<PictureDTO>> GetAllPictures()
        {
            return await _picturesRepository.GetAllAsync();
        }

        public async Task<PictureDTO> GetPicture(int id)
        {
            return await _picturesRepository.GetAsync(id);
        }

        public async Task<PictureDTO> AddPicture(PictureDTO picture)
        {
            using (var image = (Bitmap)Image.FromStream(new MemoryStream(picture.Contents)))
                picture.ColorSwaths = GenerateColorSwaths(image);

            return await _picturesRepository.AddAsync(picture);
        }

        public async Task<bool> DeletePicture(int id)
        {
            return await _picturesRepository.DeleteAsync(id);
        }

        #region Helper Methods

        private List<int[]> GenerateColorSwaths(Bitmap image)
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
            var rgbSwaths = hsvSwaths.Select(hsv => hsv.ToRGB()).ToList();

            return rgbSwaths;
        }

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
                var sample = values.Skip(entriesPerSwath * i).Take(entriesPerSwath).ToList();

                //var medianHue = sample[entriesPerSwath / 2].Hue;
                //var normalizedSaturation = sample.Select(s => s.Saturation).Average();
                //var normalizedValue = sample.Select(s => s.Value).Average();

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