using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalManagement.Controllers
{
    [Authorize(Roles = "Admin, Manager, Staff")]
    public class AccountingController : Controller
    {
        // GET: Accounting
        public ActionResult Index()
        {
            ViewBag.Message = "Accounting page.";

            return View();
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

            return View();
        }
    }
}