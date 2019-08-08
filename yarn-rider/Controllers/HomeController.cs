using System.Linq;
using System.Web.Mvc;
using Tweetinvi;
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

        [HttpPost]
        public ActionResult Share(string content)
        {
            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials("96mnJPmylSXUbFjrumCAsCSS0", "vDZ34fIFOvXRwNotTObifVpbXnpRnlUDS6ija3O6QkiS9Fudho",
                "1140353914900029444-uL7VCwYLwLyMiITcheOarBH9kKiJ2A", "G5DCUz8JlfAH8w4uYr2oOzgSinF78DYz5uD86eR9L5azk");

            // Publish the Tweet on my Timeline
            Tweet.PublishTweet(content);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Statistics()
        {
            return View();
        }


        public ActionResult Records()
        {
            return View();
        }
    }
}