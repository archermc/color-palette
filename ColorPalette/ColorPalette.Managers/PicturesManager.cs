using System.Collections.Generic;
using System.Threading.Tasks;
using ColorPalette.Managers.Interfaces;
using ColorPalette.Objects.DTOs;
using ColorPalette.Repositories.Interfaces;

namespace ColorPalette.Managers
{
    public class PicturesManager : IPicturesManager
    {
        private readonly IPicturesRepository _picturesRepository;

        public PicturesManager(IPicturesRepository picturesRepository)
        {
            _picturesRepository = picturesRepository;
        }

        public async Task<List<PictureDTO>> GetAllPictures()
        {
            return await _picturesRepository.GetAllAsync();
        }

        public async Task<PictureDTO> AddPictureContents(byte[] contents)
        {
            return await _picturesRepository.AddContentsAsync(contents);
        }

        public async Task<bool> AddPictureMetadata(PictureDTO pictureDto)
        {
            return await _picturesRepository.AddMetadataAsync(pictureDto);
        }

        public async Task<bool> DeletePicture(int id)
        {
            return await _picturesRepository.DeleteAsync(id);
        }
    }
}