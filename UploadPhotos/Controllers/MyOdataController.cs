using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using System.Web.Security;
using UploadPhotos.Models;

namespace UploadPhotos.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using UploadPhotos.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Emotion>("MyOdata");
    builder.EntitySet<Facerectangle>("Facerectangles"); 
    builder.EntitySet<Score>("Scores"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    //[Authorize]
    public class MyOdataController: ODataController
    {  
        private MyPhotoModel db = new MyPhotoModel();
        
        // GET: odata/MyOdata
        [EnableQuery]
        public IQueryable<Emotion> GetMyOdata()
        {
            //var user = Membership.GetUser().UserName;
            //var user2 = HttpContext.Current.User.Identity.Name;

            //var user3 = User.Identity.Name;
            return db.Emotions;
        }

        // GET: odata/MyOdata(5)
        [EnableQuery]
        public SingleResult<Emotion> GetEmotion([FromODataUri] int key)
        {
            return SingleResult.Create(db.Emotions.Where(emotion => emotion.Id == key));
        }

       
        // GET: odata/MyOdata(5)/Facerectangle
        [EnableQuery]
        public SingleResult<Facerectangle> GetFacerectangle([FromODataUri] int key)
        {
            return SingleResult.Create(db.Emotions.Where(m => m.Id == key).Select(m => m.Facerectangle));
        }

        // GET: odata/MyOdata(5)/Score
        [EnableQuery]
        public SingleResult<Score> GetScore([FromODataUri] int key)
        {
            return SingleResult.Create(db.Emotions.Where(m => m.Id == key).Select(m => m.Scores));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmotionExists(int key)
        {
            return db.Emotions.Count(e => e.Id == key) > 0;
        }
    }

}
