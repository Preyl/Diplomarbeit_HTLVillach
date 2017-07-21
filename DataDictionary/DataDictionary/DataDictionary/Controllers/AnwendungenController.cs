using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataDictionary.Models;
using System.Data.Entity.Infrastructure;
using DataDictionary.ViewModels;

namespace DataDictionary.Controllers
{
    public class AnwendungenController : Controller
    {
        private EntityDataModel db = new EntityDataModel();

        // GET: Anwendungen
        public ActionResult Index()
        {
            return View(db.Anwendung.ToList());
        }

        // GET: Anwendungen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anwendung anwendung = db.Anwendung.Find(id);
            if (anwendung == null)
            {
                return HttpNotFound();
            }
            return View(anwendung);
        }

        // GET: Anwendungen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anwendungen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Beschreibung")] Anwendung anwendung)
        {
            if (ModelState.IsValid)
            {
                db.Anwendung.Add(anwendung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anwendung);
        }

        // GET: Anwendungen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anwendung anwendung = db.Anwendung
              .Include(a => a.MeineDatentypen)
              .Where(i => i.Id == id)
              .Single();

            if (anwendung == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedDatentypData(anwendung);
            return View(anwendung);
        }

        // POST: Anwendungen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedDatentypen)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var anwendungToUpdate = db.Anwendung
                .Include(p => p.MeineDatentypen)
                .Where(i => i.Id == id)
                .Single();

            if (TryUpdateModel(anwendungToUpdate, "",
                   new string[] { "Id", "Name", "Beschreibung" }))
            {
                try
                {
                    UpdateAnwendungDatentyp(selectedDatentypen, anwendungToUpdate);                
                    db.Entry(anwendungToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            //PopulateAssignedAnwendungData(datentypToUpdate);
            //ViewBag.FeldId = new SelectList(db.Datentyp, "Id", "Name", datentypToUpdate.Id);
            return View(anwendungToUpdate);
        }

        // GET: Anwendungen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anwendung anwendung = db.Anwendung.Find(id);
            if (anwendung == null)
            {
                return HttpNotFound();
            }
            return View(anwendung);
        }

        // POST: Anwendungen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anwendung anwendung = db.Anwendung.Find(id);
            db.Anwendung.Remove(anwendung);
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

        private void PopulateAssignedDatentypData(Anwendung anwendung)
        {
            var alleDatentypen = db.Datentyp;
            var AnwendungDatentyp = new HashSet<int>(anwendung.MeineDatentypen.Select(b => b.Id));
            var viewModel = new List<Datentyp_Anwendung_VM>();
            foreach (var datentyp in alleDatentypen)
            {
                viewModel.Add(new Datentyp_Anwendung_VM
                {
                    DatentypID = datentyp.Id,
                    DatentypName = datentyp.Name,
                    Assigned = AnwendungDatentyp.Contains(datentyp.Id)
                });
            }
            ViewBag.Datentyp = viewModel;
        }
        private void UpdateAnwendungDatentyp(string[] selectedDatentypen, Anwendung anwendungToUpdate)
        {
            if (selectedDatentypen == null)
            {
                anwendungToUpdate.MeineDatentypen = new List<Datentyp>();
                return;
            }

            var selectedDatentypenHS = new HashSet<string>(selectedDatentypen);
            var datentypenAnwendungen = new HashSet<int>
                (anwendungToUpdate.MeineDatentypen.Select(d => d.Id));

            foreach (var datentyp in db.Datentyp)
            {
                if (selectedDatentypenHS.Contains(datentyp.Id.ToString()))
                {
                    if (!datentypenAnwendungen.Contains(datentyp.Id))
                    {
                        anwendungToUpdate.MeineDatentypen.Add(datentyp);
                    }
                }
                else
                {
                    if (datentypenAnwendungen.Contains(datentyp.Id))
                    {
                        anwendungToUpdate.MeineDatentypen.Remove(datentyp);
                    }
                }
            }
        }
    }
}
