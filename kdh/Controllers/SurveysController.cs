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
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace kdh.Controllers
{
    public class SurveysController : Controller
    {
        private HospitalContext db = new HospitalContext();

        //[HttpPost]
        //[Authorize]
        //public ActionResult Search_By_Name_Ajax(FormCollection form) //search by question in Admin Page
        //{
        //    try
        //    {
        //        string search = form["search"];
        //        List<Survey> = survey;
        //        if (search == "")
        //        {
        //            survey = db.Surveys.ToList();
        //        }
        //        else
        //        {
        //            survey = db.Surveys.Where(q => q.Username.ToLower().Contains(search.ToLower())).ToList();
        //        }
        //        return PartialView("~/Views/Survey/_Search_By_Name_Ajax.cshtml", survey);
        //    }
        //    catch (Exception e)
        //    {

        //        ViewBag.ExceptionMessage = e.Message;
        //    }
        //    return View("~/Views/Shared/_Error.cshtml");
        //}



        // GET: Surveys
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            
            return View(db.Surveys.ToList());
        }









        // GET: Surveys/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Survey survey = db.Surveys.Find(id);
                if (survey == null)
                {
                    return HttpNotFound();
                }
                return View(survey);
            }
            catch (Exception Exception)
            {
                TempData["ExceptionMessage"] = Exception.Message;
            }
            return RedirectToAction("Index");
            
        }









        // GET: Surveys/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create(Survey survey)
        public ActionResult Create([Bind(Include = "Id,UserName,DateOfSurvey,QualityService,AverageVisit,AppointmentIssue,StaffRate,Comment")] Survey survey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Surveys.Add(survey);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                return View(survey);
            }
            catch (Exception Exception)
            {
                TempData["ExceptionMessage"] = Exception.Message;
            }
            
            return RedirectToAction("Index", "Home");
            
        }










        // GET: Surveys/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Survey survey = db.Surveys.Find(id);
                if (survey == null)
                {
                    return HttpNotFound();
                }
                return View(survey);
            }
            catch (Exception Exception)
            {
                TempData["ExceptionMessage"] = Exception.Message;
            }
            return RedirectToAction("Index");

        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,DateOfSurvey,QualityService,AverageVisit,AppointmentIssue,StaffRate,Comment")] Survey survey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(survey).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(survey);
            }
            catch (DbUpdateException uex)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(uex);
                //ViewBag.DbExceptionMessage = uex.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception Exception)
            {
                TempData["ExceptionMessage"] = Exception.Message;
            }
            return RedirectToAction("Index");

        }











        // GET: Surveys/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Survey survey = db.Surveys.Find(id);
                if (survey == null)
                {
                    return HttpNotFound();
                }
                return View(survey);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Survey survey = db.Surveys.Find(id);
                db.Surveys.Remove(survey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(dbException);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
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
