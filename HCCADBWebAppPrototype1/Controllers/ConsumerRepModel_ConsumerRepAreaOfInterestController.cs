using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.DAL;
using HCCADBWebAppPrototype1.ViewModels;

namespace HCCADBWebAppPrototype1.Controllers
{
    public class ConsumerRepModel_ConsumerRepAreaOfInterestController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/
        public async Task<ActionResult> Index()
        {
            var viewModel = new ConsumerRepAreaOfInterestIndexData();

            viewModel.ConsumerRepModel_ConsumerRepAreaOfInterests = db.ConsumerRep_ConsumerRepAreasOfInterest
                .Include(i => i.ConsumerRepAreaOfInterestModel)
                .Include(i => i.ConsumerRepModel);

            return View(viewModel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRep_ConsumerRepAreasOfInterest.FindAsync(id);
            if (consumerrepmodel_consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Create
        public ActionResult Create()
        {
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName");
            ViewBag.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName");
            return View();
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ConsumerRepModel_ConsumerRepAreaOfInterestModelID,ConsumerRepModelID,ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID")] ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel)
        {
            if (ModelState.IsValid)
            {
                db.ConsumerRep_ConsumerRepAreasOfInterest.Add(consumerrepmodel_consumerrepareaofinterestmodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID);
            ViewBag.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepAreaOfInterestModel_ConsumerRepAreaOfInterestModelID);
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRep_ConsumerRepAreasOfInterest.FindAsync(id);
            if (consumerrepmodel_consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID);
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsumerRepModel_ConsumerRepAreaOfInterestModelID,ConsumerRepModelID")] ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consumerrepmodel_consumerrepareaofinterestmodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepmodel_consumerrepareaofinterestmodel.ConsumerRepModelID);
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // GET: /ConsumerRepModel_ConsumerRepAreaOfInterest/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRep_ConsumerRepAreasOfInterest.FindAsync(id);
            if (consumerrepmodel_consumerrepareaofinterestmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel_consumerrepareaofinterestmodel);
        }

        // POST: /ConsumerRepModel_ConsumerRepAreaOfInterest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ConsumerRepModel_ConsumerRepAreaOfInterestModel consumerrepmodel_consumerrepareaofinterestmodel = await db.ConsumerRep_ConsumerRepAreasOfInterest.FindAsync(id);
            db.ConsumerRep_ConsumerRepAreasOfInterest.Remove(consumerrepmodel_consumerrepareaofinterestmodel);
            await db.SaveChangesAsync();
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
