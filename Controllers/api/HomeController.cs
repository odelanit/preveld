using Preveld.Models;
using Preveld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Preveld.Controllers.api
{
    public class HomeController : ApiController
    {
        readonly ApplicationDBContext db = new ApplicationDBContext();

        [Authorize]
        [HttpGet]
        public object Clients()
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
            var list = query.ToList();

            return new { 
                data = list
            };
        }

       
    }
}