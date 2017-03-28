using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStar.Models;

namespace eStar.ViewModels
{
    public class StudentGuardianViewModel
    {
        public List<Student> Students { get; set; }

        public List<Award> Awards { get; set; }

        public List<StudentAward> StudentAwards { get; set; }

        public List<int> AwardID { get; set; }
    }
}