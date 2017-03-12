using eStar.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    public class Subject
    {
        [Key]
        public int Subject_ID { get; set; }
        public string Subject_Name { get; set; }
    }
}