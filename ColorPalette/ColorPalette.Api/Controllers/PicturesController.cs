using System;
using ColorPalette.Objects;
using ColorPalette.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ColorPalette.Api.Controllers
{
    public class PicturesController : ControllerBase
    {
        private readonly IPicturesService _picturesService;

        public PicturesController(IPicturesService picturesService)
        {
            _picturesService = picturesService;
        }

        // GET: api/Pictures
        [HttpGet, Route("api/pictures")]
        public async Task<IActionResult> GetPictures()
        {
            var allPictures = await _picturesService.GetAllPictures();

            return Ok(allPictures);
        }

        // GET: api/Pictures/5
        public async Task<IActionResult> GetPicture(int id)
        {
            var picture = await _picturesService.GetPicture(id);

            if (picture == null)
                return NotFound();

            return Ok(picture);
        }

        // POST: api/Pictures
        [HttpPost, Route("api/pictures")]
        public async Task<IActionResult> PostPicture(IFormFile file)
        {
            try
            {
                if (file == null)
                    return BadRequest();

                var fileName = Path.GetFileName(file.FileName);
                byte[] fileContents;

                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                    fileContents = binaryReader.ReadBytes((int) file.Length);

                var pictureDto = new PictureDto
                {
                    FileName = fileName,
                    Contents = fileContents
                };

                var completedEntry = await _picturesService.AddPicture(pictureDto);

                return Ok(completedEntry.ColorSwatches);
                //return Created($"api/Pictures/{completedEntry.Id}", completedEntry.ColorSwatches);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        // DELETE: api/Pictures/5
        public async Task<IActionResult> DeletePicture(int id)
        {
            var result = await _picturesService.DeletePicture(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}