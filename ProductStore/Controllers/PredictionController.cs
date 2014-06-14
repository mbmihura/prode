using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;

namespace Prode.Controllers
{
    public class PredictionController : ApiController
    {
        // GET api/prediction
        public IEnumerable<Prediction> Get()
        {
            var a = Request.Headers.GetCookies();

            bool userIdPresent = Request.Headers.GetCookies().Any(cs => cs.Cookies.Any(c => c.Name == "userId")) && Request.Headers.GetCookies().First(cs => cs.Cookies.Any(c => c.Name == "userId")).Cookies.Any(c => c.Name == "userId");
            if (!userIdPresent)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            else {
                string userId = Request.Headers.GetCookies().First(cs => cs.Cookies.Any(c => c.Name == "userId")).Cookies.First(c => c.Name == "userId").Value;
                return Prediction.getAllForUser(Convert.ToInt32(userId));
            }
        }

        // POST api/prediction
        public void Post([FromBody]List<Prediction> predictionsList)
        {
            bool error = false;
            bool userIdPresent = Request.Headers.GetCookies().Any(cs => cs.Cookies.Any(c => c.Name == "userId")) && Request.Headers.GetCookies().First(cs => cs.Cookies.Any(c => c.Name == "userId")).Cookies.Any(c => c.Name == "userId");
            if (!userIdPresent)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            else
            {
                string userId = Request.Headers.GetCookies().First(cs => cs.Cookies.Any(c => c.Name == "userId")).Cookies.First(c => c.Name == "userId").Value;
                foreach (Prediction p in predictionsList)
                {
                    error = error || !p.SaveToDbIn(userId);
                }
                if (error)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
    }
}
