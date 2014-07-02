using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;

namespace Prode.Controllers
{
    public class MejoresController : ApiController
    {
        // GET api/mejores
        public IEnumerable<dynamic> Get()
        {
            return Mejores.GetByUser()
                .GroupBy(p => p.UserId)
                .Select(g => new { 
                    userId = g.Key,
                    username = g.First().Username,
                    campeon = g.First(p => p.Key == "camp"),
                    sorpresa = g.First(p => p.Key == "sorp"),
                    goleador = g.First(p => p.Key == "gol"),
                    mejor = g.First(p => p.Key == "mej")
                })
                .ToList();
        }

        // POST api/mejores
        public void Post([FromBody]string value)
        {
        }
    }
}
