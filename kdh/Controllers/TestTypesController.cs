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
    public class TestTypesController : Controller
    {

        private HospitalContext db = new HospitalContext();

        // GET: TestTypes
        public ActionResult Index()
        {
            try
            {
                return View(db.TestTypes.ToList());

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: TestTypes/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: TestTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestTypeVM testTypeVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TestType testType = new TestType
                    {
                        Id = Guid.NewGuid(),
                        TestItem = testTypeVM.TestItem,
                        Category = testTypeVM.Category,
                        MaxReference = testTypeVM.MaxReference,
                        MinReference = testTypeVM.MinReference,
                        Unit = testTypeVM.Unit
                    };

                    db.TestTypes.Add(testType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(testTypeVM);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: TestTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TestType testType = db.TestTypes.Find(id);
                TestTypeVM testTypeVM = new TestTypeVM
                {
                    Id = testType.Id,
                    Category = testType.Category,
                    MaxReference = testType.MaxReference,
                    MinReference = testType.MinReference,
                    TestItem = testType.TestItem,
                    Unit = testType.Unit
                };

                if (testType == null)
                {
                    return HttpNotFound();
                }
                return View(testTypeVM);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // POST: TestTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestTypeVM testTypeVM, Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    TestType testType = db.TestTypes.SingleOrDefault(q => q.Id == id);
                    testType.TestItem = testTypeVM.TestItem;
                    testType.Category = testTypeVM.Category;
                    testType.MaxReference = testTypeVM.MaxReference;
                    testType.MinReference = testTypeVM.MinReference;
                    testType.Unit = testTypeVM.Unit;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(testTypeVM);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: TestTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TestType testType = db.TestTypes.Find(id);
                if (testType == null)
                {
                    return HttpNotFound();
                }
                return View(testType);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: TestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                TestType testType = db.TestTypes.Find(id);
                db.TestTypes.Remove(testType);
                db.SaveChanges();
                return RedirectToAction("Index");
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
