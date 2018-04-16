using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.CustomFilters;
using RentalManagement.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
using System.Data.Entity;


namespace RentalManagement.Controllers
{
    [AuthLog(Roles = "Tenant")]
    public class AccountingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Accounting
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            // NEVER FORGET TO INCLUDE() TO LOAD UNDERLYING ENTITY 
            var currentUser = db.Users.Include("Tenant").SingleOrDefault(s => s.Id == currentUserId);
            // Gets the tenent entity for the current logged in user
            var tenant = currentUser.Tenant;

            //  query data that would include data from two or more of the above entities and is filtered by a Where clause.
            var getInfo = db.Occupancies.Include("ClientID").Include("AssetID").Include(e => e.AssetID.Address).Where(s => s.ClientID.ID == currentUser.Tenant.ID).ToList();
        
            return View(getInfo);
        }
        // GET: Accounting//PaymentDetails
        public ActionResult PaymentDetails()
        {
            ViewBag.Message = "Payment Details page.";

            return View();
        }
        // GET: Accounting/OrderDetails()
        public ActionResult OrderDetails()
        {
            ViewBag.Message = "Order Details page.";

            var currentUserId = User.Identity.GetUserId();
            // NEVER FORGET TO INCLUDE() TO LOAD UNDERLYING ENTITY DATA
            var currentUser = db.Users.Include("Tenant").SingleOrDefault(s => s.Id == currentUserId);
            // Gets the tenent entity for the current logged in user
            var tenant = currentUser.Tenant;

            //query data that would include data from two or more of the above entities and is filtered by a Where clause.
            var getInfo = db.Occupancies.Include("ClientID").Include("AssetID").Include(e => e.AssetID.Address).Where(s => s.ClientID.ID == currentUser.Tenant.ID).ToList();

            return View(getInfo);
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDetails([Bind(Include = "AssetID,ClientID,NegoationedOn,Details")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Tenant"))
                {
                    var currentUserId = User.Identity.GetUserId();  
                    // NEVER FORGET TO INCLUDE() TO LOAD UNDERLYING ENTITY DATA
                    var currentUser = db.Users.Include("Tenant").Include(e => e.Tenant.RequestedAssets).SingleOrDefault(s => s.Id == currentUserId);
                    var getAsset = currentUser.Tenant.RequestedAssets;

                    rental.AssetID = getAsset;
                    rental.ClientID = currentUser.Tenant;
                    rental.NegotiatedOn = DateTime.Now;
                    rental.Details = currentUser.Tenant.Details;

                    db.Rentals.Add(rental);
                    db.SaveChanges();
                    return Redirect(Url.Content("~/"));
                } 
            }
            return View(rental);
        }

    }
}
