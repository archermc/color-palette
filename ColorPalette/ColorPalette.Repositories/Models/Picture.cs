using System.ComponentModel.DataAnnotations;

namespace ColorPalette.Repositories.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Contents { get; set; }
        public string ColorSwatches { get; set; }
    }
}
