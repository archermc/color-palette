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

        public async Task<PictureDTO> GetPicture(int id)
        {
            return await _picturesRepository.GetAsync(id);
        }

        public async Task<PictureDTO> AddPicture(PictureDTO picture)
        {
            picture.ColorSwaths = GenerateColorSwaths(picture.Contents);

            return await _picturesRepository.AddAsync(picture);
        }

        public async Task<bool> DeletePicture(int id)
        {
            return await _picturesRepository.DeleteAsync(id);
        }

        #region Helper Methods

        private List<int[]> GenerateColorSwaths(byte[] contents)
        {
            return null;
        }

        #endregion
    }
}