using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentalManagement.Models;

namespace RentalManagement.Controllers
{
    [Authorize(Roles = "Manager")]
    public class AssetsController : Controller
    {
        private static string NULL = "null";

        private ApplicationDbContext db = new ApplicationDbContext();

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
            Asset asset = db.Assets.Include("Address").SingleOrDefault(a => a.ID == id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Assets/Create
        public ActionResult Create()
        {
            List<Appliance> availableAppliances = db.Appliances.Where(a => a.BelongsToAsset == null).ToList();
            var selectableAppliances = new Dictionary<string, List<SelectListItem>>();
            var selectListAppliances = new Dictionary<string, SelectList>();

            // create selectableAppliances from ApplianceType enum
            foreach (ApplianceType applianceType in Enum.GetValues(typeof(ApplianceType)))
            {
                List<SelectListItem> newList = new List<SelectListItem>();
                selectableAppliances.Add(applianceType.ToString(), newList);
                newList.Add(
                    new SelectListItem()
                    {
                        Text = "Nothing",
                        Value = NULL
                    }
                );
            }

            // populate selectableAppliances
            foreach (Appliance a in availableAppliances)
            {
                List<SelectListItem> currentList;
                if (selectableAppliances.TryGetValue(a.Type.ToString(), out currentList))
                {
                    currentList.Add(
                        new SelectListItem()
                        {
                            Text = a.Name.ToString(),
                            Value = a.ID.ToString()
                        }
                    );
                }
            }

            // populate selectListAppliances from selectableAppliances
            foreach (KeyValuePair<string, List<SelectListItem>> e in selectableAppliances)
            {
                selectListAppliances.Add(e.Key, new SelectList(e.Value, "Value", "Text"));
            }

            ViewBag.AppLists = selectListAppliances;

            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Type,AskingRent,Address")] Asset asset)
        {
            List<Appliance> appliances = new List<Appliance>();

            foreach (ApplianceType applianceType in Enum.GetValues(typeof(ApplianceType)))
            {
                if (Request[applianceType.ToString()] == NULL)
                    continue;

                Guid id = Guid.Parse(Request[applianceType.ToString()]);
                Appliance app = db.Appliances.Find(id);
                if (app != null)
                    appliances.Add(app);
            }
    
            if (ModelState.IsValid)
            {
                asset.ID = Guid.NewGuid();
                asset.IsOccuppied = false;
                asset.Appliances = appliances;
                db.Assets.Add(asset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asset);
        }

        // GET: Assets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Include("Address").Include("Appliances").SingleOrDefault(a => a.ID == id);
            if (asset == null)
            {
                return HttpNotFound();
            }

            List<Appliance> availableAppliances = db.Appliances.Where(a => a.BelongsToAsset == null).ToList();
            var selectableAppliances = new Dictionary<string, List<SelectListItem>>();
            var selectListAppliances = new Dictionary<string, SelectList>();

            // create selectableAppliances from ApplianceType enum
            foreach (ApplianceType applianceType in Enum.GetValues(typeof(ApplianceType)))
            {
                List<SelectListItem> newList = new List<SelectListItem>();
                selectableAppliances.Add(applianceType.ToString(), newList);
                newList.Add(
                    new SelectListItem()
                    {
                        Text = "Nothing",
                        Value = NULL
                    }
                );
            }

            // add currently associated appliances to lists
            foreach (Appliance a in asset.Appliances)
            {
                selectableAppliances[a.Type.ToString()].Add(
                    new SelectListItem()
                    {
                        Text = a.Name,
                        Value = a.ID.ToString()
                    }
                );
            }

            // populate selectableAppliances
            foreach (Appliance a in availableAppliances)
            {
                List<SelectListItem> currentList;
                if (selectableAppliances.TryGetValue(a.Type.ToString(), out currentList))
                {
                    currentList.Add(
                        new SelectListItem()
                        {
                            Text = a.Name.ToString(),
                            Value = a.ID.ToString()
                        }
                    );
                }
            }

            // populate selectListAppliances from selectableAppliances
            foreach (KeyValuePair<string, List<SelectListItem>> e in selectableAppliances)
            {
                Appliance def = asset.Appliances.SingleOrDefault(a => a.Type.ToString() == e.Key);
                string defaultValue = (def == null) ? NULL : def.ID.ToString();
                selectListAppliances.Add(e.Key, new SelectList(e.Value, "Value", "Text", defaultValue));
            }

            ViewBag.AppLists = selectListAppliances;

            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IsOccuppied,Name,Type,AskingRent,Address")] Asset asset)
        {
            List<Appliance> appliancesOld = db.Appliances.Include("BelongsToAsset").Where(a => a.BelongsToAsset.ID == asset.ID).ToList();
            List<Appliance> appliancesSelected = new List<Appliance>();
            Asset thisAsset = db.Assets.Find(asset.ID);

            // update all properties
            thisAsset.Name = asset.Name;
            thisAsset.Type = asset.Type;
            thisAsset.Address = asset.Address;
            thisAsset.AskingRent = asset.AskingRent;

            // add newly selected appliances
            foreach (ApplianceType applianceType in Enum.GetValues(typeof(ApplianceType)))
            {
                Guid appID;
                Appliance selectedApp;

                // skip if appliance type was not selected
                if (Request[applianceType.ToString()] == NULL)
                    continue;

                appID = Guid.Parse(Request[applianceType.ToString()]);
                selectedApp = db.Appliances.Find(appID);

                // skip if selected appliance non-existent in database
                if (selectedApp == null)
                    continue;

                // add to selected appliances
                // used for removing unselected appliances
                appliancesSelected.Add(selectedApp);

                // add appliance
                if (!appliancesOld.Contains(selectedApp))
                    selectedApp.BelongsToAsset = thisAsset;
                else
                    appliancesOld.Remove(selectedApp);
            }

            // remove unselected appliances
            foreach (Appliance oldApp in appliancesOld)
            {
                oldApp.BelongsToAsset = null;
            }

            if (ModelState.IsValid)
            {
                //db.Entry(asset).State = EntityState.Modified;
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
            Asset asset = db.Assets.Include("Address").Include("Appliances").SingleOrDefault(a => a.ID == id);
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
            Asset asset = db.Assets.Include("Appliances").Single(a => a.ID == id);

            foreach (Appliance app in asset.Appliances)
            {
                app.BelongsToAsset = null;
            }

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
