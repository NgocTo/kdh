using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kdh.Models;

namespace kdh.Controllers
{
    public class LabResultsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: LabResults
        public ActionResult Index()
        {
            var results = db.Results.Include(r => r.LabReport).Include(r => r.TestType);
            return View(results.ToList());
        }

        // GET: LabResults/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: LabResults/Create
        public ActionResult Create()
        {
            ViewBag.ReportId = new SelectList(db.LabReports, "Id", "Status");
            ViewBag.TestId = new SelectList(db.TestTypes, "Id", "Category");
            return View();
        }

        // POST: LabResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReportId,TestId,Flag,Result1,Note")] Result result)
        {
            if (ModelState.IsValid)
            {
                result.Id = Guid.NewGuid();
                db.Results.Add(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportId = new SelectList(db.LabReports, "Id", "Status", result.ReportId);
            ViewBag.TestId = new SelectList(db.TestTypes, "Id", "Category", result.TestId);
            return View(result);
        }

        // GET: LabResults/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportId = new SelectList(db.LabReports, "Id", "Status", result.ReportId);
            ViewBag.TestId = new SelectList(db.TestTypes, "Id", "Category", result.TestId);
            return View(result);
        }

        // POST: LabResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReportId,TestId,Flag,Result1,Note")] Result result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportId = new SelectList(db.LabReports, "Id", "Status", result.ReportId);
            ViewBag.TestId = new SelectList(db.TestTypes, "Id", "Category", result.TestId);
            return View(result);
        }

        // GET: LabResults/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: LabResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
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
