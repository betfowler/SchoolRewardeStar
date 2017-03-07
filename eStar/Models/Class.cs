using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eStar.Models
{
    [Validator(typeof(ClassValidator))]
    public class Class
    {
        [Key]
        public int Class_ID { get; set; }
        public string Class_Name { get; set; }

        public virtual List<Enrolment> Enrolments { get; set; }
        public virtual List<ClassStaff> ClassStaff { get; set; }
    }

    public class ClassValidator : AbstractValidator<Class>
    {
        public ClassValidator()
        {
            RuleFor(Class => Class.Class_Name).Must(IsUniqueClass).WithMessage("Class Name Already Exists");
        }

        private bool IsUniqueClass (Class @class, string className)
        {
            var db = new eStarContext();

            if (@class.Class_ID > 0)
            {
                if (db.Classes.Where(cl => cl.Class_ID == @class.Class_ID).FirstOrDefault().Class_Name == className) return true;
            }

            if (db.Classes.Where(cl => cl.Class_Name == className).FirstOrDefault() == null) return true;
            return false;
        }
    }
}