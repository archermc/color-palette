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

        public string GetColorSwathsAsString()
        {
            return string.Join(
                ",", ColorSwaths.Select(swath => string.Join(",", swath.Explode()))
                .ToArray());
        }

    }
}
