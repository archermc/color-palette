using System.Collections.Generic;
using System.Linq;

namespace ColorPalette.Objects.DTOs
{
    public class PictureDTO
    {
        public int  Id { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public SwathDTO[] ColorSwaths { get; set; }

        /// <summary>
        /// A method for transforming the SwathDTOs to a comma separated string in order to store it in DB
        /// </summary>
        /// <returns>Comma delimited string representing </returns>
        public string GetColorSwathsAsString()
        {
            return string.Join(
                ",", ColorSwaths.Select(swath => string.Join(",", swath.Explode()))
                .ToArray());
        }

    }
}
