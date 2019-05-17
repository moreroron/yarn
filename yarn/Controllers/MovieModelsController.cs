using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using yarn.Models;

namespace yarn.Controllers
{
    public class MovieModelsController : Controller
    {
        private MovieDbContext db = new MovieDbContext();
        

        // GET: MovieModels
        public ActionResult Index()
        {
            return View(db.Movies.ToList());
        }

        // GET: MovieModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // GET: MovieModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,Name,Date,Rate")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movieModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(movieModels);
        }

        // GET: MovieModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // POST: MovieModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,Name,Date,Rate")] MovieModels movieModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieModels);
        }

        // GET: MovieModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieModels movieModels = db.Movies.Find(id);
            if (movieModels == null)
            {
                return HttpNotFound();
            }
            return View(movieModels);
        }

        // POST: MovieModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieModels movieModels = db.Movies.Find(id);
            db.Movies.Remove(movieModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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
