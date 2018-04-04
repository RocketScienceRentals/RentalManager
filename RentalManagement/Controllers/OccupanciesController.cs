using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class OccupanciesController : Controller
    {
        private OccupancyDBContext db = new OccupancyDBContext();

        // GET: Occupancies
        public ActionResult Index()
        {
            return View(db.Occupancies.ToList());
        }

        // GET: Occupancies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occupancy occupancy = db.Occupancies.Find(id);
            if (occupancy == null)
            {
                return HttpNotFound();
            }
            return View(occupancy);
        }

        // GET: Occupancies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Occupancies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AssetID,ClientID,StartDate,EndDate,Detail")] Occupancy occupancy)
        {
            if (ModelState.IsValid)
            {
                db.Occupancies.Add(occupancy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(occupancy);
        }

        // GET: Occupancies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occupancy occupancy = db.Occupancies.Find(id);
            if (occupancy == null)
            {
                return HttpNotFound();
            }
            return View(occupancy);
        }

        // POST: Occupancies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AssetID,ClientID,StartDate,EndDate,Detail")] Occupancy occupancy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(occupancy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(occupancy);
        }

        // GET: Occupancies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Occupancy occupancy = db.Occupancies.Find(id);
            if (occupancy == null)
            {
                return HttpNotFound();
            }
            return View(occupancy);
        }

        // POST: Occupancies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Occupancy occupancy = db.Occupancies.Find(id);
            db.Occupancies.Remove(occupancy);
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
