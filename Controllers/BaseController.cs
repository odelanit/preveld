using Preveld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Preveld.Controllers
{
    public class BaseController : Controller
    {
        ApplicationDBContext db = new ApplicationDBContext();

        public BaseController()
        {
            var query = db.Valves.Select(v => new Client()
            {
                Name = v.Client
            });

            var query2 = db.Wraps.Select(v => new Client()
            {
                Name = v.Client
            });

            query = query.Union(query2);
            ViewBag.Clients = query.ToList();
        }
    }
}