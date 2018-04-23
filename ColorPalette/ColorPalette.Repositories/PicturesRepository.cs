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

        /// <summary>
        /// Gets every picture in the database
        /// </summary>
        /// <returns>List of Picture DTOs that represent every entry in the database</returns>
        public async Task<List<PictureDTO>> GetAllAsync()
        {
            var pictures = await _dbContext.Pictures.ToListAsync();

            return pictures.Select(p => new PictureDTO
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
        /// <returns>Picture DTO representing picture object in the database</returns>
        public async Task<PictureDTO> GetAsync(int id)
        {
            var picture = await _dbContext.Pictures.SingleOrDefaultAsync(p => p.Id == id);

            if (picture == null)
                return null;

            return new PictureDTO
            {
                Id = picture.Id,
                FileName = picture.FileName,
                ColorSwatches = FormatColorSwatches(picture.ColorSwatches),
                Contents = picture.Contents
            };
        }

        /// <summary>
        /// Adds a picture object from the database and creates a new PictureDTO
        /// </summary>
        /// <param name="picture">PictureDTO that represents the picture to add to the database</param>
        /// <returns>PictureDTO represeting the object created in the database</returns>
        public async Task<PictureDTO> AddAsync(PictureDTO picture)
        {
            var newPicture = _dbContext.Pictures.Add(new Picture
            {
                FileName = picture.FileName,
                Contents = picture.Contents,
                ColorSwatches = picture.GetColorSwatchesAsString()
            });

            await _dbContext.SaveChangesAsync();

            return new PictureDTO
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
            var picture = new Picture {Id = id};

            _dbContext.Pictures.Attach(picture);
            _dbContext.Pictures.Remove(picture);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        #region Helper Methods

        private SwatchDTO[] FormatColorSwatches(string rawInput)
        {
            if (rawInput.IsNullOrEmpty())
                return null;

            var pixelList = rawInput.Split(',');
            var toReturn = new List<SwatchDTO>();

            for (int i = 0; i < pixelList.Length; i += 3)
            {
                toReturn.Add(new SwatchDTO
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
        //    picture.ColorSwatches = pictureDto.ColorSwatches.ToString();
        //    picture.Contents = pictureDto.Contents;

        //    return picture;
        //}

        #endregion
    }
}
