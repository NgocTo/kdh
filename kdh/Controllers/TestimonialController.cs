using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using System.Data.Entity;
using kdh.Models;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace kdh.Controllers
{
    public class TestimonialController : Controller
    {
        HospitalContext db = new HospitalContext();

        // GET: Testimonials
        public ActionResult Index()
        {
            try
            {
                var testimonials = db.Testimonials.Include(t => t.department);
                return View(testimonials.ToList());
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: Testimonials/Create
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.DepartmentId = new SelectList(db.departments, "departmentid", "department_name");
                return View();
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // POST: Testimonials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Role,Subject,Content,Rate,DepartmentId")] Testimonial testimonial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Testimonials.Add(testimonial);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.DepartmentId = new SelectList(db.departments, "departmentid", "department_name", testimonial.DepartmentId);
                return View(testimonial);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: Testimonials/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Testimonial testimonial = db.Testimonials.Find(id);
                if (testimonial == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DepartmentId = new SelectList(db.departments, "departmentid", "department_name", testimonial.DepartmentId);
                return View(testimonial);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // POST: Testimonials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Role,Subject,Content,Rate,Timestamp,Reviewed,DepartmentId")] Testimonial testimonial)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(testimonial).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentId = new SelectList(db.departments, "departmentid", "department_name", testimonial.DepartmentId);
                return View(testimonial);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: Testimonials/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Testimonial testimonial = db.Testimonials.Find(id);
                db.Testimonials.Remove(testimonial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
    }
}