using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prode.Models
{
    public class Position
    {
        public string User;
        public int Points;

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<Position> GetTable()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getPosiciones", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Position> table = new List<Position>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Position p = new Position()
                {
                    Points = Convert.ToInt32(rdr["puntos"]),
                    User = Convert.ToString(rdr["user"])
                };
                table.Add(p);
            }
            return table;
        }
    }
}