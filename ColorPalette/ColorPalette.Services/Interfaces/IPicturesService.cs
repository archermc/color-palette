using ColorPalette.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorPalette.Services.Interfaces
{
    public interface IPicturesService
    {
        Task<List<PictureDto>> GetAllPictures();
        Task<PictureDto> GetPicture(int id);
        Task<PictureDto> AddPicture(PictureDto picture);
        Task<bool> DeletePicture(int id);
    }
}
