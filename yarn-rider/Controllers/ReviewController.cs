using System;
using System.Collections;
using System.Collections.Generic;
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

        [HttpGet]
        public ActionResult Index(string searchMovieString, string searchByRate, string searchReviewString)
        {
            // no value to search after (display all reviews)
            if (String.IsNullOrEmpty(searchMovieString) && String.IsNullOrEmpty(searchByRate) &&
                String.IsNullOrEmpty(searchReviewString))
            {
                return View(db.Reviews.ToList());
            }

            // search for only specified value combinations (Keyword, Genre, Name, Year) 
            var reviews = db.Reviews.Where(m =>
                (m.Rating.ToString().Equals(searchByRate) || String.IsNullOrEmpty(searchByRate)) &&
                (m.Movie.MovieName.Contains(searchMovieString) || String.IsNullOrEmpty(searchMovieString) ||
                 m.Movie.MovieName.Equals("By Movie Title")) &&
                (m.Title.Contains(searchReviewString) || String.IsNullOrEmpty(searchReviewString) ||
                 m.Title.Equals("By Review Title")));

            return View(reviews.ToList());
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
        public ActionResult Create(int movieId, Review sendedReview)
        {
            if (ModelState.IsValid)
            {
                User currentUser = (User) Session["currentUser"];
                User user = db.Users.Find(currentUser.UserID);
                Movie movie = db.Movies.Find(movieId);

                db.Reviews.Add(sendedReview);

                db.SaveChanges();

                db.Reviews.Find(sendedReview.ReviewID).User = user;
                db.Reviews.Find(sendedReview.ReviewID).Movie = movie;

                db.Users.Find(user.UserID).Reviews.Add(sendedReview);
                db.Movies.Find(movieId).Reviews.Add(sendedReview);
                
                db.SaveChanges();

                // calculating the avg of all review scores 
                var Count = 0;
                for (var i = 0; i < movie.Reviews.Count; i++)
                {
                    Count += movie.Reviews[i].Rating;
                }

                movie.Rate = Count / movie.Reviews.Count;
                db.SaveChanges();

                return RedirectToAction("Details/" + movieId, "Movie");
            }

            return View(sendedReview);

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
//            User user = (User)Session["currentUser"];
//            Movie movie = review.Movie;

//            db.Users.Find(user).Reviews.Remove(review);
//            db.Movies.Find(movie).Reviews.Remove(review);
            db.Reviews.Remove(review);

            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}