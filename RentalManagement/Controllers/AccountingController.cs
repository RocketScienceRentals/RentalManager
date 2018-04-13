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

            //rental.ClientID = currentUser.Tenant;
            // stores the assetID to the rental.asset
            //rental.AssetID = ctx.Occupancies.Include("AssetID.ClientID").Where(s => s.ClientID.ID == currentUser.Tenant.ID).ToList().First().AssetID;
            var test = db.Occupancies.Include("ClientID").Where(s => s.ClientID.ID == currentUser.Tenant.ID).ToList();
            return View(test);
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

            //rental.ClientID = currentUser.Tenant;
            // stores the assetID to the rental.asset
            //rental.AssetID = ctx.Occupancies.Include("AssetID.ClientID").Where(s => s.ClientID.ID == currentUser.Tenant.ID).ToList().First().AssetID;
            var test = db.Occupancies.Include("ClientID").Where(s => s.ClientID.ID == currentUser.Tenant.ID).ToList();

            return View(test);
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderDetails([Bind(Include = "ID,AssetID,ClientID,NegoationedOn,Details")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Tenant"))
                {
                    var currentUserId = User.Identity.GetUserId();
                    // NEVER FORGET TO INCLUDE() TO LOAD UNDERLYING ENTITY DATA
                    var currentUser = db.Users.Include("Tenant").SingleOrDefault(s => s.Id == currentUserId);

                    rental.ID = 1;
                    rental.ClientID = currentUser.Tenant;
                    rental.AssetID = null;
                    rental.NegotiatedOn = DateTime.Now;
                    rental.Details = "Test 123";

                    db.Rentals.Add(rental);
                    db.SaveChanges();
                    return Redirect(Url.Content("~/"));
                } 
            }
            return View(rental);
        }
    }
}
