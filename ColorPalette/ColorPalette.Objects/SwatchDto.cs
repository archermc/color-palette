using System;

namespace ColorPalette.Objects
{
    public class SwatchDto
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public SwatchDto() { }

        public SwatchDto(int[] rgb)
        {
            if (rgb.Length != 3)
                throw new Exception("INPUT ARRAY IS TOO DAMN LONG 3 INTS ONLY");
            R = rgb[0];
            G = rgb[1];
            B = rgb[2];
        }

        public SwatchDto((int R, int G, int B) rgb)
        {
            R = rgb.R;
            G = rgb.G;
            B = rgb.B;
        }

        /// <summary>
        /// Emits string[] representation of Swatch value as [R, G, B]
        /// </summary>
        /// <returns>string[] in form of [R, G, B]</returns>
        public string[] Explode()
        {
            return new[] { R.ToString(), G.ToString(), B.ToString() };
        }
    }
}
