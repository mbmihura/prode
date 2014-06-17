using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;
using System.Net.Http.Headers;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Prode.Controllers
{
    public class AuthenticationController : ApiController
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        // POST api/authentication
        public User Get(string token)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            // 1.  create a command object identifying the stored procedure
            SqlCommand cmd = new SqlCommand("getUserDataForUserId", conn);

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserId", token));

            // execute the command
            SqlDataReader rdr = cmd.ExecuteReader();
            List<BracketsPrediction> matches = new List<BracketsPrediction>();

            // iterate through results, printing each to console
            User user = null;
            while (rdr.Read())
            {
                user = new User()
                {
                    Id = Convert.ToInt32(rdr["id"]),
                    Username = Convert.ToString(rdr["username"]),
                    Email = Convert.ToString(rdr["email"])
                };
            }
            return user;
        }

        // POST api/authentication
        public void Post([FromBody]User newUser)
        {
            if (!newUser.SingUp()) {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            };
        }

        // PUT api/authentication/
        public HttpResponseMessage Put([FromBody] UserCredentials userCredential)
        {
            User currentUser = userCredential.LogIn();
            if (currentUser != null)
            {
                var resp = new HttpResponseMessage();

                //create and set cookie in response
                var cookie = new CookieHeaderValue("userId", currentUser.Id.ToString());
                cookie.Path = "/";
                cookie.Expires = DateTime.Today.AddYears(1);
                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });

                cookie = new CookieHeaderValue("pass", userCredential.Password);
                cookie.Path = "/";
                cookie.Expires = DateTime.Today.AddYears(1);
                resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                resp.StatusCode = HttpStatusCode.OK;

                var a = Request.Headers.GetCookies();
                return resp;
            } else {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        // DELETE api/usercredentials/
        public void Delete()
        {
            var a = Request.Headers.GetCookies().First(cs => cs.Cookies.Any(c => c.Name == "userId")).Expires = DateTime.Now.AddDays(-1);
            
        }
    }
}
