using ColorPalette.Objects;
using ColorPalette.Repositories.Interfaces;
using ColorPalette.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPalette.Repositories
{
    public class PicturesRepository : IPicturesRepository
    {
        private readonly IColorPaletteContext _dbContext;

        public PicturesRepository(IColorPaletteContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Gets every picture in the database
        /// </summary>
        /// <returns>List of Picture Dtos that represent every entry in the database</returns>
        public async Task<List<PictureDto>> GetAllAsync()
        {
            var pictures = await _dbContext.Pictures.ToListAsync();

            return pictures.Select(p => new PictureDto
            {
                Id = p.Id,
                FileName = p.FileName,
                Contents = p.Contents,
                ColorSwatches = FormatColorSwatches(p.ColorSwatches)
            }).ToList();
        }

        /// <summary>
        /// Gets a specific picture from the database based on a unique identifier
        /// </summary>
        /// <param name="id">Unique identifier tied to the picture</param>
        /// <returns>Picture Dto representing picture object in the database</returns>
        public async Task<PictureDto> GetAsync(int id)
        {
            var picture = await _dbContext.Pictures.SingleOrDefaultAsync(p => p.Id == id);

            if (picture == null)
                return null;

            return new PictureDto
            {
                Id = picture.Id,
                FileName = picture.FileName,
                ColorSwatches = FormatColorSwatches(picture.ColorSwatches),
                Contents = picture.Contents
            };
        }

        /// <summary>
        /// Adds a picture object from the database and creates a new PictureDto
        /// </summary>
        /// <param name="picture">PictureDto that represents the picture to add to the database</param>
        /// <returns>PictureDto represeting the object created in the database</returns>
        public async Task<PictureDto> AddAsync(PictureDto picture)
        {
            var newPicture = _dbContext.Pictures.Add(new Picture
            {
                FileName = picture.FileName,
                Contents = picture.Contents,
                ColorSwatches = picture.GetColorSwatchesAsString()
            }).Entity;

            await _dbContext.SaveChangesAsync();

            return new PictureDto
            {
                Id = newPicture.Id,
                FileName = newPicture.FileName,
                Contents = newPicture.Contents,
                ColorSwatches = FormatColorSwatches(newPicture.ColorSwatches)
            };
        }

        /// <summary>
        /// Deletes a picture object from the database and returns the result in boolean form
        /// </summary>
        /// <param name="id">Unique identifier for the </param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var picture = new Picture { Id = id };

            _dbContext.Pictures.Attach(picture);
            _dbContext.Pictures.Remove(picture);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        #region Helper Methods

        private SwatchDto[] FormatColorSwatches(string rawInput)
        {
            if (string.IsNullOrEmpty(rawInput))
                return null;

            var pixelList = rawInput.Split(',');
            var toReturn = new List<SwatchDto>();

            for (int i = 0; i < pixelList.Length; i += 3)
            {
                toReturn.Add(new SwatchDto
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
        //private Picture UpdatePicture(Picture picture, PictureDto pictureDto)
        //{
        //    picture.FileName = pictureDto.FileName;
        //    picture.ColorSwatches = pictureDto.ColorSwatches.ToString();
        //    picture.Contents = pictureDto.Contents;

        //    return picture;
        //}

        #endregion
    }
}
