using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prode.Models
{
    public class Mejores
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
        public string Prediction { get; set; }
        public string Real { get; set; }
        public int? PtosGanados { get; set; }

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static List<Mejores> GetByUser()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getMejoresPredictions", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Mejores> matches = new List<Mejores>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Mejores p = new Mejores()
                {
                    Key = Convert.ToString(rdr["desc"]),
                    Username = Convert.ToString(rdr["username"]),
                    UserId = Convert.ToInt32(rdr["userId"]),
                    Prediction = Convert.ToString(rdr["prediccion"]),
                    Real = Convert.ToString(rdr["resultado"]),
                };
                if (rdr["puntosGanados"] != DBNull.Value)
                    p.PtosGanados = Convert.ToInt32(rdr["puntosGanados"]);
                matches.Add(p);
            }
            return matches;
        }
    }


}