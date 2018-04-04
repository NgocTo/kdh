using kdh.Utils;
using kdh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kdh.ViewModels;

namespace kdh.Controllers
{
    public class AccountController : Controller
    {
        HospitalContext context = new HospitalContext();

        // GET: Registration
        public ActionResult Index()
        {
            try
            {
                // patients (and users) from database with values
                List<Patient> patients = context.Patients.ToList();

                // goes to view. now it's empty
                List<PatientVM> patientsVM = new List<PatientVM>();

                // assign each patient data from db to patientsVM(list)
                foreach (var p in patients)
                {
                    // assigning required field
                    PatientVM patientVM = new PatientVM
                    {
                        Id = p.Id,
                        FullName = p.FirstName + " " + p.LastName,
                        Gender = p.Gender,
                    };

                    // assign if patient has an User account
                    if (p.User != null)
                    {
                        patientVM.Email = p.User.Email;
                        patientVM.UserId = p.User.Id;
                    }
                    else patientVM.Email = "N/A";

                    patientsVM.Add(patientVM);
                }

                return View(patientsVM.OrderBy(q => q.FullName));

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        // Read a speciific patient 
        [HttpGet]
        public ActionResult Detail(Guid id)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Index");
                }

                // get data from db
                Patient patient = context.Patients.SingleOrDefault(q => q.Id == id);

                if (patient != null)
                {
                    // assigining value from db to VM
                    PatientRegistrationVM registrationVM = new PatientRegistrationVM();
                    registrationVM.FirstName = patient.FirstName;
                    registrationVM.LastName = patient.LastName;
                    registrationVM.Gender = patient.Gender;
                    registrationVM.HealthCardNumber = (String.IsNullOrEmpty(patient.HealthCardNumber)) ? "N/A" : patient.HealthCardNumber;
                    registrationVM.Address1 = (String.IsNullOrEmpty(patient.Address1)) ? "N/A" : patient.Address1;
                    registrationVM.Address2 = (String.IsNullOrEmpty(patient.Address1)) ? "N/A" : patient.Address2;
                    registrationVM.City = (String.IsNullOrEmpty(patient.City)) ? "N/A" : patient.City;
                    registrationVM.Province = (String.IsNullOrEmpty(patient.Province)) ? "N/A" : patient.Province;
                    registrationVM.PostalCode = (String.IsNullOrEmpty(patient.PostalCode)) ? "N/A" : patient.PostalCode;
                    registrationVM.DateOfBirth = patient.DateOfBirth;
                    registrationVM.Phone = (String.IsNullOrEmpty(patient.Phone)) ? "N/A" : patient.Phone;
                    registrationVM.Email = "N/A";

                    if(patient.User != null)
                    {
                        registrationVM.Email =  patient.User.Email;
                    }
                    


                    ViewBag.Id = id;
                    return View(registrationVM);
                }

                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // Add a patients
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
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
        public ActionResult Create(PatientRegistrationVM registrationVM) // represents what user submitted through the form
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Guid patientId = Guid.NewGuid();
                    Guid userId = Guid.NewGuid();

                    // Create an object of Patient
                    Patient p = new Patient
                    {
                        Id = patientId,
                        FirstName = registrationVM.FirstName,
                        LastName = registrationVM.LastName,
                        HealthCardNumber = registrationVM.HealthCardNumber,
                        Gender = registrationVM.Gender,
                        Address1 = registrationVM.Address1,
                        Address2 = registrationVM.Address2,
                        City = registrationVM.City,
                        Province = registrationVM.Province,
                        PostalCode = registrationVM.PostalCode,
                        DateOfBirth = registrationVM.DateOfBirth,
                        Phone = registrationVM.Phone,
                        EmailToken = TokenGenerator.GenerateEmailToken(),
                        Status = "active"
                    };


                    // If email address is provided, create a user account
                    if (!String.IsNullOrWhiteSpace(registrationVM.Email))
                    {
                        User u = new User
                        {
                            Id = userId,
                            Email = registrationVM.Email,
                            Role = "patient"
                        };
                        p.UserId = userId;

                        Mailer.SendEmail(u.Email, p.EmailToken);

                        context.Users.Add(u);
                    }

                    context.Patients.Add(p);

                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(registrationVM);

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

        // Update a Patient
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(id.ToString()))
                {
                    return RedirectToAction("Index");
                }

                // get data from db
                Patient patient = context.Patients.SingleOrDefault(q => q.Id == id);

                if (patient != null)
                {
                    // assigining value from db to VM
                    PatientRegistrationVM registrationVM = new PatientRegistrationVM
                    {
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        HealthCardNumber = patient.HealthCardNumber,
                        Gender = patient.Gender,
                        Address1 = patient.Address1,
                        Address2 = patient.Address2,
                        City = patient.City,
                        Province = patient.Province,
                        PostalCode = patient.PostalCode,
                        DateOfBirth = patient.DateOfBirth,
                        Phone = patient.Phone,
                    };

                    if (patient.User != null)
                    {
                        registrationVM.Email = patient.User.Email;
                    }

                    ViewBag.Id = id;
                    return View(registrationVM);
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
        public ActionResult Edit(PatientRegistrationVM registrationVM, Guid id) // id in patients table
        {
            try
            {
            if (ModelState.IsValid)
            {
                // get data from db and assign new value to patient obj
                Patient patient = context.Patients.SingleOrDefault(q => q.Id == id);
                patient.FirstName = registrationVM.FirstName;
                patient.LastName = registrationVM.LastName;
                patient.HealthCardNumber = registrationVM.HealthCardNumber;
                patient.Gender = registrationVM.Gender;
                patient.Address1 = registrationVM.Address1;
                patient.Address2 = registrationVM.Address2;
                patient.City = registrationVM.City;
                patient.Province = registrationVM.Province;
                patient.PostalCode = registrationVM.PostalCode;
                patient.DateOfBirth = registrationVM.DateOfBirth;
                patient.Phone = registrationVM.Phone;

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

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

        // Delete Patient's Portal Account
        [HttpGet]
        public ActionResult Delete(Guid id) // id in users table
        {
            try
            {
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
        public ActionResult Delete (PatientRegistrationVM registrationVM, Guid id) // id in users table
        {
            try
            {
                User u = context.Users.SingleOrDefault(q => q.Id == id);
                Patient p = context.Patients.SingleOrDefault(q => q.UserId == id);

                context.Users.Remove(u);
                p.UserId = null;
                context.SaveChanges();

                return RedirectToAction("Index");
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

        // Once patients received an email with their own token
        [HttpGet]
        public ActionResult Registration(string token)
        {
            Patient p = context.Patients.SingleOrDefault(q => q.EmailToken == token && q.EmailToken != "");

            // if email token does not exist in the table (invalid accesss)
            if (p == null)
            {
                return HttpNotFound();
            }
            else
            {
                // if valid access, pass those value to the view 
                ViewBag.DisplayName = $"{p.FirstName} {p.LastName}";
                ViewBag.UserId = p.UserId;
                return View();
            }

        }

        // when patients create a password and click the register button
        [HttpPost]
        public ActionResult Registration(AccountRegistrationVM vm)
        {
            if (ModelState.IsValid)
            {
                User u = context.Users.Single(q => q.Id == vm.UserId);
                Patient p = context.Patients.Single(q => q.UserId == vm.UserId);

                // hash password
                u.Password = Hasher.ToHashedStr(vm.Password);

                // replace the email token used for generating the link to new token
                // String.Empty does not work because the column must be unique
                p.EmailToken = TokenGenerator.GenerateEmailToken();

                context.SaveChanges();

            }

            return View();
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