using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kdh.Models;
using kdh.ViewModels;

namespace kdh.Controllers
{
    public class PatientLabResultController : Controller
    {
        private HospitalContext db = new HospitalContext();

        // GET: PatientLabResult
        public ActionResult Index()
        {
            var patients = db.Patients.Include(p => p.User)
                .Select(q => new PatientProfileVM
                {
                    Id = q.Id,
                    FirstName = q.FirstName,
                    LastName = q.LastName,
                    Phone = q.Phone,
                    HealthCardNumber = q.HealthCardNumber
                })
                .ToList();
            return View(patients);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
