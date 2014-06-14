using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Prode.Models
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public User LogIn()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getUserForUsernameAndPass", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@Username", this.Username));
            cmd.Parameters.Add(new SqlParameter("@Password", this.Password));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            User currentUser = null;

            // iterate through results, printing each to console
            if (rdr.Read())
            {
                currentUser = new User()
                {
                    Id = Convert.ToInt32(rdr["id"]),
                    Username = Convert.ToString(rdr["username"]),
                    Password = Convert.ToString(rdr["password"]),
                    Email = Convert.ToString(rdr["email"])
                };
            }
            return currentUser;
        }
    }
}
