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
    public class TestTypesController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: TestTypes
        public ActionResult Index()
        {
            return View(db.TestTypes.ToList());
        }

        // GET: TestTypes/Details/5
        public ActionResult Details(Guid? id)
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

        // GET: TestTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category,TestItem,MaxReference,MinReference,Unit")] TestType testType)
        {
            if (ModelState.IsValid)
            {
                testType.Id = Guid.NewGuid();
                db.TestTypes.Add(testType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testType);
        }

        // GET: TestTypes/Edit/5
        public ActionResult Edit(Guid? id)
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

        // POST: TestTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category,TestItem,MaxReference,MinReference,Unit")] TestType testType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testType);
        }

        // GET: TestTypes/Delete/5
        public ActionResult Delete(Guid? id)
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

        // POST: TestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TestType testType = db.TestTypes.Find(id);
            db.TestTypes.Remove(testType);
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
