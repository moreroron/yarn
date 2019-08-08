using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using yarn_rider.Models;

namespace yarn_rider.Controllers
{
    public class UserController : Controller
    {
        SiteDbContext db = new SiteDbContext();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Index(string usernameKeyword)
        {
            if (String.IsNullOrEmpty(usernameKeyword)) return View(db.Users.ToList());

            var users = db.Users.Where(user => user.UserName.Contains(usernameKeyword));
            return View(users.ToList());
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            ViewBag.Message = user.UserName;
            return View(user);
        }


        [HttpPost]
        public ActionResult Edit(User user)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            if (user.Admin)
            {
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Details/" + user.UserID, "User");
        }

        public ActionResult Reviews()
        {
            return View();
        }
    }
}