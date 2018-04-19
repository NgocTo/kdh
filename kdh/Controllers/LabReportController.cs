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
    public class LabReportController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: LabReport
        public ActionResult Index(Guid id)
        {
            var labReports = db.LabReports.Where(q=>q.PatientId==id);
            return View(labReports.ToList());
        }

        // GET: LabReport/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabReport labReport = db.LabReports.Find(id);
            if (labReport == null)
            {
                return HttpNotFound();
            }
            return View(labReport);
        }

        // GET: LabReport/Create
        public ActionResult Create()
        {
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName");
            return View();
        }

        // POST: LabReport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,CollectionDate,IssueDate,Status,OrderedBy")] LabReport labReport)
        {
            if (ModelState.IsValid)
            {
                labReport.Id = Guid.NewGuid();
                db.LabReports.Add(labReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName", labReport.PatientId);
            return View(labReport);
        }

        // GET: LabReport/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabReport labReport = db.LabReports.Find(id);
            if (labReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName", labReport.PatientId);
            return View(labReport);
        }

        // POST: LabReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,CollectionDate,IssueDate,Status,OrderedBy")] LabReport labReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(labReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "FirstName", labReport.PatientId);
            return View(labReport);
        }

        // GET: LabReport/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LabReport labReport = db.LabReports.Find(id);
            if (labReport == null)
            {
                return HttpNotFound();
            }
            return View(labReport);
        }

        // POST: LabReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            LabReport labReport = db.LabReports.Find(id);
            db.LabReports.Remove(labReport);
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
