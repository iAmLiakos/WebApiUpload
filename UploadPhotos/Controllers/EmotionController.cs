using System.Linq;
using System.Web.Mvc;
using UploadPhotos.Models;

namespace UploadPhotos.Controllers
{
    public class EmotionController : Controller
    {
        //Emotion Controller
        public ActionResult Index()
        {
            var emotionsdb = new PhotoContext();
            var hello = emotionsdb.Photos.ToList();

            return View(hello);
        }

    }
}
