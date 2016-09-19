using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using UploadPhotos.Models;

namespace UploadPhotos.Controllers
{
    
    public class FetchDataController : ApiController
    {
        private MyPhotoModel db = new MyPhotoModel();

        //public HttpResponseMessage GetScores()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var scores = db.Scores;

        //    return Request.CreateResponse(HttpStatusCode.OK, scores);
        //}

        //[ResponseType(typeof(Score))]
        //public HttpResponseMessage GetScores(int locationID)
        //{
        //    ///db.Configuration.ProxyCreationEnabled = false;
        //    using (var data = new MyPhotoModel())
        //    {
        //        var scores = data.Emotions.Where(e => e.locationID == locationID);
        //        return Request.CreateResponse(HttpStatusCode.OK,scores);
        //    }

        //}
        //GET: api/FetchData
        public IQueryable<Scores> GetEmotion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var scores = db.Scores;
            return scores;
        }

        // GET: api/FetchData/5
        [ResponseType(typeof(Emotion))]
        public IHttpActionResult GetEmotion(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Emotion emotion = db.Emotions.Find(id);
            if (emotion == null)
            {
                return NotFound();
            }

            return Ok(emotion);
        }


        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EmotionExists(int id)
        //{
        //    return db.Emotions.Count(e => e.EmotionID == id) > 0;
        //}
    }
}