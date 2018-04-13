using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;
using RentalManagement.CustomFilters;
using Microsoft.AspNet.Identity;

namespace RentalManagement.Controllers
{
    [AuthLog(Roles = "Admin, Manager, Staff, Tenant")]
    public class MaintenanceRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MaintenanceRequests
        public ActionResult Index()
        {
            return View(db.MaintenanceRequest.ToList());
        }

        // GET: MaintenanceRequests/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceRequest maintenanceRequest = db.MaintenanceRequest.Find(id);
            if (maintenanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin, Manager, Staff, Tenant")]
        public ActionResult Create([Bind(Include = "Asset,Subject,RequestDetail")] MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                // Tenant request creation
                if (User.IsInRole("Tenant"))
                {
                    maintenanceRequest.ID = Guid.NewGuid();
                    maintenanceRequest.CreatedDate = System.DateTime.Now;
                    maintenanceRequest.CompletedDate = null;

                    // Bind Asset and Tenant data from Occupancy for the currently logged in tenant user    
                    var currentUserId = User.Identity.GetUserId();
                    var currentUser = db.Users.Include("Tenant").SingleOrDefault(s => s.Id == currentUserId);
                    var tenant = currentUser.Tenant;

                    maintenanceRequest.Tenant = tenant;
                        
                    var asset = db.Occupancies
                                .Include("AssetID")
                                .Include("ClientID")
                                .Where(s => s.ClientID.ID == tenant.ID)
                                .First().AssetID;
                    maintenanceRequest.Asset = asset;
                    db.MaintenanceRequest.Add(maintenanceRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Admin, manager, staff request creation
                if (User.IsInRole("Admin") ||
                    User.IsInRole("Manager") ||
                    User.IsInRole("Staff"))
                {
                    maintenanceRequest.ID = Guid.NewGuid();
                    maintenanceRequest.CreatedDate = System.DateTime.Now;
                    maintenanceRequest.CompletedDate = null;
                    db.MaintenanceRequest.Add(maintenanceRequest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceRequest maintenanceRequest = db.MaintenanceRequest.Find(id);
            if (maintenanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreatedDate,CompletedDate,Subject,RequestDetail,StatusDetail,FixDetail,HoursSpent")] MaintenanceRequest maintenanceRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintenanceRequest);
        }

        // GET: MaintenanceRequests/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceRequest maintenanceRequest = db.MaintenanceRequest.Find(id);
            if (maintenanceRequest == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceRequest);
        }

        // POST: MaintenanceRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MaintenanceRequest maintenanceRequest = db.MaintenanceRequest.Find(id);
            db.MaintenanceRequest.Remove(maintenanceRequest);
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
