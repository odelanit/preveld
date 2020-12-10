using Preveld.Infrastructure;
using Preveld.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        [CustomAuthorize]
        public ActionResult Index()
        {
            List<UserProfile> lists = db.UserProfiles.ToList();
            return View();
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

        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "Un Authorized Page!";

            return View();
        }
    }
}