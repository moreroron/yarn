using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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
        public ActionResult Index(string searchString)
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

            return View(movies.ToList());
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

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = db.Movies.Find(id);
            if (movie == null) return HttpNotFound();
            
            ViewBag.Message = movie.MovieName;

            return View(movie);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Movie movie = db.Movies.Find(id);
            if (movie == null) return HttpNotFound();

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
    }
}