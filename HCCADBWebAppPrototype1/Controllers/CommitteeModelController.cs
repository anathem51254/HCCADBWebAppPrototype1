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

namespace HCCADBWebAppPrototype1.Controllers
{
    public class CommitteeModelController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /CommitteeModel/
        public async Task<ActionResult> Index()
        {
            return View(await db.Committees.ToListAsync());
        }

        // GET: /CommitteeModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // GET: /CommitteeModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /CommitteeModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CommitteeName,CurrentStatus")] CommitteeModel committeemodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Committees.Add(committeemodel);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save the chances. Please try again.");
            }

            return View(committeemodel);
        }

        // GET: /CommitteeModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // POST: /CommitteeModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CommitteeModelID,CommitteeName,CurrentStatus")] CommitteeModel committeemodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(committeemodel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save chanes. Please try again.");
            }

            return View(committeemodel);
        }

        // GET: /CommitteeModel/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Failed. Please try again";
            }
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            if (committeemodel == null)
            {
                return HttpNotFound();
            }
            return View(committeemodel);
        }

        // POST: /CommitteeModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                CommitteeModel committeemodel = await db.Committees.FindAsync(id);
                db.Committees.Remove(committeemodel);
                await db.SaveChangesAsync();
            }
            catch(DataException /* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChancesError = true });
            }
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
