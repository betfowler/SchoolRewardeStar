using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStar.ViewModels
{
    public class EnrolmentViewModel
    {
        public List<Staff> Staff { get; set; }
        public List<Student> Students { get; set; }
        public int ClassID { get; set; }

    }
}