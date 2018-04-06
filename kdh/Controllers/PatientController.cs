using kdh.Models;
using kdh.Utils;
using kdh.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kdh.Controllers
{
    [Authorize(Roles = "patient")]
    public class PatientController : Controller
    {
        HospitalContext context = new HospitalContext();

        // GET: PortalPatient
        public ActionResult Index(Guid id) // id in Users table (=UserId in Patients table)
        {
            try
            {
                Patient p = context.Patients.SingleOrDefault(q => q.UserId == id);
                return View(p);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        public ActionResult MyProfile()
        {
            try
            {
                // Guid id = new Guid(Session["id"].ToString());
                Guid authId = new Guid(User.Identity.Name);

                // get data from db
                Patient patient = context.Patients.SingleOrDefault(q => q.UserId == authId);

                if (patient != null)
                {
                    // assigining value from db to VM
                    PatientProfileVM profile = new PatientProfileVM();
                    profile.FirstName = patient.FirstName;
                    profile.LastName = patient.LastName;
                    profile.Gender = patient.Gender;
                    profile.HealthCardNumber = (String.IsNullOrEmpty(patient.HealthCardNumber)) ? "N/A" : patient.HealthCardNumber;
                    profile.Address1 = (String.IsNullOrEmpty(patient.Address1)) ? "N/A" : patient.Address1;
                    profile.Address2 = (String.IsNullOrEmpty(patient.Address1)) ? "N/A" : patient.Address2;
                    profile.City = (String.IsNullOrEmpty(patient.City)) ? "N/A" : patient.City;
                    profile.Province = (String.IsNullOrEmpty(patient.Province)) ? "N/A" : patient.Province;
                    profile.PostalCode = (String.IsNullOrEmpty(patient.PostalCode)) ? "N/A" : patient.PostalCode;
                    profile.DateOfBirth = patient.DateOfBirth;
                    profile.Phone = (String.IsNullOrEmpty(patient.Phone)) ? "N/A" : patient.Phone;

                    return View(profile);
                }

                return RedirectToAction("Index", authId);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            try
            {
                // Guid id = new Guid(Session["id"].ToString());
                Guid authId = new Guid(User.Identity.Name);

                // get data from db
                Patient patient = context.Patients.SingleOrDefault(q => q.UserId == authId);

                if (patient != null)
                {
                    // assigining value from db to VM
                    PatientProfileVM profile = new PatientProfileVM();
                    profile.FirstName = patient.FirstName;
                    profile.LastName = patient.LastName;
                    profile.Gender = patient.Gender;
                    profile.HealthCardNumber = (String.IsNullOrEmpty(patient.HealthCardNumber)) ? null : patient.HealthCardNumber;
                    profile.Address1 = (String.IsNullOrEmpty(patient.Address1)) ? null : patient.Address1;
                    profile.Address2 = (String.IsNullOrEmpty(patient.Address1)) ? null : patient.Address2;
                    profile.City = (String.IsNullOrEmpty(patient.City)) ? null : patient.City;
                    profile.Province = (String.IsNullOrEmpty(patient.Province)) ? null : patient.Province;
                    profile.PostalCode = (String.IsNullOrEmpty(patient.PostalCode)) ? null : patient.PostalCode;
                    profile.DateOfBirth = patient.DateOfBirth;
                    profile.Phone = (String.IsNullOrEmpty(patient.Phone)) ? null : patient.Phone;

                    return View(profile);
                }

                return RedirectToAction("Index", authId);

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(PatientProfileVM profile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Guid id = new Guid(Session["id"].ToString());
                    Guid authId = new Guid(User.Identity.Name);

                    // get data from db and assign new value to patient obj
                    Patient patient = context.Patients.SingleOrDefault(q => q.UserId == authId);

                    patient.UserId = authId;

                    patient.FirstName = profile.FirstName;
                    patient.LastName = profile.LastName;
                    patient.HealthCardNumber = String.IsNullOrEmpty(profile.HealthCardNumber) ? null : profile.HealthCardNumber;
                    patient.Gender = String.IsNullOrEmpty(profile.Gender) ? null : profile.Gender;
                    patient.Address1 = String.IsNullOrEmpty(profile.Address1) ? null : profile.Address1;
                    patient.Address2 = String.IsNullOrEmpty(profile.Address2) ? null : profile.Address2;
                    patient.City = String.IsNullOrEmpty(profile.City) ? null : profile.City;
                    patient.Province = String.IsNullOrEmpty(profile.Province) ? null : profile.Province;
                    patient.PostalCode = String.IsNullOrEmpty(profile.PostalCode) ? null : profile.PostalCode;
                    patient.DateOfBirth = profile.DateOfBirth == null ? null : profile.DateOfBirth;
                    patient.Phone = String.IsNullOrEmpty(profile.Phone) ? null : profile.Phone;

                    context.SaveChanges();

                    return RedirectToAction("MyProfile");
                }

                return RedirectToAction("MyProfile");

            }
            catch (DbUpdateException DbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(DbException);
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

    }
}