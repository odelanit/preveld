using System;
using Preveld.Infrastructure;
using Preveld.Models;
using Preveld.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using QRCoder;

namespace Preveld.Controllers
{
    public class HomeController : BaseController
    {
        ApplicationDBContext dbContext = new ApplicationDBContext();

        [CustomAuthorize]
        public ActionResult Index()
        {
            var latestValve = dbContext.Valves.OrderByDescending(v => v.Date_of_Inspection).FirstOrDefault();
            var latestWrap = dbContext.Wraps.OrderByDescending(w => w.Date_of_last_Inspection).FirstOrDefault();

            int valveId = -1;
            int wrapId = -1;
            if (latestValve != null)
            {
                valveId = latestValve.ID;
            }

            if (latestWrap != null)
            {
                wrapId = latestWrap.ID;
            }

            var plainText = "{\"valve\":" + valveId + ",\"wrap\":" + wrapId + "}";

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            LatestRecord record = new LatestRecord();
            record.qrCode = BitmapToBytesCode(qrCodeImage);
            record.Valve = latestValve;
            record.Wrap = latestWrap;

            return View(record);
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