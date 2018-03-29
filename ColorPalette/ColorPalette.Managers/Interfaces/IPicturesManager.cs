using System.Collections.Generic;
using System.Threading.Tasks;
using ColorPalette.Objects.DTOs;

namespace ColorPalette.Managers.Interfaces
{
    public interface IPicturesManager
    {
        Task<List<PictureDTO>> GetAllPictures();
        Task<PictureDTO> GetPicture(int id);
        Task<PictureDTO> AddPictureContents(byte[] contents);
        Task<bool> AddPictureMetadata(PictureDTO pictureDto);
        Task<bool> DeletePicture(int id);
    }
}
