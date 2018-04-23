namespace ColorPalette.Objects.DTOs
{
    public class SwatchDTO
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public SwatchDTO() { }

        public SwatchDTO(int[] rgb)
        {
            if (rgb.Length != 3)
                throw new System.Exception("INPUT ARRAY IS TOO DAMN LONG 3 INTS ONLY");
            R = rgb[0];
            G = rgb[1];
            B = rgb[2];
        }

        /// <summary>
        /// Emits string[] representation of Swatch value as [R, G, B]
        /// </summary>
        /// <returns>string[] in form of [R, G, B]</returns>
        public string[] Explode()
        {
            return new string[] { R.ToString(), G.ToString(), B.ToString() };
        }
    }
}
