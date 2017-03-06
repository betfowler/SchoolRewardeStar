using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class ClassStaff
    {
        [Key]
        public int ClassStaff_ID { get; set; }
        public int Class_ID { get; set; }
        public int User_ID { get; set; }
        public virtual Class Class { get; set; }
        public virtual Staff Staff { get; set; }
    }
}