using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Class
    {
        [Key]
        public int Class_ID { get; set; }
        public string Class_Name { get; set; }

        public virtual ICollection<Enrolment> Enrolments { get; set; }
        public virtual ICollection<ClassStaff> ClassStaff { get; set; }

    }
}