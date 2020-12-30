using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Preveld.Controllers.api
{
    public class WrapController : ApiController
    {
        ApplicationDBContext _db = new ApplicationDBContext();

        [Authorize]
        [HttpGet]
        public object Trend(string id)
        {
            var wraps = _db.Wraps
                .Where(w => w.Client.Equals(id))
                .Select(r => new {r.Date_of_last_Inspection, r.Final_Findings})
                .ToList();
            return new {data = wraps};
        }

        [Authorize]
        [HttpGet]
        public object Get(int id)
        {
            var wrap = _db.Wraps.FirstOrDefault(v => v.ID == id);
            return new { data = wrap };
        }
    }
}