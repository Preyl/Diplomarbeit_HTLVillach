using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataDictionary.Models;

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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Sensibel,PK")] Feld feld)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feld).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feld);
        }

        // GET: Felder/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Felder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feld feld = db.Feld.Find(id);
            db.Feld.Remove(feld);
            db.SaveChanges();
            return RedirectToAction("Index");
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
