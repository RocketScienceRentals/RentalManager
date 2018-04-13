using RentalManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RentalManagement.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RentalList()
        {

            ApplicationDbContext db = new ApplicationDbContext();
            var asset = db.Assets.Include("Address");
            return View(asset.ToList());
        }

        public ActionResult Create(Guid? id)
        {
            ViewBag.Message = "This is your selected " + id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        public ActionResult ApplicationForm()
        {
            return View();
        }

    }
}