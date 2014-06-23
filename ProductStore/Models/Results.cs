using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prode.Models
{
    public class Result
    {
        public int Id;
        public string Description;
        public bool IsActualResult;

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static List<Result> GetAllPosibleResultsForGroup(int situationId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getAllPosibleResults", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@SituationId", situationId));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Result> results = new List<Result>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Result p = new Result()
                {
                    Id = Convert.ToInt32(rdr["id"]),
                    Description = Convert.ToString(rdr["results"]),
                    IsActualResult = Convert.ToBoolean(rdr["isActualResult"])
                };
                if (p.Description.Length == 1)
                    results.Add(p);
            }
            return results;
        }

        public static List<Result> GetAllPosibleResultsForBrackets(int situationId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getAllPosibleResults", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@SituationId", situationId));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Result> results = new List<Result>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Result p = new Result()
                {
                    Id = Convert.ToInt32(rdr["id"]),
                    Description = Convert.ToString(rdr["results"]),
                    IsActualResult = Convert.ToBoolean(rdr["isActualResult"])
                };
                if (p.Description.Length > 1)
                    results.Add(p);
            }
            return results;
        }

        public static void SetResultForSituation(int id, string Description)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("setResultsForSituation", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@SituationId", id));
            cmd.Parameters.Add(new SqlParameter("@Result", Description));

            // execute the command
            cmd.ExecuteReader();
        }
    }
}