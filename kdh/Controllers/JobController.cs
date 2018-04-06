using kdh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace kdh.Controllers
{
    public class JobController : Controller
    {
        HospitalContext db = new HospitalContext();
        // GET: Jobs
        [Authorize(Roles = "hr")]
        public ActionResult Index_admin()
        {
            try
            {
                var extra = db.Jobs.Include(j => j.department).Include(j => j.User);
                List<Job> job = db.Jobs.ToList();
                return View(job);

            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: Jobs/Details
        [Authorize(Roles = "hr")]
        public ActionResult Details_Admin(string job_id)
        {
            if (job_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(job_id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: add job
        [Authorize(Roles = "hr")]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.departments = db.departments.ToList();
                ViewBag.users = db.Users.ToList();
                return View();
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        // POST: add job
        [Authorize(Roles = "hr")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,JobTitle,JobStatus,JobDescription,DepartmentId,DatePosted,DateClosed,JobShift,Salary,Requirement,UserId")] Job job)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Jobs.Add(job);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.departments = db.departments.ToList();
                ViewBag.users = db.Users.ToList();
                return View(job);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        // GET: edit job
        [Authorize(Roles = "hr")]
        [HttpGet]
        public ActionResult Edit(string job_id)
        {
            try
            {
                Job job = db.Jobs.SingleOrDefault(m => m.JobId == job_id);

                if (job == null)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.departments = db.departments.ToList();
                ViewBag.users = db.Users.ToList();
                return View(job);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/General.cshtml");
        }

        // POST: edit job
        [Authorize(Roles = "hr")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,JobTitle,JobStatus,JobDescription,DepartmentId,DatePosted,DateClosed,JobShift,Salary,Requirement,UserId")]Job job)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.departments = db.departments.ToList();
                ViewBag.users = db.Users.ToList();
                return View(job);
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
            return View("~/Views/Errors/General.cshtml");
        }

        //GET delete job
        [Authorize(Roles = "hr")]
        //[HttpGet]
        public ActionResult Delete(string job_id)
        {
            try
            {
                Job job = db.Jobs.SingleOrDefault(model => model.JobId == job_id);
                if (job_id == null)
                {
                    return RedirectToAction("Index");
                }
                return View(job);
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

        //POST delete job
        [Authorize(Roles = "hr")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection form)
        {
            try
            {
                string job_id = form["JobId"];
                Job job = db.Jobs.Find(job_id);
                db.Jobs.Remove(job);
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

        // PUBLIC CONTROLLER
        // GET: Jobs Index public side
        public ActionResult Index()
        {
            try
            {
                var extra = db.Jobs.Include(j => j.department).Include(j => j.User);
                List<Job> job = db.Jobs.ToList();
                return View(job);

            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // GET: Jobs/Details public side

        public ActionResult Details(string job_id)
        {
            if (job_id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(job_id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
    }
}