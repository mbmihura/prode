using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ProductStore.Controllers
{
    public class LogController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        // POST api/log
        public void Post(LogEntry entry)
        {
            var req = ((System.Web.HttpContextWrapper)this.Request.Properties["MS_HttpContext"]).Request;

            // Look for a proxy address first
            String _ip = req.ServerVariables["HTTP_X_FORWARDED_FOR"];

            // If there is no proxy, get the standard remote address
            if (_ip == null || _ip == "" || _ip.ToLower() == "unknown")
	            _ip = req.ServerVariables["REMOTE_ADDR"];

            var ip = req.UserHostAddress;
            var userAgent = req.UserAgent;
            using(SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("insertEventToLog", conn))
            {
                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@userId", entry.userId));
                cmd.Parameters.Add(new SqlParameter("@type", entry.type));
                cmd.Parameters.Add(new SqlParameter("@desc", entry.desc));
                cmd.Parameters.Add(new SqlParameter("@ip", ip));
                cmd.Parameters.Add(new SqlParameter("@userAgent", userAgent));

                // execute the command
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                conn.Close();
            }
        }
    }
}
