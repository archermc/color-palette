using System.Collections.Generic;
using System.Linq;

namespace ColorPalette.Objects.DTOs
{
    public class PictureDTO
    {
        public int  Id { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public SwatchDTO[] ColorSwatches { get; set; }

        /// <summary>
        /// A method for transforming the SwatchDTOs to a comma separated string in order to store it in DB
        /// </summary>
        /// <returns>Comma delimited string representing </returns>
        public string GetColorSwatchesAsString()
        {
            return string.Join(
                ",", ColorSwatches.Select(swatch => string.Join(",", swatch.Explode()))
                .ToArray());
        }

    }
}
