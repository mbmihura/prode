using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prode.Models
{
    public class GroupMatchPrediction
    {
        public int Id { get; set; }
        public string Letter { get; set; }
        public DateTime Date { get; set; }
        public string TeamL { get; set; }
        public string TeamV { get; set; }
        public string Resultado { get; set; }
        public string Prediccion { get; set; }
        public int? PuntosGanados { get; set; }
        public bool Enable { get; set; }

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<GroupMatchPrediction> GetByUser(int userId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getGroupsMatchesPredictionsForUserId", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<GroupMatchPrediction> matches = new List<GroupMatchPrediction>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                GroupMatchPrediction p = new GroupMatchPrediction()
                {
                    Id = Convert.ToInt32(rdr["situacionId"]),
                    Letter = Convert.ToString(rdr["groupLetter"]),
                    TeamL = Convert.ToString(rdr["teamL"]),
                    TeamV = Convert.ToString(rdr["teamV"]),
                };
                if (rdr["prediccion"] != DBNull.Value)
                    p.Prediccion = Convert.ToString(rdr["prediccion"]);
                if (rdr["resultado"] != DBNull.Value)
                    p.Resultado = Convert.ToString(rdr["resultado"]);
                if (rdr["date"] != DBNull.Value)
                    p.Date = Convert.ToDateTime(rdr["date"]);
                if(rdr["puntosGanados"] != DBNull.Value)
                    p.PuntosGanados =  Convert.ToInt32(rdr["puntosGanados"]);
                matches.Add(p);
            }
            return matches;
        }
    }
}