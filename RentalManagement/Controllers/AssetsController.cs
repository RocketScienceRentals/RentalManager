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

namespace RentalManagement.Controllers
{
    [AuthLog(Roles = "Manager")]
    public class AssetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<Appliance> app;

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,AskingRent,Address")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.ID = Guid.NewGuid();
                db.Assets.Add(asset);
                db.SaveChanges();

                //using (var transaction = db.Database.BeginTransaction())
                //{
                //    try
                //    {

                //    } catch (Exception)
                //    {
                //        //If anything goes wrong rollback
                //        transaction.Rollback();
                //        ViewBag.ResultMessage = "Error occured, records rolledback.";
                //    }
                //}
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Assets
        public ActionResult Index()
        {
            return View(db.Assets.ToList());
        }

        // GET: Assets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Assets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name,Type,AskingRent")] Asset asset)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        asset.ID = Guid.NewGuid();
        //        db.Assets.Add(asset);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(asset);
        //}

        // GET: Assets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            List<SelectListItem> appliances = new List<SelectListItem>();

            app = db.Appliances.Where(m => m.BelongsToAsset == null).ToList();

            for(int i = 0; i < app.Count; i++)
            {
                appliances.Add(new SelectListItem() { Text = app[i].Name.ToString(),
                    Value = app[i].ID.ToString()
                });
            }

            ViewBag.AppList = new SelectList(appliances, "Value", "Text");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Type,AskingRent")] Asset asset
            , FormCollection fc)
        {
            Guid appId = new Guid(fc["appForm"].ToString());

            //var chosenApp = db.Appliances.ToList();
            //var chosenApp3 = db.Appliances.Find((Guid)appId);
            var chosenApp2 = db.Appliances.Where(m => m.ID == (Guid)appId).First();
            chosenApp2.BelongsToAsset = asset;
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Assets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
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
