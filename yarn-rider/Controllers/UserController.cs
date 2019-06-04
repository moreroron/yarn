using System;
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
        
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = db.Users.Find(id);
            if (user == null) return HttpNotFound();

            ViewBag.Message = user.UserName;

            return View(user);
        }

//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            
//            User user = db.Users.Find(id);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            return View(user);
//        }
        
    }
}