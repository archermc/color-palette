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
                ",", ColorSwatches.Select(swatch => string.Join(",", swatch.Explode()))
                    .ToArray());
        }
    }
}
