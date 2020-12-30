using Preveld.Infrastructure;
using Preveld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class ClientController : BaseController
    {
        private ApplicationDBContext _db = new ApplicationDBContext();
        
        [CustomAuthorize]
        public ActionResult Index(string name)
        {
            var valves = _db.Valves.Where(v => v.Client.Equals(name)).ToList();
            var wraps = _db.Wraps.Where(w => w.Client.Equals(name)).ToList();
            ClientDataViewModel clientDataViewModel = new ClientDataViewModel();
            clientDataViewModel.Valves = valves;
            clientDataViewModel.Wraps = wraps;
            ViewBag.Client = name;
            return View(clientDataViewModel);
        }
    }
}