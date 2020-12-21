using Preveld.Infrastructure;
using Preveld.Models;
using Preveld.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class HomeController : BaseController
    {
        [CustomAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}