using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using kdh.Models;

namespace kdh.ViewModels
{
    public class DoctorDepartment
    {
        public Doctor doctor { get; set; }
        public department department { get; set; }

    }
}