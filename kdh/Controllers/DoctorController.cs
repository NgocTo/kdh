using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kdh.Models;
using kdh.ViewModels;

namespace kdh.Controllers
{
    public class DoctorController : Controller
    {
        HospitalContext db = new HospitalContext();
        // GET: Doctor
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
            catch(Exception e)
            {
                ViewBag.err = e.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                ViewBag.Departments = db.departments.ToList();
                return View();
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
            }
            return View();
        }
        [HttpPost]
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
            catch(Exception e)
            {
                ViewBag.err = e.Message;
            }
            return View();
        }
        [HttpGet]
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
                ViewBag.err = e.Message;
            }
            //errror
            return View();
        }
        [HttpPost]
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
                else
                {
                   // ViewBag.err = "invalid";
                }
                
                ViewBag.err = "invalid";
                return View();

            }
            catch(Exception e)
            {
                ViewBag.err = e.Message;
            }
            return View();
        }
        [HttpGet]
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
            catch(Exception e)
            {
                ViewBag.err = e.Message;
            }

            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
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
            catch(Exception e)
            {
                ViewBag.err = e.Message;
            }
            return View();
        }
        public PartialViewResult DoctorSearch(FormCollection form)
        {
            string search = form["SearchBar"];
            
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
            return PartialView("~/Views/Doctor/_DoctorSearch.cshtml", doctordepartment);

        }
    }
    
}