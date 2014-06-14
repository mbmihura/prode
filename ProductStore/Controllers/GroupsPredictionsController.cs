using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;

namespace Prode.Controllers
{
    public class GroupsPredictionsController : ApiController
    {
        // GET api/groupspredictions/234234
        public List<GroupMatchPrediction> GetAll(int userId)
        {
            return GroupMatchPrediction.GetByUser(userId);
        }

        // POST api/groupspredictions
        public void Post([FromBody]string value)
        {
        }

        // PUT api/groupspredictions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/groupspredictions/5
        public void Delete(int id)
        {
        }
    }
}
