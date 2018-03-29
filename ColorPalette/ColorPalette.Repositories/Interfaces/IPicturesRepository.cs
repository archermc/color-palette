using System.Collections.Generic;
using System.Threading.Tasks;
using ColorPalette.Objects.DTOs;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IPicturesRepository
    {
        Task<List<PictureDTO>> GetAllAsync();
        Task<PictureDTO> AddContentsAsync(byte[] contents);
        Task<bool> AddMetadataAsync(PictureDTO pictureDto);
        Task<bool> DeleteAsync(int id);
    }
}
