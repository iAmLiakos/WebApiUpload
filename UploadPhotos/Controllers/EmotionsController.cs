using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UploadPhotos.Models;

namespace UploadPhotos.Controllers
{
    public class EmotionsController : Controller
    {
        private MyPhotoModel db = new MyPhotoModel();

        // GET: Emotions
        public ActionResult Index()
        {
            var emotion = db.Emotions.Include(e => e.Facerectangle).Include(e => e.Scores);
            //var statistics = db.Scores.SqlQuery("SELECT * FROM Scores");
            return View(emotion.ToList());
        }

        // GET: Emotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var emotion = db.Emotions.Find(id);          
            if (emotion == null)
            {
                return HttpNotFound();
            }
            return View(emotion);
        }

        // GET: Emotions/Create
        public ActionResult Create()
        {
            ViewBag.FaceID = new SelectList(db.Facerectangles, "faceID", "faceID");
            ViewBag.ScoreID = new SelectList(db.Scores, "scoreID", "scoreID");
            
            return View();
        }

        // POST: Emotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmotionID,FaceID,ScoreID")] Emotion emotion)
        {
            if (ModelState.IsValid)
            {
                db.Emotions.Add(emotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FaceID = new SelectList(db.Facerectangles, "faceID", "faceID", emotion.FaceId);
            ViewBag.ScoreID = new SelectList(db.Scores, "scoreID", "scoreID", emotion.ScoreId);
            return View(emotion);
        }

        // GET: Emotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emotion emotion = db.Emotions.Find(id);
            if (emotion == null)
            {
                return HttpNotFound();
            }
            ViewBag.FaceID = new SelectList(db.Facerectangles, "faceID", "faceID", emotion.FaceId);
            ViewBag.ScoreID = new SelectList(db.Scores, "scoreID", "scoreID", emotion.ScoreId);
            return View(emotion);
        }

        // POST: Emotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmotionID,FaceID,ScoreID")] Emotion emotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FaceID = new SelectList(db.Facerectangles, "faceID", "faceID", emotion.FaceId);
            ViewBag.ScoreID = new SelectList(db.Scores, "scoreID", "scoreID", emotion.ScoreId);
            return View(emotion);
        }

        // GET: Emotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emotion emotion = db.Emotions.Find(id);
            if (emotion == null)
            {
                return HttpNotFound();
            }
            return View(emotion);
        }

        // POST: Emotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emotion emotion = db.Emotions.Find(id);
            db.Emotions.Remove(emotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AverageValues()
        {
            ViewBag.averageanger = db.Scores.Average(x => x.anger);
            ViewBag.averagecontempt = db.Scores.Average(x => x.contempt);
            ViewBag.averagedisgust = db.Scores.Average(x => x.disgust);
            ViewBag.averagefear = db.Scores.Average(x => x.fear);
            ViewBag.averagehappiness = db.Scores.Average(x => x.happiness);
            ViewBag.averageneutral = db.Scores.Average(x => x.neutral);
            ViewBag.averagesadness = db.Scores.Average(x => x.sadness);
            ViewBag.averagesurprise = db.Scores.Average(x => x.surprise);
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
