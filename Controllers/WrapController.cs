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
    public class WrapController : BaseController
    {
        private ApplicationDBContext db = new ApplicationDBContext();
        
        [CustomAuthorize]
        public ActionResult Index()
        {
            List<Wrap> lists = db.Wraps.OrderByDescending(w => w.Date_of_last_Inspection).ToList();
            return View(lists);
        }

        [CustomAuthorize]
        public ActionResult Create()
        {
            WrapHistoryViewModel wrapHistoryViewModel = new WrapHistoryViewModel();
            wrapHistoryViewModel.Wrap = new Wrap();
            wrapHistoryViewModel.Wraps = db.Wraps.OrderByDescending(x => x.Date_of_last_Inspection).ToList();
            return View(wrapHistoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize]
        public ActionResult Create(WrapHistoryViewModel wrapHistoryViewModel)
        {
            var wrap = wrapHistoryViewModel.Wrap;

            if (ModelState.IsValid)
            {
                db.Wraps.Add(wrap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            wrapHistoryViewModel.Wraps = db.Wraps.OrderByDescending(x => x.Date_of_last_Inspection).ToList();
            return View(wrapHistoryViewModel);
        }

        [CustomAuthorize]
        public ActionResult Show(int ID)
        {
            WrapHistoryViewModel wrapHistoryViewModel = new WrapHistoryViewModel();
            var currentWrap = db.Wraps.Find(ID);
            wrapHistoryViewModel.Wrap = currentWrap;
            wrapHistoryViewModel.Wraps = db.Wraps.OrderByDescending(x => x.Date_of_last_Inspection).ToList();

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

            var plainText = "{\"wrap\":" + currentWrap.ID + "}";

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            wrapHistoryViewModel.QrCode = BitmapToBytesCode(qrCodeImage);


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