using HCCADBWebAppPrototype1.BusinessLogic;
using HCCADBWebAppPrototype1.DAL;
using HCCADBWebAppPrototype1.Models;
using HCCADBWebAppPrototype1.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace HCCADBWebAppPrototype1.Controllers
{
    public class CommitteeModelController : Controller
    {
        private HCCADatabaseContext db = new HCCADatabaseContext();

        private Util_CommitteeModelController utils_Committees = new Util_CommitteeModelController();

        // GET: /CommitteeModel/
        public async Task<ActionResult> Index(string searchByStatus)
        {
            if (String.IsNullOrEmpty(searchByStatus))
            {
                searchByStatus = "Current"; 
            }

            List<string> CommitteeStatuses = new List<string>();
            CommitteeStatuses.Add("Current");
            CommitteeStatuses.Add("Past");
            CommitteeStatuses.Add("All");
            ViewBag.CommitteeStatuses = new SelectList(CommitteeStatuses);

            DateTime currentDate = DateTime.Today;

            DateTime Start = new DateTime(currentDate.Year, 1, 1);
            DateTime MiddleStart = new DateTime(currentDate.Year, 6, 30);
            DateTime MiddleEnd = new DateTime(currentDate.Year, 7, 1);
            DateTime End = new DateTime(currentDate.Year, 12, 31);

            if (searchByStatus == "Current")
            {
                if (currentDate >= Start && currentDate <= MiddleStart)
                { 
                    var currentCommittees = from com in db.Committees
                        from joinComHist in db.ConsumerRepCommitteeHistory
                        where com.CommitteeModelID == joinComHist.CommitteeModelID
                        where joinComHist.EndorsementDate < MiddleEnd && (joinComHist.EndorsementDate > Start || joinComHist.EndorsementDate == Start)
                        select com;

                    return View(await currentCommittees.ToListAsync());
                }
                else if (currentDate >= MiddleEnd && currentDate <= End)
                { 
                    var currentCommittees = from com in db.Committees
                            from joinComHist in db.ConsumerRepCommitteeHistory
                            where com.CommitteeModelID == joinComHist.CommitteeModelID
                            where joinComHist.EndorsementDate > MiddleStart && (joinComHist.EndorsementDate < End || joinComHist.EndorsementDate == End)
                            select com;

                    return View(await currentCommittees.ToListAsync());
                }
            }
            else if (searchByStatus == "Past")
            { 
                if (currentDate >= Start && currentDate <= MiddleStart)
                { 
                    var currentCommittees = from com in db.Committees
                        from joinComHist in db.ConsumerRepCommitteeHistory
                        where com.CommitteeModelID == joinComHist.CommitteeModelID
                        where joinComHist.EndorsementDate < Start 
                        select com;

                    return View(await currentCommittees.ToListAsync());
                }
                else if (currentDate >= MiddleEnd && currentDate <= End)
                { 
                    var currentCommittees = from com in db.Committees
                            from joinComHist in db.ConsumerRepCommitteeHistory
                            where com.CommitteeModelID == joinComHist.CommitteeModelID
                            where joinComHist.EndorsementDate < MiddleStart 
                            select com;

                    return View(await currentCommittees.ToListAsync());
                }

            }
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
        public async Task<ActionResult> Create([Bind(Include="CommitteeName")] CommitteeModel committeemodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    committeemodel.CurrentStatus = CurrentStatus.InActive;
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
