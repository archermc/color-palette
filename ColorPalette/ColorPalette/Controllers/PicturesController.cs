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
        public async Task<List<PictureDTO>> GetPictures()
        {
            return await _picturesManager.GetAllPictures();
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
        [ResponseType(typeof(PictureDTO))]
        [HttpPost, Route("api/pictures")]
        public async Task<IHttpActionResult> PostPicture()
        {
            var file = HttpContext.Current.Request.Files.Count == 1 ?
                    HttpContext.Current.Request.Files[0] : null;

            if (file == null || file.ContentLength == 0)
                return BadRequest();

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

            return Created($"api/Pictures/{completedEntry.Id}", completedEntry);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(PictureDTO))]
        public async Task<IHttpActionResult> DeletePicture(int id)
        {
            PictureDTO pictureDto = null; // manager call
            if (pictureDto == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}