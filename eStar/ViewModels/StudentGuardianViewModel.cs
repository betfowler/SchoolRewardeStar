using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStar.Models;

namespace eStar.ViewModels
{
    public class StudentGuardianViewModel
    {
        public StudentGuardian StudentGuardian { get; set; }
        public Student Student { get; set; }
        public Guardian Guardian { get; set; }
    }
}