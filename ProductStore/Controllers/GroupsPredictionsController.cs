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

        // GET api/bracketspredictions
        public List<Result> GetPosibleResult(int situationId)
        {
            return Result.GetAllPosibleResultsForGroup(situationId);
        }

        // PUT api/groupspredictions/5
        [HttpPost]
        public void GetPosibleResult(int id, string resultado)
        {
            Result.SetResultForSituation(id, resultado);
        }
    }
}
