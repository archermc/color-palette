using ColorPalette.Repositories.Interfaces;
using ColorPalette.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ColorPalette.Repositories.Implementations
{
    public class PicturesRepository : IPicturesRepository
    {
        private readonly ColorPaletteContext _dbContext;

        public PicturesRepository(ColorPaletteContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Gets every picture in the database
        /// </summary>
        /// <returns>List of Picture Dtos that represent every entry in the database</returns>
        public async Task<List<Picture>> GetAllAsync()
        {
            return await _dbContext.Pictures.ToListAsync();
        }

        /// <summary>
        /// Gets a specific picture from the database based on a unique identifier
        /// </summary>
        /// <param name="id">Unique identifier tied to the picture</param>
        /// <returns>Picture Dto representing picture object in the database</returns>
        public async Task<Picture> GetAsync(int id)
        {
            return await _dbContext.Pictures.SingleOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Adds a picture object from the database and creates a new PictureDto
        /// </summary>
        /// <param name="picture">PictureDto that represents the picture to add to the database</param>
        /// <returns>PictureDto represeting the object created in the database</returns>
        public async Task<Picture> AddAsync(Picture picture)
        {
            var newPicture = _dbContext.Pictures.Add(picture).Entity;

            await _dbContext.SaveChangesAsync();

            return newPicture;
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
    }
}
