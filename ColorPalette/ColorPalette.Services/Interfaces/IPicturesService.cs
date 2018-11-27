using ColorPalette.Objects;
using ColorPalette.Objects.Utility;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorPalette.Services.Interfaces
{
    public interface IPicturesService
    {
        Task<Operation<IEnumerable<PictureDto>>> GetAllPictures();
        Task<Operation<PictureDto>> GetPicture(int id);
        Task<Operation<PictureDto>> AddPicture(PictureDto pictureDto);
        Task<Operation> DeletePicture(int id);
    }
}
