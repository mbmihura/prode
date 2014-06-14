using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;
using System.Net.Http.Headers;

namespace Prode.Controllers
{
    public class AuthenticationController : ApiController
    {
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
