using kdh.Models;
using kdh.Utils;
using kdh.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kdh.Controllers
{
    [Authorize(Roles = "patient")]
    public class PatientController : Controller
    {
        HospitalContext context = new HospitalContext();

        private string DisplayPatientName(Patient p)
        {
            string pFn = context.Patients.SingleOrDefault(q => q.UserId == p.UserId).FirstName;
            string pLn = context.Patients.SingleOrDefault(q => q.UserId == p.UserId).LastName;
            return pFn + " " + pLn;
        }

        // GET: PortalPatient
        public ActionResult Index(Guid id) // id in Users table (=UserId in Patients table)
        {
            try
            {
                Patient patient = context.Patients.SingleOrDefault(q => q.UserId == id);
                ViewBag.PatientName = DisplayPatientName(patient);
                return View(patient);
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
                    profile.Email = (String.IsNullOrEmpty(patient.User.Email)) ? "N/A" : patient.User.Email;

                    ViewBag.PatientName = DisplayPatientName(patient);
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

                    ViewBag.PatientName = DisplayPatientName(patient);
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


        // Add a patients
        [HttpGet]
        public ActionResult UpdateAccount()
        {
            try
            {
                Guid authId = new Guid(User.Identity.Name);
                Patient patient = context.Patients.SingleOrDefault(q => q.UserId == authId); // Patient.UserId

                ViewBag.PatientName = DisplayPatientName(patient);
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        [HttpPost]
        public ActionResult UpdateAccount(UpdateAccountVM vm)
        {

            try
            {
                Guid userId = new Guid(User.Identity.Name);
                Patient patient = context.Patients.SingleOrDefault(q => q.UserId == userId); // Patient.UserId

                // find the patient and update email
                User u = context.Users.SingleOrDefault(q => q.Id == userId);
                string token = context.Patients.SingleOrDefault(q => q.UserId == userId).EmailToken;

                u.Email = vm.Email;
                context.SaveChanges();

                Mailer.SendEmail(u.Email, token);

                ViewBag.NewEmail = u.Email;
                ViewBag.PatientName = DisplayPatientName(patient);

                return View("UpdateComplete");

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }

            return View("~/Views/Errors/Details.cshtml");

        }

        // DeleteUser Patient's Portal Account
        [HttpGet]
        public ActionResult DeleteConfirmation()
        {
            try
            {
                Guid id = new Guid(User.Identity.Name);

                if (String.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Index");
                }

                User u = context.Users.SingleOrDefault(q => q.Id == id);
                Patient p = context.Patients.SingleOrDefault(q => q.UserId == id);


                if (p != null)
                {
                    // assigining value from db to VM
                    PatientVM patientVM = new PatientVM
                    {
                        FullName = p.FirstName + " " + p.LastName,
                    };

                    if (p.User != null)
                    {
                        patientVM.Email = p.User.Email;
                    }

                    ViewBag.Id = id;
                    ViewBag.AdminEmail = DisplayPatientName(p);
                    return View(patientVM);
                }

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }

            return View("~/Views/Errors/Details.cshtml");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            try
            {
                Guid id = new Guid(User.Identity.Name);

                User u = context.Users.SingleOrDefault(q => q.Id == id);
                Patient p = context.Patients.SingleOrDefault(q => q.UserId == id);

                context.Users.Remove(u);
                p.UserId = null;
                context.SaveChanges();

                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
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


        // Remote Validation
        // return false if email adress is in use
        [HttpPost]
        public JsonResult IsAvailableEmail(string email)
        {
            bool result = context.Users.Any(q => q.Email.ToLower() == email.ToLower());
            return Json(!result);
        }

    }
}