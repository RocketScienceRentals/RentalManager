using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentalManagement.CustomFilters;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    [AuthLog(Roles = "Tenant")]
    public class FullAddressesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FullAddresses
        public ActionResult Index()
        {
            return View(db.FullAddresses.ToList());
        }

        // GET: FullAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullAddress fullAddress = db.FullAddresses.Find(id);
            if (fullAddress == null)
            {
                return HttpNotFound();
            }
            return View(fullAddress);
        }

        // GET: FullAddresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FullAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MyProperty,StreetAddress,Province,Country,PostalCode")] FullAddress fullAddress)
        {
            if (ModelState.IsValid)
            {
                db.FullAddresses.Add(fullAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fullAddress);
        }

        // GET: FullAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullAddress fullAddress = db.FullAddresses.Find(id);
            if (fullAddress == null)
            {
                return HttpNotFound();
            }
            return View(fullAddress);
        }

        // POST: FullAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MyProperty,StreetAddress,Province,Country,PostalCode")] FullAddress fullAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fullAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fullAddress);
        }

        // GET: FullAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FullAddress fullAddress = db.FullAddresses.Find(id);
            if (fullAddress == null)
            {
                return HttpNotFound();
            }
            return View(fullAddress);
        }

        // POST: FullAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FullAddress fullAddress = db.FullAddresses.Find(id);
            db.FullAddresses.Remove(fullAddress);
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
