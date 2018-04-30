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
    public class OnlineConsultationFormsController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: OnlineConsultationForms
        public ActionResult Index()
        {
            return View(db.OnlineConsultationForms.ToList());
        }

        // GET: OnlineConsultationForms/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineConsultationForm onlineConsultationForm = db.OnlineConsultationForms.Find(id);
            if (onlineConsultationForm == null)
            {
                return HttpNotFound();
            }
            return View(onlineConsultationForm);
        }

        // GET: OnlineConsultationForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OnlineConsultationForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DateOfBirth,Gender,PhoneNumber,Email,Specialization,Comment,DateOfConsultation")] OnlineConsultationForm onlineConsultationForm)
        {
            if (ModelState.IsValid)
            {
                onlineConsultationForm.Id = Guid.NewGuid();
                db.OnlineConsultationForms.Add(onlineConsultationForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(onlineConsultationForm);
        }

        // GET: OnlineConsultationForms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineConsultationForm onlineConsultationForm = db.OnlineConsultationForms.Find(id);
            if (onlineConsultationForm == null)
            {
                return HttpNotFound();
            }
            return View(onlineConsultationForm);
        }

        // POST: OnlineConsultationForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DateOfBirth,Gender,PhoneNumber,Email,Specialization,Comment,DateOfConsultation")] OnlineConsultationForm onlineConsultationForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(onlineConsultationForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(onlineConsultationForm);
        }

        // GET: OnlineConsultationForms/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OnlineConsultationForm onlineConsultationForm = db.OnlineConsultationForms.Find(id);
            if (onlineConsultationForm == null)
            {
                return HttpNotFound();
            }
            return View(onlineConsultationForm);
        }

        // POST: OnlineConsultationForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            OnlineConsultationForm onlineConsultationForm = db.OnlineConsultationForms.Find(id);
            db.OnlineConsultationForms.Remove(onlineConsultationForm);
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
