using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class StudentAward
    {
        [Key]
        public int StudentAward_ID { get; set; }
        public int Student_ID { get; set; }
        public int Award_ID { get; set; }


    }
}