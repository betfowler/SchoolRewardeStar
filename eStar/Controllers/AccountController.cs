using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using eStar.ViewModels;
using eStar.Security;

namespace eStar.Controllers
{
    public class AccountController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        //Check correct log in details provided
        [HttpPost]
        public ActionResult Login(AccountViewModel avm, AccountModel am)
        {
            if(am.findPassword(avm.Account.Email) == true)
            {
                ViewBag.Error = "You have not set up a password for your account";
                return View("Index");
            }
            if (string.IsNullOrEmpty(avm.Account.Email) || string.IsNullOrEmpty(avm.Account.Password) || am.login(avm.Account.Email, avm.Account.Password) == false)
            {
                ViewBag.Error = "Log in details invalid";
                return View("Index");
            }
            Session["Email"] = avm.Account.Email;
            Session["Username"] = am.find(avm.Account.Email).First_Name;
            Session["UserType"] = am.find(avm.Account.Email).User_Type;

            string usertype = Session["UserType"].ToString();
            return View("Success");
            //return View("~/Views/Homepages/"+usertype+"Index.cshtml");
            
        }

        //For new users to set up their password once their account has been created
        [HttpPost]
        public ActionResult Register(AccountViewModel avm, AccountModel am)
        {
            //Search for email to check registered
            if (am.find(avm.Account.Email) == null)
            {
                ViewBag.Error = "There are no accounts registered with this email address.  To request an account with your school, please select the button at the bottom of the page";
                return View("Index");
            }
            //Check password for email == null
            if (am.findPassword(avm.Account.Email) == false)
            {
                ViewBag.Error = "There is already a password set for this account.  Please use the log in section.";
                return View("Index");
            }
            SessionPersister.Email = avm.Account.Email;
            return View("Register");
        }

        //Setting Password
        [HttpPost]
        public ActionResult SetPassword(PasswordViewModel pvm, ChangePasswordView cpv, AccountModel am)
        {
            int userId = am.find(Session["Email"].ToString()).User_ID;
            //if there is already a set password and it is uncorrect
            if (am.findPassword(Session["Email"].ToString()) == false && string.IsNullOrEmpty(cpv.OldPassword) || am.findPassword(Session["Email"].ToString()) == false && am.login(Session["Email"].ToString(), cpv.OldPassword) == false)
            {

                ViewBag.Error = "Incorrect password";
                return View("ChangePassword");                

            }
            
            if (pvm.Password != pvm.ConfirmPassword)
            {
                ViewBag.Error = "Password and confirm password must match.";
                if(cpv.OldPassword == null)
                {
                    return View("Register");
                }
            }
            else
            {
                //update query
                var query = from acc in db.Accounts
                            where acc.User_ID == userId
                            select acc;

                foreach (Account acc in query)
                {
                    acc.Password = Hashing.ComputeHash(pvm.Password);
                }

                try
                {
                    db.SaveChanges();
                    if (string.IsNullOrEmpty(cpv.OldPassword))
                    {
                        ViewBag.Message = "You have now registered your password!  Please log in.";
                        return View("Index");
                    }

                    ViewBag.Message = "You have reset your password!  Please log in.";
                    return View("Index");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return View("ChangePassword");
        }
      
        public ActionResult Logout()
        {
            SessionPersister.Email = string.Empty;
            SessionPersister.UserType = string.Empty;
            SessionPersister.Username = string.Empty;
            return RedirectToAction("Index");
        }
    }
}
