using Preveld.Infrastructure;
using Preveld.Models;
using Preveld.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class WrapController : BaseController
    {
        private ApplicationDBContext db = new ApplicationDBContext();
        
        [CustomAuthorize]
        public ActionResult Index()
        {
            List<Wrap> lists = db.Wraps.ToList();
            return View(lists);
        }

        [CustomAuthorize]
        public ActionResult Create()
        {
            WrapHistoryViewModel wrapHistoryViewModel = new WrapHistoryViewModel();
            wrapHistoryViewModel.wrap = new Wrap();
            wrapHistoryViewModel.wraps = db.Wraps.OrderByDescending(x => x.Date_of_last_Inspection).ToList();
            return View(wrapHistoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Create(WrapHistoryViewModel wrapHistoryViewModel)
        {
            var wrap = wrapHistoryViewModel.wrap;

            if (ModelState.IsValid)
            {
                db.Wraps.Add(wrap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            wrapHistoryViewModel.wraps = db.Wraps.OrderByDescending(x => x.Date_of_last_Inspection).ToList();
            return View(wrapHistoryViewModel);
        }

        [CustomAuthorize]
        public ActionResult Show(int ID)
        {
            WrapHistoryViewModel wrapHistoryViewModel = new WrapHistoryViewModel();
            var currentWrap = db.Wraps.Find(ID);
            wrapHistoryViewModel.wrap = currentWrap;
            wrapHistoryViewModel.wraps = db.Wraps.OrderByDescending(x => x.Date_of_last_Inspection).ToList();

            var nextWrap = db.Wraps.Where(x => x.Date_of_last_Inspection < currentWrap.Date_of_last_Inspection).OrderByDescending(x => x.Date_of_last_Inspection).FirstOrDefault();
            if (nextWrap != null)
            {
                ViewBag.NextRecordID = nextWrap.ID;
            } else
            {
                ViewBag.NextRecordID = null;
            }

            var prevWrap = db.Wraps.Where(x => x.Date_of_last_Inspection > currentWrap.Date_of_last_Inspection).OrderBy(x => x.Date_of_last_Inspection).FirstOrDefault();
            if (prevWrap != null)
            {
                ViewBag.PrevRecordID = prevWrap.ID;
            }
            else
            {
                ViewBag.PrevRecordID = null;
            }


            return View(wrapHistoryViewModel);
        }

        [CustomAuthorize]
        public ActionResult Edit(int ID)
        {
            var wrap = db.Wraps.Find(ID);
            return View(wrap);
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Wrap wrap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wrap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wrap);
        }

        [CustomAuthorize]
        public ActionResult Delete(int ID)
        {
            var wrap = db.Wraps.Find(ID);
            db.Wraps.Remove(wrap);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}