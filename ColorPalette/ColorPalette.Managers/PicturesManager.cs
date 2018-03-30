using ColorPalette.Managers.Interfaces;
using ColorPalette.Objects.DTOs;
using ColorPalette.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
            var bmpData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            //var hsvEntries = colorPalette.Entries.Select(ConvertRGBToHSV).ToList();
            //var sortedEntries = SortAndSeparateHsvValues(hsvEntries);

            return new List<int[]>();
        }

        private Hsv ConvertRGBToHSV(Color pixel)
        {
            // find the percent value of each color
            var red = pixel.R / SCALE;
            var green = pixel.G / SCALE;
            var blue = pixel.B / SCALE;

            // find the minimum and maximum of the percentage values
            var max = Math.Max(red, Math.Max(green, blue));
            var min = Math.Min(red, Math.Min(green, blue));

            // get the change between the min and the max
            var delta = max - min;

            // get ready to set the raw hue before it is scaled
            var rawHue = 0;

            // operate differently on hue depending on which color is the max
            if (max == red)
                rawHue = (green - blue) / delta;
            else if (max == green)
                rawHue = ((blue - red) / delta) + 2;
            else if (max == blue)
                rawHue = ((red - green) / delta) + 4;

            // scale hue
            var hue = rawHue * SCALE;

            // scale the maximum color to get value
            var value = max * SCALE;

            // find the saturation, which is 0 if value is 0 else it's delta - value which we then scale
            var saturation = value == 0 ? 0 :
                (delta / value) * SCALE;

            // return our lovely HSV object
            return new Hsv { Hue = hue, Saturation = saturation, Value = value };
        }

        private List<List<Hsv>> SortAndSeparateHsvValues(List<Hsv> values)
        {
            if (values == null || values.Count == 1)
                return null;

            // sort HSV values by Hue first off
             values.Sort((a,b) => a.Hue.CompareTo(b.Hue));

            return new List<List<Hsv>>();
        }

        // make a kiddie shit POCO to hold our values
        private class Hsv
        {
            public int Hue { get; set; }
            public int Saturation { get; set; }
            public int Value { get; set; }
        }

        #endregion
    }
}