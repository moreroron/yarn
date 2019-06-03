using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using yarn_rider.Models;

namespace yarn_rider.Controllers
{
    public class ReviewController : Controller
    {
        SiteDbContext db = new SiteDbContext();

        // GET
        public ActionResult Index(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = db.Movies.Find(id);
            if (movie == null) return HttpNotFound();

            ViewBag.Message = movie.MovieName;

            return View(movie.Reviews.ToList());
        }

//        public ActionResult Create(int movieKey, int userKey)
//        {
//            Review review = new Review();
//            review.User.UserID = userKey;
//            review.Movie.MovieID = movieKey;
//            return View(review);
//        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            db.Reviews.Add(review);
            db.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
        
    }
}