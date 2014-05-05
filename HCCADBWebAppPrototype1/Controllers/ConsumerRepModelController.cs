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
    public class ConsumerRepModelController : Controller
    {
        private Util_ConsumerRepModelController utils_ConRep = new Util_ConsumerRepModelController();
        private HCCADatabaseContext db = new HCCADatabaseContext();

        // GET: /ConsumerRepModel/
        public async Task<ActionResult> Index(string sortOrder, string searchByInterest, string searchByStatus, string searchByName)
        {
            List<string> MemberStatusTypes = new List<string>();
            MemberStatusTypes.Add("All");
            MemberStatusTypes.Add("Active");
            MemberStatusTypes.Add("InActive");
            ViewBag.MemberStatusTypes = new SelectList(MemberStatusTypes);

            List<string> AreasOfInterest = new List<string>();
            AreasOfInterest.Add("All");
            foreach (var interest in db.ConsumerRepAreasOfInterest)
            {
                AreasOfInterest.Add(interest.AreaOfInterestName);
            }
            ViewBag.AreasOfInterest = new SelectList(AreasOfInterest);

            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MemberStatusSortParam = sortOrder == "MemberStatus" ? "memberstatus_desc" : "MemberStatus";

            var consumerReps = from cr in db.ConsumerReps
                               select cr;

            var consumerRepsByName = from cr in db.ConsumerReps
                                     where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                     select cr;

            var consumerRepsByInterest = from cr in db.ConsumerReps
                                       from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                       where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                       where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                       select cr;

            var consumerRepsByInterestByName = from cr in db.ConsumerReps
                                             from joinCrInte in db.ConsumerRepModel_ConsumerRepAreasOfInterestModel
                                             where cr.ConsumerRepModelID == joinCrInte.ConsumerRepModelID
                                             where joinCrInte.ConsumerRepAreaOfInterestModel.AreaOfInterestName == searchByInterest
                                             where cr.FirstName.ToUpper().Contains(searchByName.ToUpper()) || cr.LastName.ToUpper().Contains(searchByName.ToUpper())
                                             select cr;

            #region Search Logic
            if (!String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && !String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All")
            {
                MemberStatus _searchByStatus = MemberStatus.Active;

                if (searchByStatus == "Active")
                {
                    _searchByStatus = MemberStatus.Active;
                    consumerRepsByInterestByName = consumerRepsByInterestByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
                else if (searchByStatus == "InActive")
                {
                    _searchByStatus = MemberStatus.InActive;
                    consumerRepsByInterestByName = consumerRepsByInterestByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
            }
            else if (!String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && !String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All")
            {
                MemberStatus _searchByStatus = MemberStatus.Active;

                if (searchByStatus == "Active")
                {
                    _searchByStatus = MemberStatus.Active;
                    consumerRepsByInterest = consumerRepsByInterest.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
                else if (searchByStatus == "InActive")
                {
                    _searchByStatus = MemberStatus.InActive;
                    consumerRepsByInterest = consumerRepsByInterest.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
            }
            else if (!String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
            {
                MemberStatus _searchByStatus = MemberStatus.Active;

                if (searchByStatus == "Active")
                {
                    _searchByStatus = MemberStatus.Active;
                    consumerRepsByName = consumerRepsByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
                else if (searchByStatus == "InActive")
                {
                    _searchByStatus = MemberStatus.InActive;
                    consumerRepsByName = consumerRepsByName.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
            }
            else
            { 
                MemberStatus _searchByStatus = MemberStatus.Active;

                if (searchByStatus == "Active")
                {
                    _searchByStatus = MemberStatus.Active;
                    consumerReps = consumerReps.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
                else if (searchByStatus == "InActive")
                {
                    _searchByStatus = MemberStatus.InActive;
                    consumerReps = consumerReps.Where(cr => cr.MemberStatus.Value == _searchByStatus);
                }
            }
            #endregion

            if (!String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && !String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All")
            {
                consumerRepsByInterestByName = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByInterestByName);
                return View(await consumerRepsByInterestByName.ToListAsync());
            }
            else if (!String.IsNullOrEmpty(searchByStatus) && String.IsNullOrEmpty(searchByName) && !String.IsNullOrEmpty(searchByInterest) && searchByInterest != "All")
            {
                consumerRepsByInterest = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByInterest);
                return View(await consumerRepsByInterest.ToListAsync());
            }
            else if (!String.IsNullOrEmpty(searchByStatus) && !String.IsNullOrEmpty(searchByName) && (String.IsNullOrEmpty(searchByInterest) || searchByInterest == "All"))
            { 
                consumerRepsByName = utils_ConRep.ComsumerReps_SortIndex(sortOrder, consumerRepsByName);
                return View(await consumerRepsByName.ToListAsync());
            }
            else
                return View(await consumerReps.ToListAsync());
        }

        // GET: /ConsumerRepModel/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
            if (consumerrepmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel);
        }

        // GET: /ConsumerRepModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ConsumerRepModel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="FirstName,LastName,Address,PhoneNumber,Email,MemberStatus,DateTrained")] ConsumerRepModel consumerrepmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ConsumerReps.Add(consumerrepmodel);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // Log the error here with dex var
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            return View(consumerrepmodel);
        }

        // GET: /ConsumerRepModel/AddToInterest/id
        public async Task<ActionResult> AddToInterest(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vm = new AddToInterestConsumerRepViewModel
            {
                ConsumerRepAreaOfInterestModel = new ConsumerRepAreaOfInterestModel(),
                ConsumerRepModel = db.ConsumerReps.Find(id),
                ConsumerRepAreaOfInterests = new SelectList(db.ConsumerRepAreasOfInterest, "ConsumerRepAreaOfInterestModelID", "AreaOfInterestName")
            };

            if (vm.ConsumerRepModel == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: /ConsumerRepModel/AddToInterest
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToInterest(AddToInterestConsumerRepViewModel vm)
        {
            /*vm.ConsumerRepModel.ConsumerRepAreasOfInterestModels.Add(vm.ConsumerRepAreaOfInterestModel);

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vm.ConsumerRepModel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Details/" + vm.ConsumerRepModel.ConsumerRepModelID, "ConsumerRepModel");
                }
            }
            catch (DataException /* dex *///)
            //{
                // Log the error here with dex var
                /*
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }

            ViewBag.CommitteeModelID = new SelectList(db.Committees, "CommitteeModelID", "CommitteeName");
            ViewBag.ConsumerRepModelID = vm.ConsumerRepModel.ConsumerRepModelID;
            */
            return View();
        } 


        // GET: /ConsumerRepModel/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
            if (consumerrepmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel);
        }

        // POST: /ConsumerRepModel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ConsumerRepModelID,FirstName,LastName,Address,PhoneNumber,Email,MemberStatus,DateTrained")] ConsumerRepModel consumerrepmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(consumerrepmodel).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // Log the error here with dex var
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            return View(consumerrepmodel);
        }

        // GET: /ConsumerRepModel/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Please Try Again";
            }
            ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
            if (consumerrepmodel == null)
            {
                return HttpNotFound();
            }
            return View(consumerrepmodel);
        }

        // POST: /ConsumerRepModel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ConsumerRepModel consumerrepmodel = await db.ConsumerReps.FindAsync(id);
                db.ConsumerReps.Remove(consumerrepmodel);
                await db.SaveChangesAsync();
            }
            catch (DataException/* dex */)
            {
                //Log the error 
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
