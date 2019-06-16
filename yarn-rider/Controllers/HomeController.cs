using System.Linq;
using System.Web.Mvc;
using yarn_rider.Models;

namespace yarn_rider.Controllers
{
    public class HomeController : Controller
    {
        private SiteDbContext db = new SiteDbContext();
        
        public ActionResult Index()
        {
            if (Session["currentUser"] == null) return RedirectToAction("Login", "Account");
            return View(db.Movies.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            if (Session["currentUser"] == null) return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult Locations()
        {
            if (Session["currentUser"] == null) return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult Share()
        {
            return View();
        }
    }
}