using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;

namespace Prode.Controllers
{
    public class PositionsController : ApiController
    {
        // GET api/positions/
        public List<Position> GetAll()
        {
            return Position.GetTable()
                .GroupBy(p => p.Points)
                .Select(p => new Position { 
                    Points = p.Key,
                    User = p.Aggregate(String.Empty, 
                        (s, p2) => s + (string.IsNullOrEmpty(s) ?"":", ") + p2.User
                    )
                }).ToList<Position>();
           }





    }
}
