using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using yarn_rider.Models;

namespace yarn_rider.Controllers
{
    public class ReviewController : Controller
    {
        SiteDbContext db = new SiteDbContext();

        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Review review = db.Reviews.Find(id);
            if (review == null) return HttpNotFound();

            ViewBag.Message = review.Title;

            return View(review);
        }

        [HttpPost]
        public ActionResult Edit(Review review)
        {
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int userId, int movieId, Review review)
        {
            if (ModelState.IsValid)
            {
                Movie movie = db.Movies.Find(movieId);
                User user = db.Users.Find(userId);

                movie.Reviews.Add(review);
                db.Reviews.Add(review);

                db.SaveChanges();

                db.Reviews.Find(review.ReviewID).User = user;
                db.Reviews.Find(review.ReviewID).Movie = movie;
                
                // calculating the avg of all review scores 
                var Count = 0;
                for(var i=0;i< movie.Reviews.Count; i++)
                {
                    Count += movie.Reviews[i].Rating;
                }
                
                movie.Rate = Count/movie.Reviews.Count;
                db.SaveChanges();
                
                return RedirectToAction("Details/"+movieId.ToString(),"Movie");
            }

            return View(review);

//            User user=db.Users.Find(userId);
//            Movie movie = db.Movies.Find(movieId);
//            db.Reviews.Add(review);
//            db.Reviews.Find(review).User = user;
//            db.Reviews.Find(review).Movie = movie;
//            
//            db.SaveChanges();
//            return RedirectToAction("Index", "Movie");
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

//        public PartialViewResult Delete(int? id)
//        {
//            Review review = db.Reviews.Find(id);
//            return PartialView(review);
//        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}