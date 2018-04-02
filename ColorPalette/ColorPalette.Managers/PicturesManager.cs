using ColorPalette.Managers.Interfaces;
using ColorPalette.Objects.DTOs;
using ColorPalette.Repositories.Interfaces;
using System;
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
        private const int SCALE = 255;
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
            using (Bitmap image = (Bitmap)Image.FromStream(new MemoryStream(picture.Contents)))
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
            var bmpArea = image.Width * image.Height;

            // create the array that will hold the rgb values as well as the one that will hold the hsv values
            var pixelBois = new byte[bmpArea * PIXEL_COUNT];
            var hsvValues = new Hsv[bmpArea];

            var bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                image.PixelFormat); // or PixelFormat.Format24bppRgb 

            // copy the bytes into a byte array the length of the image's dimensions * 3 (for each R G B value)
            Marshal.Copy(bmpData.Scan0, pixelBois, 0, pixelBois.Length);

            for (int i = 0; i < bmpArea; i += PIXEL_COUNT)
            {
                // create a color object with the RGB values reversed as the storage from the int* pointer goes BGR
                // Shout out to github.com/programmingthomas for helping resolve this problem
                var c = Color.FromArgb(pixelBois[i + 2], pixelBois[i + 1], pixelBois[i]);

                // convert it to HSV and store it in the corresponding HSV array (divided by 3 since each represents a whole pixel value)
                hsvValues[i / PIXEL_COUNT] = ConvertRGBToHSV(c);
            }

            var sortedEntries = SortAndFormatHsvValues(hsvValues.ToList());

            return new List<int[]>();
        }

        private Hsv ConvertRGBToHSV(Color pixel)
        {
            // find the percent value of each color
            var red = (float)pixel.R / SCALE;
            var green = (float)pixel.G / SCALE;
            var blue = (float)pixel.B / SCALE;

            // find the minimum and maximum of the percentage values
            var max = Math.Max(red, Math.Max(green, blue));
            var min = Math.Min(red, Math.Min(green, blue));

            // get the change between the min and the max
            var delta = max - min;

            // get ready to set the raw hue before it is scaled
            float rawHue = 0;

            // operate differently on hue depending on which color is the max
            if (delta != 0)
            {
                if (max == red)
                    rawHue = (green - blue) / delta;
                else if (max == green)
                    rawHue = (blue - red) / delta + 2;
                else if (max == blue)
                    rawHue = (red - green) / delta + 4;
            }

            // scale hue
            var hue = rawHue * SCALE;

            // scale the maximum color to get value
            var value = max * SCALE;

            // find the saturation, which is 0 if value is 0 else it's delta - value which we then scale
            var saturation = value == 0 ? 0 :
                delta / value * SCALE;

            // return our lovely HSV object
            return new Hsv { Hue = hue, Saturation = saturation, Value = value };
        }

        private List<List<Hsv>> SortAndFormatHsvValues(List<Hsv> values)
        {
            const int NUMBER_OF_SWATHS = 7;
            List<List<Hsv>> sortedRawSwaths;

            if (values == null || values.Count == 1)
                return null;

            // sort HSV values by Hue first off
            values.Sort((a,b) => a.Hue.CompareTo(b.Hue));

            var entriesPerSwath = values.Count / NUMBER_OF_SWATHS;
            for (int i = 0; i < values.Count; i += entriesPerSwath)
            {
                var sample = values.Skip(entriesPerSwath * i).Take(entriesPerSwath);

                continue;
            }

            return new List<List<Hsv>>();
        }

        // make a kiddie shit POCO to hold our values
        private class Hsv
        {
            public float Hue { get; set; }
            public float Saturation { get; set; }
            public float Value { get; set; }
        }

        #endregion
    }
}