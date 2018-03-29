using ColorPalette.Helpers;
using ColorPalette.Objects.DTOs;
using ColorPalette.Repositories.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ColorPalette.Repositories.Interfaces;

namespace ColorPalette.Repositories
{
    public class PicturesRepository : IPicturesRepository
    {
        private readonly ColorPaletteContext _dbContext;

        public PicturesRepository(ColorPaletteContext context)
        {
            _dbContext = context;
        }

        public async Task<List<PictureDTO>> GetAllAsync()
        {
            return _dbContext.Pictures.Select(p => new PictureDTO
            {
                Id = p.Id,
                FileName = p.FileName,
                Contents = p.Contents,
                ColorSwaths = FormatColorSwaths(p.ColorSwaths)
            }).ToList();
        }

        public async Task<PictureDTO> AddContentsAsync(byte[] contents)
        {
            var newPicture = _dbContext.Pictures.Add(new Picture
            {
                Contents = contents
            });

            await _dbContext.SaveChangesAsync();

            return new PictureDTO
            {
                Id = newPicture.Id,
                // do I need to pass back the contents?
                // Contents = newPicture.contents
            };
        }

        public async Task<bool> AddMetadataAsync(PictureDTO pictureDto)
        {
            var picture = await _dbContext.Pictures.FirstOrDefaultAsync(p => p.Id == pictureDto.Id);

            var updatedPicture = UpdatePicture(picture, pictureDto);
            _dbContext.Entry(picture).CurrentValues.SetValues(updatedPicture);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var picture = new Picture {Id = id};

            _dbContext.Pictures.Attach(picture);
            _dbContext.Pictures.Remove(picture);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        #region Helper Methods

        private List<int[]> FormatColorSwaths(string rawInput)
        {
            if (rawInput.IsNullOrEmpty())
                return null;

            return new List<int[]>();
        }

        //private bool PictureExists(int id)
        //{
        //    return _dbContext.Pictures.Count(e => e.Id == id) > 0;
        //}

        // TODO: possibly refactor into extension method
        private Picture UpdatePicture(Picture picture, PictureDTO pictureDto)
        {
            picture.FileName = pictureDto.FileName;
            picture.ColorSwaths = pictureDto.ColorSwaths.ToString();
            picture.Contents = pictureDto.Contents;

            return picture;
        }

        #endregion
    }
}
