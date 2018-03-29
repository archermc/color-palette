using ColorPalette.Managers.Interfaces;
using ColorPalette.Objects.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            // manager call
            PictureDTO picture = null;
            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }

        //// PUT: api/Pictures/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutPicture(int id, Picture picture)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != picture.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(picture).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PictureExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Pictures
        [ResponseType(typeof(PictureDTO))]
        public async Task<IHttpActionResult> PostPictureContents(PictureDTO picture)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // manager call to post picture contents

            return CreatedAtRoute("DefaultApi", new { id = picture.Id }, picture);
        }

        // POST: api/Pictures/{id}
        [ResponseType(typeof(PictureDTO))]
        public async Task<IHttpActionResult> PostPictureMetadata(PictureDTO picture)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // manager call to post picture metadata

            return CreatedAtRoute("DefaultApi", new { id = picture.Id }, picture);
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