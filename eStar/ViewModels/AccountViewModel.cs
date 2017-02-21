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
    public class AccountViewModel
    {
        public Account Account { get; set; }
    }

    public class PasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterPasswordView : PasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ChangePasswordView : PasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        
    }

}