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

        //Check correct log in details provided
        [HttpPost]
        public ActionResult Login(AccountViewModel avm, AccountModel am)
        {
            if (string.IsNullOrEmpty(avm.Account.Email) || string.IsNullOrEmpty(avm.Account.Password) || am.login(avm.Account.Email, avm.Account.Password) == false)
            {
                ViewBag.Error = "Log in details invalid";
                return View("Index");
            }
            SessionPersister.Username = am.find(avm.Account.Email).First_Name;
            SessionPersister.UserType = am.find(avm.Account.Email).User_Type;
            return View("Success");
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

        //Editing account
        [HttpPost]
        public ActionResult SetPassword(RegisterPasswordViewModel rpvm, AccountModel am)
        {
            string usertype = am.find(SessionPersister.Email).User_Type;
            int userId = am.find(SessionPersister.Email).User_ID;

            if (usertype == "Student")
            {
                //update query
                var query = from acc in db.Accounts
                            where acc.User_ID == userId
                            select acc;

                foreach (Account acc in query)
                {
                    acc.Password = Hashing.ComputeHash(rpvm.Password);
                }

                try
                {
                    db.SaveChanges();
                    ViewBag.Message = "You are now registered!  Please log in.";
                    return View("Index");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }           
            
            return View("Register");
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
