using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Mvc;
using yarn_rider.Models;

namespace yarn_rider.Controllers
{
   
    public class MovieController : Controller
    {
        SiteDbContext db = new SiteDbContext();

        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        [HttpGet]
        public ActionResult Index(string searchString, string searchByGenre, string searchByModifier,
            string searchByYear)
        {
            // no value to search after (display all movies)
            if (String.IsNullOrEmpty(searchString) && String.IsNullOrEmpty(searchByGenre) &&
                String.IsNullOrEmpty(searchByYear) && String.IsNullOrEmpty(searchByModifier))
            {
                return View(db.Movies.ToList());
            }

            // search for only specified value combinations (Keyword, Genre, Name, Year) 
            var movies = db.Movies.Where(m =>
                (m.Genre.ToString().Equals(searchByGenre) || String.IsNullOrEmpty(searchByGenre)) &&
                (m.MovieName.Contains(searchString) || String.IsNullOrEmpty(searchString)) &&
                (m.Date.Equals(searchByYear) || String.IsNullOrEmpty(searchByYear)));

            // search with a specified modifier (with or without combination with other values)
            switch (searchByModifier)
            {
                case "RatingAscending":
                    movies = movies.OrderBy(s => s.Rate.ToString());
                    break;

                case "RatingDescending":
                    movies = movies.OrderByDescending(s => s.Rate.ToString());
                    break;

                case "TitleAscending":
                    movies = movies.OrderBy(s => s.MovieName.ToString());
                    break;

                case "TitleDescending":
                    movies = movies.OrderByDescending(s => s.MovieName.ToString());
                    break;
            }

            return View(movies.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID, MovieName, Date, Rate, PosterURL, MovieURL, Genre")]
            Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                ViewBag.Error = "Error ):";
                return RedirectToAction("Index");
            }

            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                ViewBag.Error = "No such page found, sorry ):";
                ViewData["error"] = "no such page found - sorry!";
                Session["error"] = "no such page found - sorry!";
                return RedirectToAction("Index");
            }

            ViewBag.Message = movie.MovieName;

            return View(movie);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Error ):";
                return RedirectToAction("Index");
            }

            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
//                return HttpNotFound();
                ViewBag.Error = "No such page found, sorry ):";
                ViewData["error"] = "no such page found - sorry!";
                Session["error"] = "no such page found - sorry!";
                return RedirectToAction("Index");
            }

            ViewBag.Message = movie.MovieName;

            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            List<Review> reviews = new List<Review>(movie.Reviews.ToList());
            foreach (Review review in reviews)
            {
                db.Reviews.Remove(review);
            }
            
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}