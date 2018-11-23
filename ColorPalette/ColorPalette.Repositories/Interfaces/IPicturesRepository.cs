using ColorPalette.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IPicturesRepository
    {
        Task<List<PictureDto>> GetAllAsync();
        Task<PictureDto> GetAsync(int id);
        Task<PictureDto> AddAsync(PictureDto picture);
        Task<bool> DeleteAsync(int id);
    }
}
