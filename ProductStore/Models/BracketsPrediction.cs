using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Prode.Models
{
    public class BracketsPrediction
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public int Etapa { get; set; }
        public string Resultado { get; set; }
        public string Prediccion { get; set; }
        public int? PuntosGanados { get; set; }

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static List<BracketsPrediction> GetByUser(int userId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getBracketsPredictionsForUserId", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<BracketsPrediction> matches = new List<BracketsPrediction>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                BracketsPrediction p = new BracketsPrediction()
                {
                    Id = Convert.ToInt32(rdr["situacionId"]),
                    Key = Convert.ToString(rdr["desc"]),
                    Etapa = Convert.ToInt32(rdr["etapa"])
                };
                if (rdr["resultado"] != DBNull.Value)
                    p.Resultado = Convert.ToString(rdr["resultado"]);
                if (rdr["prediccion"] != DBNull.Value)
                    p.Prediccion = Convert.ToString(rdr["prediccion"]);
                if (rdr["puntosGanados"] != DBNull.Value)
                    p.PuntosGanados = Convert.ToInt32(rdr["puntosGanados"]);
                matches.Add(p);
            }
            return matches;
        }
    }
}
