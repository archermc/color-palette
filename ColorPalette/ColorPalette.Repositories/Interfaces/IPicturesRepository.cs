using System.Collections.Generic;
using System.Threading.Tasks;
using ColorPalette.Objects.DTOs;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IPicturesRepository
    {
        Task<List<PictureDTO>> GetAllAsync();
        Task<PictureDTO> GetAsync(int id);
        Task<PictureDTO> AddAsync(PictureDTO picture);
        Task<bool> DeleteAsync(int id);
    }
}
