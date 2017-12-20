using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataDictionary.Models;
using System.Data.SqlClient;

namespace DataDictionary.Controllers
{
    public class FelderController : Controller
    {
        private EntityDataModel db = new EntityDataModel();

        // GET: Felder
        public ActionResult Index()
        {
            return View(db.Feld.ToList());
        }

        // GET: Felder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feld feld = db.Feld.Find(id);
            if (feld == null)
            {
                return HttpNotFound();
            }
            return View(feld);
        }

        // GET: Felder/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Felder/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Sensibel,PK")] Feld feld, int? id)
        {
            if (ModelState.IsValid)
            {
                db.Feld.Add(feld);
                db.SaveChanges();
                return RedirectToAction("Edit", "Datentypen", new { Id = id});
            }

            return View(feld);
        }

        // GET: Felder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feld feld = db.Feld.Find(id);
            if (feld == null)
            {
                return HttpNotFound();
            }
            return View(feld);
        }

        // POST: Felder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Sensibel,PK")] Feld feld, int? id, int? did)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feld).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Datentypen", new { Id = did });
            }
            return View(feld);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}