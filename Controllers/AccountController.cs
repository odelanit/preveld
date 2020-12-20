using Preveld.Models;
using Preveld.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserProfile userProfile = db.UserProfiles.Where(x => x.Email.Equals(model.Email)).FirstOrDefault();
            if (userProfile == null)
            {
                ModelState.AddModelError("Email", "We can't find a user with that e-mail address.");
                return View(model);
            }

            if (WebSecurity.UserExists(userProfile.User_ID))
            {
                var token = WebSecurity.GeneratePasswordResetToken(userProfile.User_ID);
                if (token == null)
                {
                    return View(model);
                }
                else
                {
                    string To = userProfile.Email, UserID, Password, SMTPPort, Host;
                    var url = Request.Url.Scheme + "://" + Request.Url.Authority +  Url.Action("ResetPassword", "Account", new { email = userProfile.Email, code = token });
                    var linkHref = "<a href=\"" + url + "\">Reset Password</a>";
                    string subject = "Reset Password Notification";
                    string body = "<b>Please find the password reset link.</b><br />" + linkHref;

                    EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);
                    EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);

                }
            }

            ViewBag.Message = "We have e-mailed your password reset link!";
            return View(model);
        }

        public ActionResult ResetPassword(string code, string email)
        {
            ResetPassword model = new ResetPassword();
            model.Email = email;
            model.ReturnToken = code;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                var membership = db.Memberships.Where(m => m.PasswordVerificationToken.Equals(model.ReturnToken)).FirstOrDefault();
                if (membership == null)
                {
                    ViewBag.Message = "Something went horribly wrong!";
                } else
                {
                    var userProfile = membership.User;
                    userProfile.User_PW = model.Password;
                    db.Entry(userProfile).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }
    }
}