using ColorPalette.Repositories.Models;
using System.Collections.Generic;
using System.Linq;

namespace ColorPalette.Objects
{
    public class PictureDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public SwatchDto[] ColorSwatches { get; set; }

        /// <summary>
        /// A method for transforming the SwatchDtos to a comma separated string in order to store it in DB
        /// </summary>
        /// <returns>Comma delimited string representing </returns>
        public string GetColorSwatchesAsString()
        {
            return string.Join(
                ",", ColorSwatches
                    .Select(swatch => string.Join(",", swatch.Explode()))
                    .ToArray());
        }

        public PictureDto() { }

        public PictureDto(Picture p)
        {
            Id = p.Id;
            FileName = p.FileName;
            Contents = p.Contents;
            ColorSwatches = FormatColorSwatches(p.ColorSwatches);
        }

        private static SwatchDto[] FormatColorSwatches(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
                return null;

            var pixelList = rawInput.Split(',');
            var toReturn = new List<SwatchDto>();

            for (var i = 0; i < pixelList.Length; i += 3)
            {
                toReturn.Add(new SwatchDto
                {
                    R = int.Parse(pixelList[i]),
                    G = int.Parse(pixelList[i + 1]),
                    B = int.Parse(pixelList[i + 2])
                });
            }

            return toReturn.Count > 0 ? toReturn.ToArray() : null;
        }
    }
}
