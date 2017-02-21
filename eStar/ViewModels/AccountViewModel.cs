using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStar.Models;
using System.ComponentModel.DataAnnotations;

namespace eStar.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
    }

    public class RegisterPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}