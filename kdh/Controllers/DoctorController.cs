using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kdh.Models;
using kdh.ViewModels;
using kdh.Utils;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace kdh.Controllers
{
    //[Authorize(Roles = "admin")]
    public class DoctorController : Controller
    {
        HospitalContext db = new HospitalContext();
        // GET: Doctor
        [Authorize(Roles = "admin,hr")]
        public ActionResult Index()
        {
            try
            {

                List<Doctor> doctors = db.Doctors.ToList();
                List<department> departments = db.departments.ToList();
                List<DoctorDepartment> doctordepartment = new List<DoctorDepartment>();
                doctordepartment = doctors.Join(departments, doc => doc.Departmentid, dep => dep.departmentid, (doc, dep) => new DoctorDepartment { doctor = doc, department = dep }).ToList();
                return View(doctordepartment);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpGet]
        [Authorize(Roles = "admin,hr")]
        public ActionResult Add()
        {
            try
            {
                ViewBag.Departments = db.departments.ToList();
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Doctor doctor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Doctors.Add(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Departments = db.departments.ToList();
                return View(doctor);
            }
            catch (DbUpdateException d)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(d);
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int? id)
        {
            try
            {
                if (id != null)
                {
     
                    Doctor doctor = db.Doctors.SingleOrDefault(d => d.Doctorid == id);
                    if (doctor == null)
                    {
                        return RedirectToAction("Index");
                    }
                    ViewBag.Departments = db.departments.ToList();
                    ViewBag.err = "invalid";
                    return View(doctor);

                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Doctor doctor)
        {
            try
            {
                ViewBag.Departments = db.departments.ToList();
                Doctor olddoctor = db.Doctors.FirstOrDefault(e => e.Doctorid == doctor.Doctorid);
                if (ModelState.IsValid)
                {
                    ViewBag.err = "invalid";
                    db.Entry(olddoctor).CurrentValues.SetValues(doctor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
                
               
                return View();

        }
            catch (DbUpdateException d)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(d);
                //ViewBag.DbExceptionMessage = uex.Message;
            }
            catch (SqlException sq)
            {
                ViewBag.SqlExceptionMessage = sq.Message;
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpGet]
        [Authorize(Roles = "admin,manager")]
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    
                    return RedirectToAction("Index");
                }
                DoctorDepartment doctorDepartment = db.Doctors.Join(db.departments, doc => doc.Departmentid, dep => dep.departmentid, (doc, dep) => new DoctorDepartment { doctor = doc, department = dep }).Where(doc => doc.doctor.Doctorid == id).FirstOrDefault();
                return View(doctorDepartment);
            }
            catch (SqlException sq)
            {
                ViewBag.SqlExceptionMessage = sq.Message;
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletepost(int? id)
        {
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Doctor doctor = db.Doctors.FirstOrDefault(d => d.Doctorid == id);
                db.Doctors.Remove(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException d)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(d);
            }
            catch (SqlException s)
            {
                ViewBag.SqlExceptionMessage = s.Message;
            }
            catch (Exception g)
            {
                ViewBag.ExceptionMessage = g.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Detail(int? id)
        {
            try
            {
                if (id == null)
                {

                    return RedirectToAction("Index");
                }
                DoctorDepartment doctorDepartment = db.Doctors.Join(db.departments, doc => doc.Departmentid, dep => dep.departmentid, (doc, dep) => new DoctorDepartment { doctor = doc, department = dep }).Where(doc => doc.doctor.Doctorid == id).FirstOrDefault();
                return View(doctorDepartment);
            }
            catch (SqlException sq)
            {
                ViewBag.SqlExceptionMessage = sq.Message;
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [AllowAnonymous]
        public PartialViewResult DoctorSearch(string ajaxdoc)
        {
            string search = ajaxdoc;
            
            List<DoctorDepartment> doctordepartment = new List<DoctorDepartment>();
            if (!String.IsNullOrWhiteSpace(search))
            {
                try
                {
                    List<Doctor> doctors = db.Doctors.ToList();
                    List<department> departments = db.departments.ToList();
                    doctordepartment = doctors.Join(departments, doc => doc.Departmentid, dep => dep.departmentid, (doc, dep) => new DoctorDepartment { doctor = doc, department = dep }).Where(doc => doc.doctor.Fname.ToUpper().Contains(search.ToUpper())).ToList();
                }
                catch(Exception e)
                {
                    ViewBag.err = e.Message;
                }
            }
            return PartialView("~/Views/Doctor/_dl.cshtml", doctordepartment);

        }
        [AllowAnonymous]
        public ActionResult PublicView()
        {
            try
            {

                List<Doctor> doctors = db.Doctors.ToList();
                List<department> departments = db.departments.ToList();
                List<DoctorDepartment> doctordepartment = new List<DoctorDepartment>();
                doctordepartment = doctors.Join(departments, doc => doc.Departmentid, dep => dep.departmentid, (doc, dep) => new DoctorDepartment { doctor = doc, department = dep }).ToList();
                return View(doctordepartment);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
    }
    
}