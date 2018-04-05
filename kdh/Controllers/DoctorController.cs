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
                    return View(doctor);

                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            catch(Exception e)
            {
                ViewBag.err = e.Message;
            }
            return View();
        }
    }
}