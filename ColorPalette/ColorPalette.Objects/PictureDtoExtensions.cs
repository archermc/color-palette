using ColorPalette.Repositories.Models;

namespace ColorPalette.Objects
{
    public static class PictureDtoExtensions
    {
        public static Picture ToPicture(this PictureDto dto)
        {
            return new Picture
            {
                Id = dto.Id,
                FileName = dto.FileName,
                Contents = dto.Contents,
                ColorSwatches = dto.GetColorSwatchesAsString()
            };
        }
    }
}
