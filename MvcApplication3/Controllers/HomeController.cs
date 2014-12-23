using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using MvcApplication3.Filters;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult ForUsersOnly()
        {
            if (WebSecurity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Index", "Home");
        }

        public ActionResult Me()
        {
            var h = Request.Headers.ToString();
            if (WebSecurity.IsAuthenticated && h.Contains("access_token"))
            {
                DS_2Entities8 db1 = new Models.DS_2Entities8();
                var user = db1.Users.Find(WebSecurity.CurrentUserName);
                var tok_db = user.Token.Trim();
                var tok = Request.Headers["access_token"].ToString();
                if (tok_db == tok && DateTime.Now < user.ExpireTime)
                    return View();
            }
            Response.StatusCode = 403;
            return View("Error");
        }

        public ActionResult Status()
        {
            return View();
        }



        /*
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }*/
    }
}
