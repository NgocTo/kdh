using kdh.Utils;
using kdh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kdh.ViewModels;
using System.Web.Security;

namespace kdh.Controllers
{
    public class AccountController : Controller
    {
        HospitalContext context = new HospitalContext();

        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("Login");
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
            try
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
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // when patients create a password and click the register button
        [HttpPost]
        public ActionResult Registration(AccountRegistrationVM vm)
        {

            try
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

                return View("RegistrationComplete");
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // Login Forms
        public ActionResult Login()
        {
            try
            {
                string authId = User.Identity.Name; // Id from users table

                // authId is set (= user has been logged in)
                if (!String.IsNullOrEmpty(authId))
                {
                    User u = context.Users.FirstOrDefault(q => q.Id.ToString() == authId);

                    if (u.Role == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (u.Role == "patient")
                    {
                        return RedirectToAction("Index", "Patient", new { Id = u.Id });
                    }
                    else // this is for other roles
                    {
                        return View();
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpPost]
        public ActionResult Login(AccountLoginVM vm)
        {
            try
            {
                string password = Hasher.ToHashedStr(vm.Password);
                var u = context.Users.SingleOrDefault(q => q.Email.ToLower() == vm.Email.ToLower() && q.Password == password);

                // if username(email) and password are correct
                if (u != null && u.Role == "patient")
                {
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), false);

                    return RedirectToAction("Index", "Patient", new { Id = u.Id });
                }
                else if (u != null && u.Role == "admin")
                {
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), false);

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password. Please confirm your login information.");
                }

                return View("Login");

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        public ActionResult Logout()
        {
            try
            {
                // if (Session["id"] != null)
                if (User.Identity.Name != null)
                {
                    FormsAuthentication.SignOut();
                }
                return RedirectToAction("Login");

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

        // JADE'S ACCOUNT CONTROLLER
        // Login action
        [HttpGet]
        public ActionResult JobLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult JobLogin(User user)
        {
            string password = Hasher.ToHashedStr(user.Password);
            var usr = context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == password);
            if (usr != null && usr.Role == "hr")
            {
                FormsAuthentication.SetAuthCookie(usr.Id.ToString(), false);

                return RedirectToAction("Index_Admin", "Job");
            }
            else
            {
                ViewBag.JobLoginMessage = "Incorrect username or password.";
            }
            return View();
            }

        //Logout action
        public ActionResult JobLogout()
        {
            try
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }

    }
}