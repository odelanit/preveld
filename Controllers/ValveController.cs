using Preveld.Infrastructure;
using Preveld.Models;
using Preveld.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class ValveController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        
        [CustomAuthorize]
        public ActionResult Index()
        {
            List<Models.Valve> lists = db.Valves.ToList();
            return View(lists);
        }

        [CustomAuthorize]
        public ActionResult Create()
        {
            ValveHistory valveHistoryViewModel = new ValveHistory();
            valveHistoryViewModel.Valve = new Valve();
            valveHistoryViewModel.Valves = db.Valves.OrderByDescending(x => x.Date_of_Inspection).ToList();
            return View(valveHistoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Create(ValveHistory valveHistory)
        {
            var valve = valveHistory.Valve;

            if (ModelState.IsValid)
            {
                db.Valves.Add(valve);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            valveHistory.Valves = db.Valves.OrderByDescending(x => x.Date_of_Inspection).ToList();
            return View(valveHistory);
        }

        [CustomAuthorize]
        public ActionResult Show(int ID)
        {
            ValveHistory valveHistory = new ValveHistory();
            var currentWrap = db.Valves.Find(ID);
            valveHistory.Valve = db.Valves.Find(ID);
            valveHistory.Valves = db.Valves.OrderByDescending(x => x.Date_of_Inspection).ToList();

            var nextValve = db.Valves.Where(x => x.Date_of_Inspection < currentWrap.Date_of_Inspection).OrderByDescending(x => x.Date_of_Inspection).FirstOrDefault();
            if (nextValve != null)
            {
                ViewBag.NextRecordID = nextValve.ID;
            }
            else
            {
                ViewBag.NextRecordID = null;
            }

            var prevValve = db.Valves.Where(x => x.Date_of_Inspection > currentWrap.Date_of_Inspection).OrderBy(x => x.Date_of_Inspection).FirstOrDefault();
            if (prevValve != null)
            {
                ViewBag.PrevRecordID = prevValve.ID;
            }
            else
            {
                ViewBag.PrevRecordID = null;
            }

            return View(valveHistory);
        }

        [CustomAuthorize]
        public ActionResult Edit(int ID)
        {
            var valve = db.Valves.Find(ID);
            return View(valve);
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Valve valve)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valve).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(valve);
        }

        [CustomAuthorize]
        public ActionResult Delete(int ID)
        {
            var valve = db.Valves.Find(ID);
            db.Valves.Remove(valve);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}