using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kdh.Models;
using System.Web.Helpers;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace kdh.Controllers
{
    [Authorize]
    public class AnswersController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: Answers
        public ActionResult Index(int? qid)
        {
            try {
                var answers = db.Answers.Where(m => m.Questionsid == qid);
                ViewBag.question = db.Questions.SingleOrDefault(m => m.Id == qid).question1;
               
                return View(answers.ToList());
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");


        }

        // GET: Answers/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Answer answer = db.Answers.Find(id);
        //    if (answer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(answer);
        //}
        private string DisplayAdminEmail()
        {
            string adminId = User.Identity.Name;
            string adminEmail = db.Users.SingleOrDefault(q => q.Id.ToString() == adminId).Email;
            return adminEmail;

        }

        // GET: Answers/Create
        [Authorize]
        public ActionResult Create(int? qid)
        {
            try {
                ViewBag.Questionid = qid;
                ViewBag.adminmail = DisplayAdminEmail();
                ViewBag.question = db.Questions.SingleOrDefault(m => m.Id == qid).question1;
                return View();
            }
            catch (Exception genericException){
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");
            //ViewBag.Questionsid = new SelectList(db.Questions, "Id", "Email");

        }

        // POST: Answers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Questionsid,Answer1,Sender_mail,Create_date")] Answer answer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Answers.Add(answer);
                    db.SaveChanges();
                    string email = db.Questions.FirstOrDefault(m => m.Id == answer.Questionsid).Email;
                    sendmail(email, answer.Answer1);

                    return RedirectToAction("Index", new { qid = answer.Questionsid });
                }

                ViewBag.Questionsid = new SelectList(db.Questions, "Id", "Email", answer.Questionsid);
                return View(answer);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // GET: Answers/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Answer answer = db.Answers.Find(id);
        //    if (answer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.Questionsid = new SelectList(db.Questions, "Id", "Email", answer.Questionsid);
        //    return View(answer);
        //}

        // POST: Answers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Questionsid,Answer1,Sender_mail,Create_date")] Answer answer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(answer).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index", new { qid = answer.Questionsid });
        //    }
        //    ViewBag.Questionsid = new SelectList(db.Questions, "Id", "Email", answer.Questionsid);
        //    return View(answer);
        //}

        // GET: Answers/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Answer answer = db.Answers.Find(id);
                if (answer == null)
                {
                    return HttpNotFound();
                }
                return View(answer);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Answer answer = db.Answers.Find(id);
                int question = db.Answers.FirstOrDefault(m => m.Id == id).Questionsid;
                db.Answers.Remove(answer);
                db.SaveChanges();

                return RedirectToAction("Index","Questions");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlException = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }
            return View("~/Views/Errors/Details.cshtml");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private void sendmail(string toemail,string Message)
        { 
                var fromAddress = new MailAddress(ConfigurationManager.AppSettings["EmailFrom"], "Hospital Admin");
                var toAddress = new MailAddress(toemail);



                string fromPassword = ConfigurationManager.AppSettings["EmailPassword"];
                const string subject = "Reply to Your Question";
                string body = "Dear visitor Answer to your Query is : " + Message + "\n If you are not satisfied you can contact us";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
       


        }
    }
}
