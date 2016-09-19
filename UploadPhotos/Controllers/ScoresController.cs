using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using UploadPhotos.Models;

namespace UploadPhotos.Controllers
{
    public class ScoresController : ODataController
    {
        private MyPhotoModel db = new MyPhotoModel();

        public IHttpActionResult Get()
        {
            return Ok(db.Scores);
        }

        public IHttpActionResult Get([FromODataUri]int key)
        {
            var score = db.Scores.FirstOrDefault(p => p.Id == key);

            if(score == null)
            {
                return NotFound();
            }
            return Ok(score);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}