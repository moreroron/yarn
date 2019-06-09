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

        public ActionResult Index()
        {
            return View(db.Reviews.ToList());
        }

        // GET
//        public ActionResult Index(int? id)
//        {
//            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//
//            Movie movie = db.Movies.Find(id);
//            if (movie == null) return HttpNotFound();
//
//            ViewBag.Message = movie.MovieName;
//
//            return View(movie.Reviews.ToList());
//        }

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
                var Count = 0;
                for(var i=0;i< movie.Reviews.Count; i++)
                {
                    Count += movie.Reviews[i].Rating;
                }
                movie.Rate = Count/movie.Reviews.Count;
                db.SaveChanges();
                return RedirectToAction("Details/"+movieId.ToString(),"Movie");
            }

            return View(db.Reviews.ToList());

//            User user=db.Users.Find(userId);
//            Movie movie = db.Movies.Find(movieId);
//            db.Reviews.Add(review);
//            db.Reviews.Find(review).User = user;
//            db.Reviews.Find(review).Movie = movie;
//            
//            db.SaveChanges();
//            return RedirectToAction("Index", "Movie");
        }
    }
}