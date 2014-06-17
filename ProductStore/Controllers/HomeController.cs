using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prode.Models;

namespace Prode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
                //string cookie = this.ControllerContext.HttpContext.Request.Cookies["currentUser"].Value;
                return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
    
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            this.ControllerContext.HttpContext.Response.Cookies.Clear();
            return RedirectToAction("Login");
        }
    }
}
