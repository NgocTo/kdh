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
using kdh.Utils;

namespace kdh.Controllers
{
    [Authorize(Roles = "admin")]
    public class LabResultsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: LabResults/Create
        public ActionResult Create(Guid id) // this is lab report id
        {

            try
            {
                LabReport labReport = db.LabReports.SingleOrDefault(q => q.Id == id);
                var categories = db.TestTypes
                    .Select(q => new TestTypeVM { Id = q.Id, Category = q.Category })
                    .ToList()
                    .DistinctBy(q => q.Category)
                    .ToList();
                ReportResultVM reportResultVM = new ReportResultVM
                {
                    LabReport = labReport
                };

                ViewBag.Category = new SelectList(categories, "Category", "Category");
                ViewBag.TestItem = new SelectList(db.TestTypes, "Id", "TestItem");
                ViewBag.DefaultCategory = categories.First().Category;

                return View(reportResultVM);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: LabResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportResultVM reportResultVM, bool add_result)
        {

            try
            {
                if (ModelState.IsValid && reportResultVM.TestId != Guid.Empty)
                {

                    TestType testType = db.TestTypes
                        .Single(q => q.Id == reportResultVM.TestId);

                    string flag = "Normal";
                    if (reportResultVM.Result1 >= testType.MaxReference)
                    {
                        flag = "High";
                    }
                    else if (reportResultVM.Result1 <= testType.MinReference)
                    {
                        flag = "Low";
                    }

                    Result result = new Result
                    {
                        Id = Guid.NewGuid(),
                        Note = reportResultVM.Note,
                        Result1 = reportResultVM.Result1,
                        ReportId = reportResultVM.Id,
                        TestId = reportResultVM.TestId,
                        Flag = flag
                    };

                    db.Results.Add(result);
                    db.SaveChanges();


                    if (add_result)
                    {
                        return RedirectToAction("Create", new { Id = result.ReportId });
                    }
                    return RedirectToAction("Details", "LabReport", new { Id = result.ReportId });
                }

                ViewBag.ReportId = new SelectList(db.LabReports, "Id", "Status", reportResultVM.ReportId);
                ViewBag.TestId = new SelectList(db.TestTypes, "Id", "Category", reportResultVM.TestId);

                var categories = db.TestTypes
                    .Select(q => new TestTypeVM { Id = q.Id, Category = q.Category })
                    .ToList()
                    .DistinctBy(q => q.Category)
                    .ToList();
                ViewBag.Category = new SelectList(categories, "Category", "Category");
                ViewBag.TestItem = new SelectList(db.TestTypes, "Id", "TestItem");

                reportResultVM.LabReport = db.LabReports.First(q => q.Id == reportResultVM.Id);
                ViewBag.DropDownError = "Tested Item is a required field";

                return View(reportResultVM);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // GET: LabResults/Edit/5
        public ActionResult Edit(Guid? id)
        {

            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Result result = db.Results.Find(id);
                var categories = db.TestTypes
                    .Select(q => new TestTypeVM { Id = q.Id, Category = q.Category })
                    .ToList()
                    .DistinctBy(q => q.Category)
                    .ToList();

                ViewBag.Category = new SelectList(categories, "Category", "Category", result.TestType.Category);
                ViewBag.TestItem = new SelectList(db.TestTypes, "Id", "TestItem", result.TestType.Id);


                ReportResultVM reportResultVM = new ReportResultVM
                {
                    Id = result.Id,
                    Note = result.Note,
                    Result1 = result.Result1,
                    TestType = result.TestType,
                    LabReport = result.LabReport,
                    ReportId = result.ReportId,
                    TestId = result.TestId
                };

                if (result == null)
                {
                    return HttpNotFound();
                }
                return View(reportResultVM);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // POST: LabResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReportResultVM reportResultVM, bool add_result)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    TestType testType = db.TestTypes
                        .Single(q => q.Id == reportResultVM.TestId);

                    string flag = "Normal";
                    if (reportResultVM.Result1 >= testType.MaxReference)
                    {
                        flag = "High";
                    }
                    else if (reportResultVM.Result1 <= testType.MinReference)
                    {
                        flag = "Low";
                    }


                    Result result = new Result
                    {
                        Id = reportResultVM.Id,
                        TestType = reportResultVM.TestType,
                        TestId = reportResultVM.TestId,
                        ReportId = reportResultVM.ReportId,
                        LabReport = reportResultVM.LabReport,
                        Note = reportResultVM.Note,
                        Result1 = reportResultVM.Result1,
                        Flag = flag
                    };

                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();

                    if (add_result)
                    {
                        return RedirectToAction("Create", new { Id = result.ReportId });
                    }

                    return RedirectToAction("Details", "LabReport", new { Id = result.ReportId });
                }
                return View(reportResultVM);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // GET: LabResults/Delete/5
        public ActionResult Delete(Guid? id)
        {
            try
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
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // POST: LabResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {

            try
            {
                Result result = db.Results.Find(id);
                db.Results.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Details", "LabReport", new { Id = result.ReportId });

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

        public ActionResult ajax_getTestItemByCategory(string categoryName)
        {

            try
            {
                var result = db.TestTypes
                    .Where(q => q.Category == categoryName)
                    .Select(q => new TestTypeVM { Id = q.Id, TestItem = q.TestItem })
                    .ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }
    }
}
