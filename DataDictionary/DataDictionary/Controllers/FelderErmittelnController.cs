using DataDictionary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DataDictionary.Controllers
{
    public class FelderErmittelnController : Controller
    {
        private EntityDataModel db = new EntityDataModel();

        // GET: FelderErmitteln
        public ActionResult Index()
        {
            return View();
        }

        // POST: FelderErmitteln/
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Index([Bind(Include = "ConnectionString")] Datenbank datenbank)
        //{
        //    return RedirectToAction("Details");
        //    if (ModelState.IsValid)
        //    {
                
        //        return RedirectToAction("Details", new { connectionString = datenbank.ConnectionString });

        //    }
        //}

        public ActionResult Details()
        {       
            return View();
        }
        public ActionResult Infos()
        {
            return View();
        }

        // POST: FelderErmitteln/Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(string connectionString)
        {
            ViewBag.ConStr = connectionString;
            return View();
        }
    }
}