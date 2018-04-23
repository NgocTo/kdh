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
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Attributes;
using kdh.ViewModels;

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

        public ActionResult Index_Admin()
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
                ViewBag.departments = db.departments.ToList();
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
                if (this.IsCaptchaValid("Captcha is not valid."))
                {
                    if (ModelState.IsValid)
                    {
                        var DirtyWords = db.DirtyWords.ToList();
                        foreach (var row in DirtyWords)
                        {
                            if ((testimonial.Content.Contains(row.Word)) || (testimonial.Subject.Contains(row.Word)))
                            {
                                testimonial.Timestamp = DateTime.Now;
                                testimonial.Reviewed = "PENDING";
                                db.Testimonials.Add(testimonial);
                                db.SaveChanges();
                                return RedirectToAction("Index"); ;
                            }
                        }
                        testimonial.Timestamp = DateTime.Now;
                        testimonial.Reviewed = "NO";
                        db.Testimonials.Add(testimonial);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.ErrorCaptcha = "Captcha is not valid.";
                    return View(testimonial);
                }

                ViewBag.departments = db.departments.ToList();
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
        public ActionResult Edit_Admin(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index_Admin"); ;
                }
                Testimonial testimonial = db.Testimonials.Find(id);
                if (testimonial == null)
                {
                    return HttpNotFound();
                }
                ViewBag.departments = db.departments.ToList();
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
        public ActionResult Edit_Admin([Bind(Include = "Id,Subject,Content")] TestimonialVM testimonial)
        {
            try
            {
                //testimonial.Reviewed = "YES";
                var dbTestimonial = db.Testimonials.Find(testimonial.Id);
                dbTestimonial.Subject = testimonial.Subject;
                dbTestimonial.Content = testimonial.Content;
                dbTestimonial.Reviewed = "YES";
                if (ModelState.IsValid)
                {
                    db.Entry(dbTestimonial).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index_Admin");
                }
                ViewBag.departments = db.departments.ToList();
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
        public ActionResult Delete_Admin(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index_Admin");
            }
            Testimonial testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete_Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Admin(int id)
        {
            try
            {
                Testimonial testimonial = db.Testimonials.Find(id);
                db.Testimonials.Remove(testimonial);
                db.SaveChanges();
                return RedirectToAction("Index_Admin");
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

        //AJAX

        public PartialViewResult Testimonial_Search(FormCollection form)
        {
            buddy_Testimonial testimonial_list = new buddy_Testimonial();
            string search_term = form["term"];
            if (!String.IsNullOrWhiteSpace(search_term))
            {
                try
                {
                    //testimonial_list = db.Testimonials.Where(t => t.Content.ToLower().Contains(search_term.ToLower())).ToList();
                    List<Testimonial> testimonial = db.Testimonials.ToList();
                    return PartialView("_Testimonials", testimonial);
                }
                catch (Exception genericException)
                {
                    ViewBag.ExceptionMessage = genericException.Message;
                }
                return PartialView("~/Views/Errors/_Details.cshtml");
            }
            return PartialView("~/Views/Testimonial/_Testimonials.cshtml", testimonial_list);
        }
    }
}