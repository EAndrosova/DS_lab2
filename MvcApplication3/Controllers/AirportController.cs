using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;
using System.Web.Security;
using WebMatrix.WebData;
using MvcApplication3.Filters;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class AirportController : Controller
    {
        private DS_2Entities2 db = new DS_2Entities2();

        //
        // GET: /Airport/

        public ActionResult Index(string from, string to)
        {
            var h = Request.Headers.ToString();
            if (WebSecurity.IsAuthenticated)
            {
                DS_2Entities8 db1 = new Models.DS_2Entities8();
                var user = db1.Users.Find(WebSecurity.CurrentUserName);
                var tok_db = user.Token.Trim();
                var tok = Request.Headers["Autherization"].ToString();
                if (!tok.Contains("Bearer "))
                {
                    Response.StatusCode = 400;
                    var error = new Dictionary<string, string>();
                    error.Add("error", "bad request");
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                tok = tok.Substring(7, tok.Length - 7);
                if (DateTime.Now > user.ExpireTime)
                {
                    Response.StatusCode = 200;
                    var error = new Dictionary<string, string>();
                    error.Add("error", "token_expired");
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                if (tok_db == tok)
                {
                    var portsToSee = new List<Models.Airports>();
                    var ports = db.Airports.ToList();
                    var fromI = Convert.ToInt32(from);
                    var toI = Convert.ToInt32(to);
                    for (int i = fromI; i < toI; i++)
                    {
                        portsToSee.Add(ports[i]);
                    }

                    return Json(portsToSee, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response.StatusCode = 403;
                }
            }
            //return new HttpStatusCodeResult(101, "Access denied");
            // json error: token_expired
            Response.StatusCode = 403;
            return View("Error");
            //return View(db.Airports.ToList());
        }

        //
        // GET: /Airport/Details/5

        public ActionResult Details(int id = 0)
        {
            var h = Request.Headers.ToString();
            if (WebSecurity.IsAuthenticated)
            {
                var tok = Request.Headers["Autherization"].ToString();
                if (!tok.Contains("Bearer "))
                {
                    Response.StatusCode = 400;
                    var error = new Dictionary<string, string>();
                    error.Add("error", "bad request");
                    return Json(error, JsonRequestBehavior.AllowGet);
                }
                tok = tok.Substring(7, tok.Length - 7);
                Airports airports = db.Airports.Find(id);
                if (airports == null)
                {
                    return HttpNotFound();
                }
                DS_2Entities8 db1 = new Models.DS_2Entities8();
                var user = db1.Users.Find(WebSecurity.CurrentUserName);
                var tok_db = user.Token.Trim();
                
                if (tok_db == tok && DateTime.Now < user.ExpireTime)
                {
                    return Json(airports, JsonRequestBehavior.AllowGet);
                }
                //return View(airports);
                Response.StatusCode = 403;
                return View("Error");
            }
            Response.StatusCode = 403;
            return View("Error");
        }

        //
        // GET: /Airport/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Airport/Create

        [HttpPost]
        public ActionResult Create(Airports airports)
        {
            if (ModelState.IsValid)
            {
                db.Airports.Add(airports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(airports);
        }

        //
        // GET: /Airport/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Airports airports = db.Airports.Find(id);
            if (airports == null)
            {
                return HttpNotFound();
            }
            return View(airports);
        }

        //
        // POST: /Airport/Edit/5

        [HttpPost]
        public ActionResult Edit(Airports airports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(airports);
        }

        //
        // GET: /Airport/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Airports airports = db.Airports.Find(id);
            if (airports == null)
            {
                return HttpNotFound();
            }
            return View(airports);
        }

        //
        // POST: /Airport/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Airports airports = db.Airports.Find(id);
            db.Airports.Remove(airports);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}