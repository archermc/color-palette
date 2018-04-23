using ColorPalette.Managers.Interfaces;
using ColorPalette.Objects.DTOs;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ColorPalette.Controllers
{
    public class PicturesController : ApiController
    {
        private readonly IPicturesManager _picturesManager;

        public PicturesController(IPicturesManager picturesManager)
        {
            _picturesManager = picturesManager;
        }

        // GET: api/Pictures
        [ResponseType(typeof(List<PictureDTO>))]
        [HttpGet, Route("api/pictures")]
        public async Task<IHttpActionResult> GetPictures()
        {
            var allPictures = await _picturesManager.GetAllPictures();

            return Ok(allPictures);
        }

        // GET: api/Pictures/5
        [ResponseType(typeof(PictureDTO))]
        public async Task<IHttpActionResult> GetPicture(int id)
        {
            var picture = await _picturesManager.GetPicture(id);

            if (picture == null)
                return NotFound();

            return Ok(picture);
        }

        // POST: api/Pictures
        [ResponseType(typeof(SwatchDTO[]))]
        [HttpPost, Route("api/pictures")]
        public async Task<SwatchDTO[]> PostPicture()
        {
            var file = HttpContext.Current.Request.Files.Count == 1 ?
                    HttpContext.Current.Request.Files[0] : null;

            if (file == null || file.ContentLength == 0)
                return null; //BadRequest();

            var fileName = Path.GetFileName(file.FileName);
            byte[] fileContents;

            using (var binaryReader = new BinaryReader(file.InputStream))
                fileContents = binaryReader.ReadBytes(file.ContentLength);

            var pictureDto = new PictureDTO
            {
                FileName = fileName,
                Contents = fileContents
            };

            var completedEntry = await _picturesManager.AddPicture(pictureDto);

            return completedEntry.ColorSwatches;
            //return Created($"api/Pictures/{completedEntry.Id}", completedEntry.ColorSwatches);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(PictureDTO))]
        public async Task<IHttpActionResult> DeletePicture(int id)
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