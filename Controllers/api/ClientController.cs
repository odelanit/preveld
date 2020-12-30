using Preveld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Preveld.Controllers.api
{
    public class ClientController : ApiController
    {
        ApplicationDBContext db = new ApplicationDBContext();

        [Authorize]
        [HttpGet]
        public object Index(string name)
        {
            var valves = db.Valves.Where(v => v.Client.Equals(name)).OrderByDescending(v => v.Date_of_Inspection).ToList();
            var wraps = db.Wraps.Where(w => w.Client.Equals(name)).OrderByDescending(w => w.Date_of_last_Inspection).ToList();
            ClientDataViewModel clientDataViewModel = new ClientDataViewModel();
            clientDataViewModel.Valves = valves;
            clientDataViewModel.Wraps = wraps;
            return new { data = clientDataViewModel };
        }
    }
}