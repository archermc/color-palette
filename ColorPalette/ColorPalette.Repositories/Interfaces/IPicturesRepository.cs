using ColorPalette.Repositories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColorPalette.Objects.Utility;

namespace ColorPalette.Repositories.Interfaces
{
    public interface IPicturesRepository
    {
        Task<Operation<IEnumerable<Picture>>> GetAllAsync();
        Task<Operation<Picture>> GetAsync(int id);
        Task<Operation<Picture>> AddAsync(Picture picture);
        Task<Operation> DeleteAsync(int id);
    }
}
