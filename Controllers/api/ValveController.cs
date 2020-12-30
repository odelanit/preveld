using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Preveld.Controllers.api
{
    public class ValveController : ApiController
    {
        ApplicationDBContext _db = new ApplicationDBContext();

        [Authorize]
        [HttpGet]
        public object Trend(string id)
        {
            var valves = _db.Valves
                .Where(v => v.Client.Equals(id))
                .Select(v => new {v.Date_of_Inspection, v.Leak_Classification, v.Leak_Rate_kgmin})
                .ToList();
            return new { data = valves };
        }

        [Authorize]
        [HttpGet]
        public object Get(int id)
        {
            var valve = _db.Valves.FirstOrDefault(v => v.ID == id);
            return new {data = valve};
        }
    }
}