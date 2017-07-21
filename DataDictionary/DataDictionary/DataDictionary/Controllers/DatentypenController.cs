using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataDictionary.Models;
using DataDictionary.ViewModels;
using System.Data.Entity.Infrastructure;

namespace DataDictionary.Controllers
{
    public class DatentypenController : Controller
    {
        private EntityDataModel db = new EntityDataModel();

        // GET: Datentypen
        public ActionResult Index()
        {
            return View(db.Datentyp.ToList());
        }

        // GET: Datentypen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datentyp datentyp = db.Datentyp.Find(id);
            if (datentyp == null)
            {
                return HttpNotFound();
            }
            return View(datentyp);
        }

        // GET: Datentypen/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Datentypen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Beschreibung")] Datentyp datentyp)
        {
            if (ModelState.IsValid)
            {
                db.Datentyp.Add(datentyp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datentyp);
        }

        // GET: Datentypen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datentyp datentyp = db.Datentyp
               .Include(p => p.MeineAnwendungen)
               .Include(p => p.MeineFelder)
               .Where(i => i.Id == id)
               .Single();


            if (datentyp == null)
            {
                return HttpNotFound();
            }
            PopulateAssignedAnwendungData(datentyp);
            PopulateAssignedFeldData(datentyp);
            return View(datentyp);
        }

        // POST: Datentypen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedAnwendungen, string[] selectedFelder)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var datentypToUpdate = db.Datentyp
                .Include(p => p.MeineAnwendungen)
                .Include(p => p.MeineFelder)
                .Where(i => i.Id == id)
                .Single();

            if (TryUpdateModel(datentypToUpdate, "",
                   new string[] { "Id", "Name", "Beschreibung" }))
            {
                try
                {
                    UpdateDatentypAnwendung(selectedAnwendungen, datentypToUpdate);
                    UpdateDatentypFeld(selectedFelder, datentypToUpdate);
                    db.Entry(datentypToUpdate).State = EntityState.Modified;
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
            return View(datentypToUpdate);
        }

        // GET: Datentypen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Datentyp datentyp = db.Datentyp.Find(id);
            if (datentyp == null)
            {
                return HttpNotFound();
            }
            return View(datentyp);
        }

        // POST: Datentypen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Datentyp datentyp = db.Datentyp.Find(id);
            db.Datentyp.Remove(datentyp);
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

        private void PopulateAssignedAnwendungData(Datentyp datentyp)
        {
            var alleAnwendungen = db.Anwendung;
            var DatentypAnwendung = new HashSet<int>(datentyp.MeineAnwendungen.Select(b => b.Id));
            var viewModel = new List<Anwendung_Datentyp_VM>();
            foreach (var anwendung in alleAnwendungen)
            {
                viewModel.Add(new Anwendung_Datentyp_VM
                {
                    AnwendungID = anwendung.Id,
                    AnwendungName = anwendung.Name,
                    Assigned = DatentypAnwendung.Contains(anwendung.Id)
                });
            }
            ViewBag.Anwendung = viewModel;
        }

        private void PopulateAssignedFeldData(Datentyp datentyp)
        {
            var alleFelder = db.Feld;
            var DatentypFeld = new HashSet<int>(datentyp.MeineFelder.Select(b => b.Id));
            var viewModel = new List<Feld_Datentyp_VM>();
            foreach (var feld in alleFelder)
            {
                viewModel.Add(new Feld_Datentyp_VM
                {
                    FeldID = feld.Id,
                    FeldName = feld.Name,
                    Assigned = DatentypFeld.Contains(feld.Id)
                });
            }
            ViewBag.Feld = viewModel;
        }

        private void UpdateDatentypAnwendung(string[] selectedAnwendungen, Datentyp datentypToUpdate)
        {
            if (selectedAnwendungen == null)
            {
                datentypToUpdate.MeineAnwendungen = new List<Anwendung>();
                return;
            }

            var selectedAnwendungHS = new HashSet<string>(selectedAnwendungen);
            var anwendungenDatentypen = new HashSet<int>
                (datentypToUpdate.MeineAnwendungen.Select(d => d.Id));

            foreach (var anwendung in db.Anwendung)
            {
                if (selectedAnwendungHS.Contains(anwendung.Id.ToString()))
                {
                    if (!anwendungenDatentypen.Contains(anwendung.Id))
                    {
                        datentypToUpdate.MeineAnwendungen.Add(anwendung);
                    }
                }
                else
                {
                    if (anwendungenDatentypen.Contains(anwendung.Id))
                    {
                        datentypToUpdate.MeineAnwendungen.Remove(anwendung);
                    }
                }
            }
        }

        private void UpdateDatentypFeld(string[] selectedFelder, Datentyp datentypToUpdate)
        {
            if (selectedFelder == null)
            {
                datentypToUpdate.MeineFelder = new List<Feld>();
                return;
            }

            var selectedFelderHS = new HashSet<string>(selectedFelder);
            var felderDatentypen = new HashSet<int>
                (datentypToUpdate.MeineFelder.Select(d => d.Id));

            foreach (var feld in db.Feld)
            {
                if (selectedFelderHS.Contains(feld.Id.ToString()))
                {
                    if (!felderDatentypen.Contains(feld.Id))
                    {
                        datentypToUpdate.MeineFelder.Add(feld);
                    }
                }
                else
                {
                    if (felderDatentypen.Contains(feld.Id))
                    {
                        datentypToUpdate.MeineFelder.Remove(feld);
                    }
                }
            }
        }
    }
}
