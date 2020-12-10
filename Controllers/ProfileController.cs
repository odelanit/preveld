using Preveld.Infrastructure;
using Preveld.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        [CustomAuthorize]
        public ActionResult Index()
        {
            List<UserProfile> lists = db.UserProfiles.ToList();
            return View(lists);
        }

        [CustomAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Create(UserProfile user)
        {
            bool IsUserIDExist = db.UserProfiles.Any(x => x.User_ID == user.User_ID && x.ID != user.ID);
            if (IsUserIDExist == true)
            {
                ModelState.AddModelError("User_ID", "Username already exists");
            }
            if (ModelState.IsValid)
            {
                db.UserProfiles.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult IsUsernameAvailable(string User_ID, int ? ID)
        {
            try
            {
                var userProfile = db.UserProfiles.Single(a => a.User_ID == User_ID && a.ID != ID);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [CustomAuthorize]
        public ActionResult Edit(int ID)
        {
            var userProfile = db.UserProfiles.SingleOrDefault(a => a.ID == ID);
            return View(userProfile);
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userProfile)
        {
            bool IsUserIDExist = db.UserProfiles.Any(x => x.User_ID == userProfile.User_ID && x.ID != userProfile.ID);
            if (IsUserIDExist == true)
            {
                ModelState.AddModelError("User_ID", "Username already exists");
            }
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userProfile);
        }

        [CustomAuthorize]
        public ActionResult Delete(int ID)
        {
            var userProfile = db.UserProfiles.Find(ID);
            db.UserProfiles.Remove(userProfile);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}