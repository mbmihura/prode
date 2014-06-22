using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prode.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<User> GetAll()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getUsers", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<User> table = new List<User>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                User p = new User()
                {
                    Id = Convert.ToInt32(rdr["id"]),
                    Username = Convert.ToString(rdr["username"])
                };
                table.Add(p);
            }
            return table;
        }
    }
}