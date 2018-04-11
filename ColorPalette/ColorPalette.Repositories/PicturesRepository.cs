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
        private readonly IColorPaletteContext _dbContext;

        public PicturesRepository(IColorPaletteContext context)
        {
            _dbContext = context;
        }

        public async Task<List<PictureDTO>> GetAllAsync()
        {
            var pictures = await _dbContext.Pictures.ToListAsync();

            return pictures.Select(p => new PictureDTO
            {
                Id = p.Id,
                FileName = p.FileName,
                Contents = p.Contents,
                ColorSwaths = FormatColorSwaths(p.ColorSwaths)
            }).ToList();
        }

        public async Task<PictureDTO> GetAsync(int id)
        {
            var picture = await _dbContext.Pictures.SingleOrDefaultAsync(p => p.Id == id);

            if (picture == null)
                return null;

            return new PictureDTO
            {
                Id = picture.Id,
                FileName = picture.FileName,
                ColorSwaths = FormatColorSwaths(picture.ColorSwaths),
                Contents = picture.Contents
            };
        }

        public async Task<PictureDTO> AddAsync(PictureDTO picture)
        {
            var newPicture = _dbContext.Pictures.Add(new Picture
            {
                FileName = picture.FileName,
                Contents = picture.Contents,
                ColorSwaths = picture.GetColorSwathsAsString()
            });

            await _dbContext.SaveChangesAsync();

            return new PictureDTO
            {
                Id = newPicture.Id,
                FileName = newPicture.FileName,
                Contents = newPicture.Contents,
                ColorSwaths = FormatColorSwaths(newPicture.ColorSwaths)
            };
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

        private SwathDTO[] FormatColorSwaths(string rawInput)
        {
            if (rawInput.IsNullOrEmpty())
                return null;

            var pixelList = rawInput.Split(',');
            var toReturn = new List<SwathDTO>();

            for (int i = 0; i < pixelList.Length; i += 3)
            {
                toReturn.Add(new SwathDTO
                {
                    R = int.Parse(pixelList[i]),
                    G = int.Parse(pixelList[i + 1]),
                    B = int.Parse(pixelList[i + 2])
                });
            }

            return toReturn.Count > 0 ? toReturn.ToArray() : null;
        }

        //private bool PictureExists(int id)
        //{
        //    return _dbContext.Pictures.Count(e => e.Id == id) > 0;
        //}

        // TODO: possibly refactor into extension method
        //private Picture UpdatePicture(Picture picture, PictureDTO pictureDto)
        //{
        //    picture.FileName = pictureDto.FileName;
        //    picture.ColorSwaths = pictureDto.ColorSwaths.ToString();
        //    picture.Contents = pictureDto.Contents;

        //    return picture;
        //}

        #endregion
    }
}
