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
            return View(db.Movies.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        
        public ActionResult Locations()
        {
            return View();
        }
    }
}