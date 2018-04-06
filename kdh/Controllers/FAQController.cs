using kdh.ViewModels;
using kdh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using kdh.Utils;
using System.Data.SqlClient;

namespace kdh.Controllers
{
    //[Authorize(Roles = "admin")]
    public class FAQController : Controller
    {

        HospitalContext db = new HospitalContext();
        //FOR FAQS
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {

            try
            {
                List<FAQ> Faq = db.FAQs.ToList();
                List<Purpos> Purpose = db.Purposes.ToList();
                FAQsPurposes faqpurpose = new FAQsPurposes();
                faqpurpose.Faqs = Faq;
                faqpurpose.Purposes = Purpose;
                return View(faqpurpose);
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }

            return RedirectToAction("Index");
        }
        //create details of FAQ for admin
        // GET: FAQ
        //[Authorize(Roles = "admin")]
        public ActionResult Details()
        {
            try
            {
                List<FAQ> Faq = db.FAQs.ToList();
                List<Purpos> Purpose = db.Purposes.ToList();
                FAQsPurposes faqpurpose = new FAQsPurposes();
                faqpurpose.Faqs = Faq;
                faqpurpose.Purposes = Purpose;
                return View(faqpurpose);
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }

            return RedirectToAction("Index");
        }


        //Create a new FAQ
       [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                ViewBag.Purpose = db.Purposes.ToList();
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FAQ faq)
        {

            try
            {
                if (ModelState.IsValid)
                {
                   /* FAQ f = new FAQ
                    {
                        QueId = faq.QueId,
                        Question = faq.Question,
                        Answer = faq.Answer,
                        DateCreated = faq.DateCreated,
                        AuthorFirstName = faq.AuthorFirstName,
                        AuthorityFirstName = faq.AuthorityFirstName,
                        PurposeId = faq.PurposeId

                    };*/
                    db.FAQs.Add(faq);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                ViewBag.Purpose = db.Purposes.ToList();
                return View(faq);
            }
            catch (DbUpdateException uex)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(uex);
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }
        //update a FAQ 
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == 0)
                {
                    return RedirectToAction("Index");
                }
                FAQ faq = db.FAQs.SingleOrDefault(c => c.QueId == id);
                if (faq == null)
                {
                    return RedirectToAction("Index");
                }
                
                ViewBag.Purpose = db.Purposes.ToList();
                return View(faq);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Edit(FAQ faq)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(faq).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Purpose = db.Purposes.ToList();
                return View(faq);
            }
            catch (DbUpdateException uex)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(uex);
                //ViewBag.DbExceptionMessage = uex.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        //Delete a FAQ
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                FAQ faq = db.FAQs.SingleOrDefault(c => c.QueId == id);
                if (faq == null)
                {
                    return RedirectToAction("Index");
                }
                return View(faq);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        public ActionResult Delete(FormCollection form)
        {
            try
            {
                int id = Convert.ToInt32(form["QueId"]); //This is getting from an input inside <form> with Name="Id"
                FAQ faq = db.FAQs.Find(id);
                db.FAQs.Remove(faq);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(dbException);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }


        //FOR PURPOSES
        [Authorize(Roles = "admin")]
        public ActionResult PurposeList()
        {
            try
            {
                List<Purpos> Purpose = db.Purposes.ToList();
                return View(Purpose);
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }

            return RedirectToAction("PurposeList");
        }
        //add new purpose
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddPurpose()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
      
        [HttpPost]
        public ActionResult AddPurpose(Purpos purpose)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Purpos p = new Purpos
                    {
                        PurposeId = purpose.PurposeId,
                        PurposeToCreate = purpose.PurposeToCreate

                    };
                    db.Purposes.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("PurposeList");

                }
                return View(purpose);
            }
            catch (DbUpdateException uex)
            {
                ViewBag.DbExceptionMessage = uex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        //update a purpose
        [HttpGet]
       [Authorize(Roles = "admin")]
        public ActionResult EditPurpose(int? id)
        {
            try
            {
                if (id == 0)
                {
                    return RedirectToAction("PurposeList");
                }
                Purpos purpose = db.Purposes.SingleOrDefault(c => c.PurposeId == id);
                if (purpose == null)
                {
                    return RedirectToAction("PurposeList");
                }
                return View(purpose);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        public ActionResult EditPurpose(Purpos purpose)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(purpose).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("PurposeList");
                }
                return View(purpose);
            }
            catch (DbUpdateException uex)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(uex);
                //ViewBag.DbExceptionMessage = uex.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        //Delete a Purpose
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeletePurpose(int? id)
        {
            if (id == 0)
            {
                return RedirectToAction("PurposeList");
            }
            try
            {
                Purpos purpose = db.Purposes.SingleOrDefault(c => c.PurposeId == id);
                if (purpose == null)
                {
                    return RedirectToAction("PurposeList");
                }
                return View(purpose);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
        [HttpPost]
        public ActionResult DeletePurpose(FormCollection form)
        {
            try
            {
                int id = Convert.ToInt32(form["PurposeId"]);
                Purpos purpose = db.Purposes.Find(id);
                db.Purposes.Remove(purpose);
                db.SaveChanges();
                return RedirectToAction("PurposeList");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(dbException);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
        }
    }
}