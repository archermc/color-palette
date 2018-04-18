namespace ColorPalette.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// A short extension to call string.IsNullOrEmpty off of a string
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Boolean value representing whether a string is null or empty</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        //private static Picture UpdatePicture(this Picture picture, PictureDTO pictureDto)
        //{

        //}
    }
}
