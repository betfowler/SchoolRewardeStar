using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Attributes;

namespace eStar.Models
{
    [Validator(typeof(AccountValidator))]
    public class Account
    {
        [Key]
        public int User_ID { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Prefix { get; set; }
        [Display(Name = "First Name")]
        public string First_Name { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        [Display(Name = "User Type")]
        public string User_Type { get; set; }

        public string FullName
        {
            get { return First_Name + " " + Surname; }
        }
    }
    public class Student : Account
    {
        [Display(Name = "Year Group")]
        public string Year_Group { get; set; }
        [Display(Name = "Tutor Group")]
        public string Tutor_Group { get; set; }
        [Display(Name = "Total Points")]
        public int Total_Points { get; set; }
        [Display(Name = "Current Balance")]
        public int Balance { get; set; }

        public virtual ICollection<StudentAward> StudentAwards { get; set; }
        public virtual ICollection<Enrolment> Enrolments { get; set; }

    }

    public class Staff : Account
    {
        [Display(Name = "Job Role")]
        public string Job_Role { get; set; }
        [Display(Name = "Weekly Point Allocation")]
        public int Weekly_Points { get; set; }
        [Display(Name = "Remaining Points")]
        public int Remaining_Points { get; set; }
        public bool? Admin { get; set; }

        public virtual ICollection<Award> Awards { get; set; }
        public virtual ICollection<ClassStaff> ClassStaff { get; set; }
    }

    public class Guardian : Account
    {
    }

    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(Account => Account.Email).Must(IsUniqueEmail).WithMessage("Email already exists");
        }

        private bool IsUniqueEmail(Account account, string email)
        {
            var db = new eStarContext();

            if(account.User_ID > 0)
            {
                if (db.Accounts.Where(acc => acc.User_ID == account.User_ID).FirstOrDefault().Email == email) return true;
            }

            if (db.Accounts.Where(acc => acc.Email == email).FirstOrDefault() == null) return true;
            return false;
        }
    }
}