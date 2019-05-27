using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Search(string searchString)
        {
            var movies = from m in db.Movies 
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.MovieName.Contains(searchString));
            }
            else
            {
                return View(db.Movies.ToList());
            }

            return View(movies);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID, MovieName, Date, Rate, PosterURL, MovieURL, Genre")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movie);
        }
    }
}