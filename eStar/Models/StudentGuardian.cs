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
        public int Student_User_ID { get; set; }
        public int Guardian_User_ID { get; set; }

        public virtual Guardian Guardians { get; set; }
        public virtual Student Students { get; set; }
    }
}