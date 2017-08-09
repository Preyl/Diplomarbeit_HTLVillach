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
              .Include(p => p.MeineFelder)
              .Where(i => i.Id == id)
              .Single();

            if (anwendung == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedDatentypData(anwendung);
            PopulateAssignedFeldData(anwendung);
            return View(anwendung);
        }

        // POST: Anwendungen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedDatentypen, string[] selectedFelder)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var anwendungToUpdate = db.Anwendung
                .Include(a => a.MeineDatentypen)
                .Include(a => a.MeineFelder)
                .Where(a => a.Id == id)
                .Single();

            if (TryUpdateModel(anwendungToUpdate, "", new string[] { "Id", "Name", "Beschreibung" }))
            {
                try
                {
                    UpdateAnwendungDatentyp(selectedDatentypen, anwendungToUpdate);
                    UpdateAnwendungFeld(selectedFelder, anwendungToUpdate);           
                    db.Entry(anwendungToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Edit", new { Id = anwendungToUpdate.Id });
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            //PopulateAssignedDatentypData(anwendungToUpdate);
            //ViewBag.FeldId = new SelectList(db.Datentyp, "Id", "Name", anwendungToUpdate.Id);
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
            var AnwendungDatentyp = new HashSet<int>(anwendung.MeineDatentypen.Select(d => d.Id));

            var viewModelAvailable = new List<Datentyp_Anwendung_VM>();
            var viewModelSelected = new List<Datentyp_Anwendung_VM>();

            foreach (var datentyp in alleDatentypen)
            {
                if (AnwendungDatentyp.Contains(datentyp.Id))
                {
                    viewModelSelected.Add(new Datentyp_Anwendung_VM
                    {
                        DatentypID = datentyp.Id,
                        DatentypName = datentyp.Name,
                        // Assigned = true
                    });
                }
                else
                {
                    viewModelAvailable.Add(new Datentyp_Anwendung_VM
                    {
                        DatentypID = datentyp.Id,
                        DatentypName = datentyp.Name,
                        // Assigned = false
                    });

                }
            }
    
                //ViewBag.Datentyp = viewModel;
                ViewBag.selectedDatentypen  = new MultiSelectList(viewModelSelected, "DatentypID", "DatentypName");
                ViewBag.alleDatentypen = new MultiSelectList(viewModelAvailable, "DatentypID", "DatentypName");

            }

        private void PopulateAssignedFeldData(Anwendung anwendung)
        {
            var alleFelder = db.Feld;
            var DatentypFeld = new HashSet<int>(anwendung.MeineFelder.Select(f => f.Id));
            var viewModel = new List<Anwendung_Feld_VM>();
            foreach (var feld in alleFelder)
            {
                viewModel.Add(new Anwendung_Feld_VM
                {
                    FeldID = feld.Id,
                    FeldName = feld.Name,
                    Assigned = DatentypFeld.Contains(feld.Id)
                });
            }
            ViewBag.Feld = viewModel;
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

        private void UpdateAnwendungFeld(string[] selectedFelder, Anwendung anwendungToUpdate)
        {
            if (selectedFelder == null)
            {
                anwendungToUpdate.MeineFelder = new List<Feld>();
                return;
            }

            var selectedFelderHS = new HashSet<string>(selectedFelder);
            var felderAnwendungen = new HashSet<int>
                (anwendungToUpdate.MeineFelder.Select(f => f.Id));

            foreach (var feld in db.Feld)
            {
                if (selectedFelderHS.Contains(feld.Id.ToString()))
                {
                    if (!felderAnwendungen.Contains(feld.Id))
                    {
                        anwendungToUpdate.MeineFelder.Add(feld);
                    }
                }
                else
                {
                    if (felderAnwendungen.Contains(feld.Id))
                    {
                        anwendungToUpdate.MeineFelder.Remove(feld);
                    }
                }
            }
        }
    }
}
