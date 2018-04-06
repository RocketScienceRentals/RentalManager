using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    public class AccountingController : Controller
    {

        private ApplicationDbContext accountContext;

        public AccountingController()
        {
            accountContext = new ApplicationDbContext();
        }

        // GET: Accounting
        // Displays the first index page
        public ActionResult Index()
        {
            ViewBag.Message = "Accounting page.";

           var aList = accountContext.Occupancies.ToList();
            //var myList = accountContext.Occupancies.ToList;
            // Where clause with lambda

            return View(aList);
        }
        // GET: Accounting//PaymentDetails
        // Displays the second page, payment details
        public ActionResult PaymentDetails()
        {
            ViewBag.Message = "Payment Details page.";

            return View();
        }
        // GET: Accounting/OrderDetails()
        // Display the final page for confirmation
        public ActionResult OrderDetails()
        {
            ViewBag.Message = "Order Details page.";

            var aList = accountContext.Occupancies.ToList();

            return View(aList);
        }
    }
}