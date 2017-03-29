using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStar.Models;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.Attributes;

namespace eStar.ViewModels
{
    public class EnrolmentViewModel
    {
        public Class Class { get; set; }
        public List<Student> Students { get; set; }
        public List<Staff> Staff { get; set; }
        public int StudentCount { get; set; }
        public int StaffCount { get; set; }
    }
}
