using Preveld.Models;
using System.Linq;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Account
        public ActionResult Login()
        {
            if (Session["User_ID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserProfile user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.UserProfiles.Where(a => a.User_ID.Equals(user.User_ID) && a.User_PW.Equals(user.User_PW)).FirstOrDefault();
                if (obj != null)
                {
                    Session["User_ID"] = obj.User_ID.ToString();
                    Session["Full_Name"] = obj.Full_Name.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session["User_ID"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}