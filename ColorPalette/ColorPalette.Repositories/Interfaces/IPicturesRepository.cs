using ColorPalette.Repositories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IPicturesRepository
    {
        Task<List<Picture>> GetAllAsync();
        Task<Picture> GetAsync(int id);
        Task<Picture> AddAsync(Picture picture);
        Task<bool> DeleteAsync(int id);
    }
}
