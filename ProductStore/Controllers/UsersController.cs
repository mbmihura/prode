using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Prode.Models;

namespace Prode.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/Users/234234
        public List<User> GetAll()
        {
            return Prode.Models.User.GetAll();
        }
    }
}
