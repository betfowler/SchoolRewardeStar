using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class StudentGuardian
    {
        [Key]
        public int StudentGuardianID { get; set; }
        [Display(Name = "Student ID")]
        public int Student_User_ID { get; set; }
        [Display(Name = "Guardian ID")]
        public int Guardian_User_ID { get; set; }

        public virtual Guardian Guardians { get; set; }
        public virtual Student Students { get; set; }
    }
}