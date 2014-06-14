using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;

namespace Prode.Controllers
{
    public class BracketsPredictionsController : ApiController
    {
        // GET api/bracketspredictions
        public dynamic GetAll(int userId)
        {
            var predictionList = BracketsPrediction.GetByUser(userId);
            var predictionDic = predictionList.ToDictionary(e => e.Key);

            int subtotalOctavos = 0;
            int subtotalCuartos = 0;
            int subtotalSemi = 0;
            int subtotalFinal = 0;
            foreach (var p in predictionList.Where(p => p.PuntosGanados.HasValue))
            {
                switch (p.Etapa)
                {
                    case 8:
                        subtotalOctavos += p.PuntosGanados.Value;
                        break;
                    case 4:
                        subtotalCuartos += p.PuntosGanados.Value;
                        break;
                    case 2:
                        subtotalSemi += p.PuntosGanados.Value;
                        break;
                    case 1:
                        subtotalFinal += p.PuntosGanados.Value;
                        break;
                }
                
            }

            return new {
                A1 = predictionDic["A1"],
                B1 = predictionDic["B1"],
                C1 = predictionDic["C1"],
                D1 = predictionDic["D1"],
                E1 = predictionDic["E1"],
                F1 = predictionDic["F1"],
                G1 = predictionDic["G1"],
                H1 = predictionDic["H1"],
                A2 = predictionDic["A2"],
                B2 = predictionDic["B2"],
                C2 = predictionDic["C2"],
                D2 = predictionDic["D2"],
                E2 = predictionDic["E2"],
                F2 = predictionDic["F2"],
                G2 = predictionDic["G2"],
                H2 = predictionDic["H2"],

                A1B2 = predictionDic["A1-B2"],
                C1D2 = predictionDic["C1-D2"],
                E1F2 = predictionDic["E1-F2"],
                G1H2 = predictionDic["G1-H2"],
                B1A2 = predictionDic["B1-A2"],
                D1C2 = predictionDic["D1-C2"],
                F1E2 = predictionDic["F1-E2"],
                H1G2 = predictionDic["H1-G2"],

                A1B2C1D2 = predictionDic["A1-B2-C1-D2"],
                E1F2G1H2 = predictionDic["E1-F2-G1-H2"],
                B1A2D1C2 = predictionDic["B1-A2-D1-C2"],
                F1E2H1G2 = predictionDic["F1-E2-H1-G2"],

                A1B2C1D2E1F2G1H2 = predictionDic["A1-B2-C1-D2-E1-F2-G1-H2"],
                B1A2D1C2F1E2H1G2 = predictionDic["B1-A2-D1-C2-F1-E2-H1-G2"],

                subtotalOctavos = subtotalOctavos,
                subtotalCuartos = subtotalCuartos,
                subtotalSemi = subtotalSemi,
                subtotalFinal = subtotalFinal
            };
        }

        // POST api/bracketspredictions
        public void Post([FromBody]string value)
        {
        }

        // PUT api/bracketspredictions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/bracketspredictions/5
        public void Delete(int id)
        {
        }
    }
}
