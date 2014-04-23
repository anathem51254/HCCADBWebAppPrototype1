﻿using System;
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
    public class ConsumerRepCommitteeHistoryController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /ConsumerRepCommitteeHistory/
        public async Task<ActionResult> Index()
        {
            var consumerrepcommitteehistory = db.ConsumerRepCommitteeHistory.Include(c => c.CommitteeModel).Include(c => c.ConsumerRepModel);
            return View(await consumerrepcommitteehistory.ToListAsync());
        }

        // GET: /ConsumerRepCommitteeHistory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepCommitteeHistoryModel consumerrepcommitteehistorymodel = await db.ConsumerRepCommitteeHistory.FindAsync(id);
            if (consumerrepcommitteehistorymodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepcommitteehistorymodel);
        }

        // GET: /ConsumerRepCommitteeHistory/Create
        public ActionResult Create()
        {
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName");
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName");
            return View();
        }

        // POST: /ConsumerRepCommitteeHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ConsumerRepCommitteeHistoryModelID,CommitteeModelID,ConsumerRepModelID,PrepTime,Meetingtime,EndorsementStatus,EndorsementDate,EndorsementType")] ConsumerRepCommitteeHistoryModel consumerrepcommitteehistorymodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ConsumerRepCommitteeHistory.Add(consumerrepcommitteehistorymodel);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }catch(DataException /* dex */ )
            {
                // Log the error
                ModelState.AddModelError("", "Unable to save changes. Please Try Again.");
            }

            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName", consumerrepcommitteehistorymodel.CommitteeModelID);
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepcommitteehistorymodel.ConsumerRepModelID);
            return View(consumerrepcommitteehistorymodel);
        }

        // GET: /ConsumerRepCommitteeHistory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepCommitteeHistoryModel consumerrepcommitteehistorymodel = await db.ConsumerRepCommitteeHistory.FindAsync(id);
            if (consumerrepcommitteehistorymodel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName", consumerrepcommitteehistorymodel.CommitteeModelID);
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepcommitteehistorymodel.ConsumerRepModelID);
            return View(consumerrepcommitteehistorymodel);
        }

        // POST: /ConsumerRepCommitteeHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsumerRepCommitteeHistoryModelID,CommitteeModelID,ConsumerRepModelID,PrepTime,Meetingtime,EndorsementStatus,EndorsementDate,EndorsementType")] ConsumerRepCommitteeHistoryModel consumerrepcommitteehistorymodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(consumerrepcommitteehistorymodel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            { 
                // Log the error            
                ModelState.AddModelError("", "Unable to save changes. Please try again");
            }
            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName", consumerrepcommitteehistorymodel.CommitteeModelID);
            ViewBag.ConsumerRepModelID = new SelectList(db.ConsumerReps, "ConsumerRepModelID", "FirstName", consumerrepcommitteehistorymodel.ConsumerRepModelID);
            return View(consumerrepcommitteehistorymodel);
        }

        // GET: /ConsumerRepCommitteeHistory/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Please try agian.";
            }
            ConsumerRepCommitteeHistoryModel consumerrepcommitteehistorymodel = await db.ConsumerRepCommitteeHistory.FindAsync(id);
            if (consumerrepcommitteehistorymodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepcommitteehistorymodel);
        }

        // POST: /ConsumerRepCommitteeHistory/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ConsumerRepCommitteeHistoryModel consumerrepcommitteehistorymodel = await db.ConsumerRepCommitteeHistory.FindAsync(id);
                db.ConsumerRepCommitteeHistory.Remove(consumerrepcommitteehistorymodel);
                await db.SaveChangesAsync();
            }
            catch (DataException /* dex */)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
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
