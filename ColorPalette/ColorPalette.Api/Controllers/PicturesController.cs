using ColorPalette.Objects;
using ColorPalette.Objects.Utility;
using ColorPalette.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ColorPalette.Api.Controllers
{
    public class PicturesController : EnhancedController
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
            var pictureOperation = await _picturesService.GetPicture(id);

            return !pictureOperation.DidSucceed 
                ? InternalError(pictureOperation.Exception) 
                : Ok(pictureOperation.Result);
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

                var operation = await _picturesService.AddPicture(pictureDto);

                return operation.DidSucceed 
                    ? Ok(operation.Result.ColorSwatches) 
                    : InternalError(operation.Exception);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        // DELETE: api/Pictures/5
        public async Task<IActionResult> DeletePicture(int id)
        {
            var operation = await _picturesService.DeletePicture(id);

            return operation.DidSucceed
                ? Ok()
                : InternalError(operation.Exception);
        }
    }
}