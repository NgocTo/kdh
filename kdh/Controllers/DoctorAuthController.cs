using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using kdh.Models;
using kdh.Utils;
using kdh.ViewModels;

namespace kdh.Controllers
{
    public class DoctorAuthController : Controller
    {
        HospitalContext db = new HospitalContext();
        // GET: DoctorAuth
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountLoginVM user)
        {
            try
            {
                string password = Hasher.ToHashedStr(user.Password);
                var u = db.Users.SingleOrDefault(r => r.Email == user.Email && r.Password == user.Password);


                if (u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.Id.ToString(), false);
                    Session["id"] = u.Id;
                    return RedirectToAction("Index", "Doctor");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password. Please confirm your login information.");
                }

                return View();

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
                if (Session["id"] != null)
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                }
                return RedirectToAction("Login", "Doctor");

            }
            catch (Exception e)
            {
                ViewBag.ExceptionMessage = e.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
    }

}