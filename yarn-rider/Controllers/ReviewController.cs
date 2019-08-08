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
        public ActionResult Index(string searchMovieString, string searchByRate, string searchReviewString, string searchUserString)
        {
            // no value to search after (display all reviews)
            if (String.IsNullOrEmpty(searchMovieString) && String.IsNullOrEmpty(searchByRate) &&
                String.IsNullOrEmpty(searchReviewString) && String.IsNullOrEmpty(searchUserString))
            {
                return View(db.Reviews.ToList());
            }

            // search for only specified value combinations (Keyword, Genre, Name, Year) 
            var reviews = db.Reviews.Where(m =>
                (m.Rating.ToString().Equals(searchByRate) || String.IsNullOrEmpty(searchByRate))
                &&
                (m.Movie.MovieName.Contains(searchMovieString) || String.IsNullOrEmpty(searchMovieString) ||
                 m.Movie.MovieName.Equals("By Movie Title"))
                &&
                (m.Title.Contains(searchReviewString) || String.IsNullOrEmpty(searchReviewString) ||
                 m.Title.Equals("By Review Title")) 
                &&
                (m.User.UserName.Contains(searchUserString) || String.IsNullOrEmpty(searchUserString) ||
                 m.User.UserName.Equals("By Username")));

            return View(reviews.ToList());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Review review = db.Reviews.Find(id);
            if (review == null) return HttpNotFound();

            ViewBag.Message = review.Title;

            return View(review);
        }

        [HttpPost]
        public ActionResult Edit(Review review, int MovieID)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(review).State = EntityState.Modified;
            db.SaveChanges();

            Movie movie = db.Movies.Find(MovieID);
            var avg = movie.Reviews.Average(r => r.Rating);
            movie.Rate = (int) avg;
            
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details/" + MovieID, "Movie");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int movieId, Review sendedReview)
        {

            db.Configuration.ValidateOnSaveEnabled = false;
            if (ModelState.IsValid)
            {
                User currentUser = (User) Session["currentUser"];
                User user = db.Users.Find(currentUser.UserID);
                Movie movie = db.Movies.Find(movieId);

                db.Reviews.Add(sendedReview);

                db.SaveChanges();

                db.Reviews.Find(sendedReview.ReviewID).User = user;
                db.Reviews.Find(sendedReview.ReviewID).Movie = movie;

               
                sendedReview.Movie.Rate = (int) db.Movies.Find(movieId).Reviews.Average(r => r.Rating);
                
                db.Users.Find(user.UserID).Reviews.Add(sendedReview);
                db.Movies.Find(movieId).Reviews.Add(sendedReview);
                
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details/" + movieId, "Movie");
            }

            return View(sendedReview);
            
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
            db.Configuration.ValidateOnSaveEnabled = false;
            Review review = db.Reviews.Find(id);
            
            Movie movie = review.Movie;
            db.Reviews.Remove(review);

            db.SaveChanges();
            
            int avgScore = 0;
            foreach (var m in movie.Reviews)
            {
                avgScore += m.Rating;
            }

            if (movie.Reviews.Count == 0)
            {
                avgScore = 0;
            }

            else
            {
                avgScore /= movie.Reviews.Count;
            }
            
            movie.Rate = avgScore;

            db.SaveChanges();
            
            return RedirectToAction("Details/" + movie.MovieID, "Movie");
        }
    }
}