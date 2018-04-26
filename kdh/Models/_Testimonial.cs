using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kdh.Models
{
    public class _Testimonial
    {
        // this view model will hold a list of fields
        public List<Testimonial> Contents { get; set; }
        public List<Testimonial> Subjects { get; set; }

        public Testimonial Content { get; set; }
        public Testimonial Subject { get; set; }
    }
}