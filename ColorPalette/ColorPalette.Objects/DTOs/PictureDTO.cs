using System.Collections.Generic;
using System.Linq;

namespace ColorPalette.Objects.DTOs
{
    public class PictureDTO
    {
        public int  Id { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public List<int[]> ColorSwaths { get; set; }

        public string ColorSwathsAsString
        {
            get
            {
                return string.Join(
                    ",", ColorSwaths.Select(
                        s => string.Join(
                            ",", s.Select(
                                c => c.ToString()
                            ).ToArray()
                        )
                    ).ToArray()
                );
            }
        }

    }
}
