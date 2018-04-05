using kdh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kdh.Controllers
{
    public class HomeController : Controller
    {
        HospitalContext db = new HospitalContext();
        // GET: Home
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
        public ActionResult Login(User users)
        {
            //Return the number of rows returned from the database (should be 1)
            int count = db.Users.Where(
                    u => u.Id == users.Id
                &&
            u.Password == users.Password).Count();
            if (count == 1)
            {
                //set the authcookie with your username or any other value. This username is also being used to determine your user role.
                FormsAuthentication.SetAuthCookie(users.Id.ToString(), false);
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Invalid username and/or password";
            return View(users);
        }
        public ActionResult Logout()
        {
            //unset the authcookie
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}