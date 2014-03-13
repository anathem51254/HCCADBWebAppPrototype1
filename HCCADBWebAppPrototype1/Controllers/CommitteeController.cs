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

namespace HCCADBWebAppPrototype1.Controllers
{
    public class CommitteeController : Controller
    {
        private HCCADbContext db = new HCCADbContext();

        // GET: /Committee/
        public async Task<ActionResult> Index(string committeeStatus, string committeeArea, string searchString)
        {
            
            var StatusList = new List<string>();

            StatusList.Add("Active");
            StatusList.Add("InActive");

            ViewBag.committeeStatus = new SelectList(StatusList);

            var AreaList = new List<string>();

            var AreaQry = from area in db.Committees
                          orderby area.HealthArea
                          select area.HealthArea;

            AreaList.AddRange(AreaQry.Distinct());
            ViewBag.committeeArea = new SelectList(AreaList);

            var Committees = from com in db.Committees
                             select com;

            if(!String.IsNullOrEmpty(searchString))
            {
                Committees = Committees.Where(s => s.CommitteeName.Contains(searchString));
            }

            if(!String.IsNullOrEmpty(committeeArea))
            {
                Committees = Committees.Where(x => x.HealthArea.Contains(committeeArea));
            }

            if(!String.IsNullOrEmpty(committeeStatus))
            {
                if (committeeStatus == "Active")
                {
                    Committees = Committees.Where(x => x.Status.Equals(1));
                }
                else if (committeeStatus == "InActive")
                {
                    Committees = Committees.Where(x => x.Status.Equals(0));
                }
            }

            return View(Committees);
        }

        // GET: /Committee/Details/5
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

        // GET: /Committee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Committee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="CommitteeId,CommitteeName,HealthArea,Status")] CommitteeModel committeemodel)
        {
            if (ModelState.IsValid)
            {
                db.Committees.Add(committeemodel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(committeemodel);
        }

        // GET: /Committee/Edit/5
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

        // POST: /Committee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="CommitteeId,CommitteeName,HealthArea,Status")] CommitteeModel committeemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(committeemodel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(committeemodel);
        }

        // GET: /Committee/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: /Committee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CommitteeModel committeemodel = await db.Committees.FindAsync(id);
            db.Committees.Remove(committeemodel);
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
