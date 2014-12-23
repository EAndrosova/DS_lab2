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
    public class IdsController : Controller
    {
        private DS_2Entities3 db = new DS_2Entities3();

        //
        // GET: /Ids/

        public ActionResult Index()
        {
            return View(db.Ids.ToList());
        }

        //
        // GET: /Ids/Details/5

        public ActionResult Details(string id = null)
        {
            Ids ids = db.Ids.Find(id);
            if (ids == null)
            {
                return HttpNotFound();
            }
            return View(ids);
        }

        //
        // GET: /Ids/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ids/Create

        [HttpPost]
        public ActionResult Create(Ids ids)
        {
            if (ModelState.IsValid)
            {
                db.Ids.Add(ids);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ids);
        }

        //
        // GET: /Ids/Edit/5

        public ActionResult Edit(string id = null)
        {
            Ids ids = db.Ids.Find(id);
            if (ids == null)
            {
                return HttpNotFound();
            }
            return View(ids);
        }

        //
        // POST: /Ids/Edit/5

        [HttpPost]
        public ActionResult Edit(Ids ids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ids);
        }

        //
        // GET: /Ids/Delete/5

        public ActionResult Delete(string id = null)
        {
            Ids ids = db.Ids.Find(id);
            if (ids == null)
            {
                return HttpNotFound();
            }
            return View(ids);
        }

        //
        // POST: /Ids/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Ids ids = db.Ids.Find(id);
            db.Ids.Remove(ids);
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