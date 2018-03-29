using System.Collections.Generic;

namespace ColorPalette.Objects.DTOs
{
    public class PictureDTO
    {
        public int  Id { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public List<int[]> ColorSwaths { get; set; }
    }
}
