using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kdh.Models;
using kdh.ViewModels;

namespace kdh.Controllers
{
    [Authorize(Roles = "admin")]
    public class LabReportController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: LabReport
        public ActionResult Index(Guid id)
        {

            try
            {
                var patient = db.Patients.SingleOrDefault(q => q.Id == id);
                var labReports = patient.LabReports;

                ViewBag.PatientId = id;
                ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";

                return View(labReports);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: LabReport/Details/5
        public ActionResult Details(Guid? id)
        {

            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                LabReport labReport = db.LabReports.Find(id);
                ViewBag.PatientId = labReport.PatientId;

                if (labReport == null)
                {
                    return HttpNotFound();
                }
                return View(labReport);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // GET: LabReport/Create
        public ActionResult Create(Guid id)
        {
            try
            {
                var patient = db.Patients
                    .SingleOrDefault(q => q.Id == id);

                ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";
                ViewBag.PatientId = patient.Id;
                return View();

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // POST: LabReport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guid id, LabReportVM labReportVM, bool add_result) // this id is PatientId
        {

            try
            {
                if (ModelState.IsValid)
                {
                    LabReport labReport = new LabReport()
                    {
                        PatientId = id,
                        Id = Guid.NewGuid(),
                        CollectionDate = labReportVM.CollectionDate,
                        IssueDate = DateTime.Now,
                        OrderedBy = labReportVM.OrderedBy,
                        Status = labReportVM.Status
                    };
                    db.LabReports.Add(labReport);
                    db.SaveChanges();

                    if (add_result)
                    {
                        return RedirectToAction("Create", "LabResults", new { Id = labReport.Id });
                    }
                    else
                    {
                        return RedirectToAction("Index", "LabReport", new { Id = labReport.PatientId });
                    }

                }

                return View(labReportVM);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: LabReport/Edit/5
        public ActionResult Edit(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                LabReport labReport = db.LabReports.Find(id);
                LabReportVM labReportVM = new LabReportVM
                {
                    Id = labReport.Id,
                    CollectionDate = labReport.CollectionDate,
                    IssueDate = labReport.IssueDate,
                    OrderedBy = labReport.OrderedBy,
                    PatientId = labReport.PatientId,
                    Status = labReport.Status
                };

                var patient = labReport.Patient;
                ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";
                ViewBag.PatientId = patient.Id;

                if (labReport == null)
                {
                    return HttpNotFound();
                }

                return View(labReportVM);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: LabReport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LabReportVM labReportVM, bool add_result)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patientId = labReportVM.PatientId;

                    LabReport labReport = new LabReport
                    {
                        OrderedBy = labReportVM.OrderedBy,
                        Status = labReportVM.Status,
                        CollectionDate = labReportVM.CollectionDate,
                        PatientId = labReportVM.PatientId,
                        IssueDate = labReportVM.IssueDate,
                        Id = labReportVM.Id
                    };

                    db.Entry(labReport).State = EntityState.Modified;
                    db.SaveChanges();

                    if (add_result)
                    {
                        return RedirectToAction("Create", "LabResults", new { Id = labReport.Id });
                    }
                    return RedirectToAction("Index", "LabReport", new { Id = labReport.PatientId });
                }

                return View(labReportVM);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: LabReport/Delete/5
        public ActionResult Delete(Guid? id)
        {

            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                LabReport labReport = db.LabReports.Find(id);
                ViewBag.PatientId = labReport.PatientId;

                if (labReport == null)
                {
                    return HttpNotFound();
                }
                return View(labReport);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: LabReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                LabReport labReport = db.LabReports.Find(id);
                List<Result> results = db.Results.Where(q => q.LabReport.Id == id).ToList();
                Guid patientId = labReport.PatientId;

                db.Results.RemoveRange(results);
                db.LabReports.Remove(labReport);
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = patientId });

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

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
