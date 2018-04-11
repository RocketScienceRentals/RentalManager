using RentalManagement.CustomFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentalManagement.CustomFilters;

namespace RentalManagement.Controllers
{
    [AuthLog(Roles = "Tenant")]
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