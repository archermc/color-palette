namespace ColorPalette.Objects.DTOs
{
    public class SwathDTO
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public SwathDTO() { }

        public SwathDTO(int[] rgb)
        {
            if (rgb.Length != 3)
                throw new System.Exception("Swaths helpo help help");
            R = rgb[0];
            G = rgb[1];
            B = rgb[2];
        }

        public string[] Explode()
        {
            return new string[] { R.ToString(), G.ToString(), B.ToString() };
        }
    }
}
