using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace ColorPalette.Objects
{
    [DebuggerDisplay("Hue = {" + nameof(Hue) + "}; Saturation = {" + nameof(Saturation) + "}; Value = {" + nameof(Value) + "}")]
    public class Hsv
    {
        private const int SCALE = 255;

        public float Hue { get; set; }
        public float Saturation { get; set; }
        public float Value { get; set; }

        public Hsv(float hue, float saturation, float value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }

        /// <summary>
        /// Constructor of HSV that takes in an RGB Color value and converts it to HSV
        /// </summary>
        /// <param name="pixel">Color object containing an RGB value to convert to HSV</param>
        public Hsv(Color pixel)
        {
            // find the percent value of each color
            var red = (float)pixel.R / SCALE;
            var green = (float)pixel.G / SCALE;
            var blue = (float)pixel.B / SCALE;

            // find the minimum and maximum of the percentage values
            var max = Math.Max(red, Math.Max(green, blue));
            var min = Math.Min(red, Math.Min(green, blue));

            // get the change between the min and the max
            var delta = max - min;

            // get ready to set the raw hue before it is scaled
            float hue = 0;

            // operate differently on hue depending on which color is the max
            if (delta == 0)
            {
                Hue = 0;
                Saturation = 0;
                Value = min;
                return;
            }

            if (max == red)
                hue = (green - blue) / delta % 6 * 60;
            else if (max == green)
                hue = ((blue - red) / delta % 6 + 2) * 60;
            else if (max == blue)
                hue = ((red - green) / delta % 6 + 4) * 60;

            // if the angle for hue goes negative, we can just adjust so that it's positive so that the sorting works better
            if (hue < 0)
                hue += 360;

            // get the maximum color as value
            var value = max;

            // find the saturation, which is 0 if value is 0 else it's delta - value which we then scale
            var saturation = value == 0 ? 0 : delta / value;

            // construct our lovely HSV object
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }

        // well I just realized that you can render colors in html with HSB not HSV
        // we'll try it with converting it back to RGB and displaying first
        //
        // derived from: https://www.rapidtables.com/convert/color/hsv-to-rgb.html
        /// <summary>
        /// Converts the HSV values contained within to RGB values in int[] form
        /// </summary>
        /// <returns>int[] representing RGB values correspondent to this HSV object's values</returns>
        public int[] ToRGB()
        {
            // some voodoo regarding calculated values that we'll used based on the degree that
            // Hue is located
            var C = Value * Saturation;
            var X = C * (1 - Math.Abs(Hue / 60 % 2 - 1));
            var m = Value - C;

            // prepare the initial unscaled values
            List<float> rgbPrime = new List<float>{0f,0f,0f};
            
            if (0 <= Hue && Hue < 60)
                rgbPrime = new List<float>{ C, X, 0};
            if (60 <= Hue && Hue < 120)
                rgbPrime = new List<float> { X, C, 0 };
            if (120 <= Hue && Hue < 180)
                rgbPrime = new List<float> { 0, C, X };
            if (180 <= Hue && Hue < 240)
                rgbPrime = new List<float> { 0, X, C };
            if (240 <= Hue && Hue < 300)
                rgbPrime = new List<float> { X, 0, C };
            if (300 <= Hue && Hue < 360)
                rgbPrime = new List<float> { C, 0, X };

            var RGB = rgbPrime.Select(rgb => (int)((rgb + m) * SCALE)).ToArray();

            return RGB;
        }
    }
}
