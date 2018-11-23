using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ColorPalette.Objects;
using ColorPalette.Services.Interfaces;
using ColorPalette.Services.Models;

namespace ColorPalette.Api.Controllers
{
    public class PicturesController : ControllerBase
    {
        private readonly IPicturesService _picturesManager;

        public PicturesController(IPicturesService picturesManager)
        {
            _picturesManager = picturesManager;
        }

        // GET: api/Pictures
        [HttpGet, Route("api/pictures")]
        public async Task<IActionResult> GetPictures()
        {
            var allPictures = await _picturesManager.GetAllPictures();

            return Ok(allPictures);
        }

        // GET: api/Pictures/5
        public async Task<IActionResult> GetPicture(int id)
        {
            var picture = await _picturesManager.GetPicture(id);

            if (picture == null)
                return NotFound();

            return Ok(picture);
        }

        // POST: api/Pictures
        [HttpPost, Route("api/pictures")]
        public async Task<IActionResult> PostPicture(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            var fileName = Path.GetFileName(file.FileName);
            byte[] fileContents;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                fileContents = binaryReader.ReadBytes((int)file.Length);

            var pictureDto = new PictureDto
            {
                FileName = fileName,
                Contents = fileContents
            };

            var completedEntry = await _picturesManager.AddPicture(pictureDto);

            return Created($"api/Pictures/{completedEntry.Id}", completedEntry.ColorSwatches);
        }

        // DELETE: api/Pictures/5
        public async Task<IActionResult> DeletePicture(int id)
        {
            var result = await _picturesManager.DeletePicture(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}