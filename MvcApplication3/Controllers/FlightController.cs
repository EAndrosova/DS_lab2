using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class FlightController : Controller
    {
        private DS_2Entities1 db = new DS_2Entities1();

        //
        // GET: /Flight/

        public ActionResult Index()
        {
            return View(db.Flights.ToList());
        }

        //
        // GET: /Flight/Details/5

        public ActionResult Details(Guid id)
        {
            Flights flights = db.Flights.Find(id);
            if (flights == null)
            {
                return HttpNotFound();
            }
           //return View(flights);
            
            return Json(flights, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Flight/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Flight/Create

        [HttpPost]
        public ActionResult Create(Flights flights)
        {
            if (ModelState.IsValid)
            {
                flights.FlightID = Guid.NewGuid();
                db.Flights.Add(flights);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flights);
        }

        //
        // GET: /Flight/Edit/5

        public ActionResult Edit(Guid id)
        {
            Flights flights = db.Flights.Find(id);
            if (flights == null)
            {
                return HttpNotFound();
            }
            return View(flights);
        }

        //
        // POST: /Flight/Edit/5

        [HttpPost]
        public ActionResult Edit(Flights flights)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flights).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flights);
        }

        //
        // GET: /Flight/Delete/5

        public ActionResult Delete(Guid id)
        {
            Flights flights = db.Flights.Find(id);
            if (flights == null)
            {
                return HttpNotFound();
            }
            return View(flights);
        }

        //
        // POST: /Flight/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Flights flights = db.Flights.Find(id);
            db.Flights.Remove(flights);
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