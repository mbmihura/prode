using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prode.Models
{
    public class Prediction
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int Id { get; set; }
        public string Letter { get; set; }
        public DateTime Date { get; set; }
        public string TeamA { get; set; }
        public string FlagUrlA { get; set; }
        public string TeamB { get; set; }
        public string FlagUrlB { get; set; }
        public string MatchId { get; set; }
        public DateTime SubmitDate { get; set; }
        public string WinnerTeam { get; set; }
        public bool Enable { get; set; }

        static public List<Prediction> getAllForUser(int userId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getPredictionsForUser", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserId", userId));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<Prediction> matches = new List<Prediction>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                matches.Add(new Prediction()
                {
                    Letter = Convert.ToString(rdr["groupLetter"]),
                    Id = Convert.ToInt32(rdr["id"]),
                    Date = Convert.ToDateTime(rdr["date"]),
                    TeamA = Convert.ToString(rdr["teamA"]),
                    FlagUrlA = Convert.ToString(rdr["flagUrlA"]),
                    TeamB = Convert.ToString(rdr["teamB"]),
                    FlagUrlB = Convert.ToString(rdr["flagUrlB"]),
                    WinnerTeam = Convert.ToString(rdr["winnerTeam"]),
                    Enable = Convert.ToBoolean(rdr["enable"])
                });
            }
            return matches;
        }

        internal bool SaveToDbIn(string userId)
        {
            // TODO save to db, return true if success
            return true;
        }
    }

    
}