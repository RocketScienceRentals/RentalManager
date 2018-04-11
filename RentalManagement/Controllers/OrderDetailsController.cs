using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentalManagement.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetails
        public ActionResult Index()
        {
            ViewBag.Message = "Order Details Page.";

            return View();
        }
    }
}