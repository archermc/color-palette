using System;
using ColorPalette.Objects.Utility;
using ColorPalette.Repositories.Interfaces;
using ColorPalette.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<Operation<IEnumerable<Picture>>> GetAllAsync()
        {
            try
            {
                var pictures = await _dbContext.Pictures.ToListAsync();

                return Operation<IEnumerable<Picture>>.WithSuccess(pictures);
            }
            catch (Exception e)
            {
                return Operation<IEnumerable<Picture>>.WithException(e);
            }
        }

        /// <summary>
        /// Gets a specific picture from the database based on a unique identifier
        /// </summary>
        /// <param name="id">Unique identifier tied to the picture</param>
        /// <returns>Picture Dto representing picture object in the database</returns>
        public async Task<Operation<Picture>> GetAsync(int id)
        {
            try
            {
                var picture = await _dbContext.Pictures.SingleAsync(p => p.Id == id);

                return Operation<Picture>.WithSuccess(picture);
            }
            catch (Exception e)
            {
                return Operation<Picture>.WithException(e);
            }
        }

        /// <summary>
        /// Adds a picture object from the database and creates a new PictureDto
        /// </summary>
        /// <param name="picture">PictureDto that represents the picture to add to the database</param>
        /// <returns>PictureDto represeting the object created in the database</returns>
        public async Task<Operation<Picture>> AddAsync(Picture picture)
        {
            var newPicture = _dbContext.Pictures.Add(picture).Entity;

            await _dbContext.SaveChangesAsync();

            return Operation<Picture>.WithSuccess(newPicture);
        }

        /// <summary>
        /// Deletes a picture object from the database and returns the result in boolean form
        /// </summary>
        /// <param name="id">Unique identifier for the </param>
        /// <returns></returns>
        public async Task<Operation> DeleteAsync(int id)
        {
            var picture = new Picture { Id = id };

            _dbContext.Pictures.Attach(picture);
            _dbContext.Pictures.Remove(picture);

            await _dbContext.SaveChangesAsync();

            return Operation.WithSuccess();
        }
    }
}
