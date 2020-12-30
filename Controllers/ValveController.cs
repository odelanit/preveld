using System;
using Preveld.Infrastructure;
using Preveld.Models;
using Preveld.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using QRCoder;

namespace Preveld.Controllers
{
    public class ValveController : BaseController
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        
        [CustomAuthorize]
        public ActionResult Index()
        {
            List<Models.Valve> lists = db.Valves.OrderByDescending(v => v.Date_of_Inspection).ToList();
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
            var currentValve = db.Valves.Find(ID);
            valveHistory.Valve = db.Valves.Find(ID);
            valveHistory.Valves = db.Valves.OrderByDescending(x => x.Date_of_Inspection).ToList();

            var nextValve = db.Valves.Where(x => x.Date_of_Inspection < currentValve.Date_of_Inspection).OrderByDescending(x => x.Date_of_Inspection).FirstOrDefault();
            if (nextValve != null)
            {
                ViewBag.NextRecordID = nextValve.ID;
            }
            else
            {
                ViewBag.NextRecordID = null;
            }

            var prevValve = db.Valves.Where(x => x.Date_of_Inspection > currentValve.Date_of_Inspection).OrderBy(x => x.Date_of_Inspection).FirstOrDefault();
            if (prevValve != null)
            {
                ViewBag.PrevRecordID = prevValve.ID;
            }
            else
            {
                ViewBag.PrevRecordID = null;
            }

            var plainText = "{\"valve\":" + currentValve.ID + "}";

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            valveHistory.QrCode = BitmapToBytesCode(qrCodeImage);

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

        [NonAction]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}