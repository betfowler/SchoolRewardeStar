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
        //modify the type of the db field
        //private IeStarContext db = new eStarContext();
        //add constructors
        //public AccountController() { }
        //public AccountController(IeStarContext context)
        //{
         //   db = context;
        //}

        // GET: Account
        public ActionResult Index()
        {
            if(SessionPersister.Email != null)
            {
                if(SessionPersister.Email != "")
                {
                    return View("~/Views/Index/" + Session["UserType"].ToString() + "Index.cshtml");
                }
            }
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
            if (string.IsNullOrEmpty(avm.Account.Email) || string.IsNullOrEmpty(avm.Account.Password) || am.find(avm.Account.Email) == null ||am.findPassword(avm.Account.Email) == true || am.login(avm.Account.Email, avm.Account.Password) == false)
            {
                ViewBag.Error = "Log in details invalid";
                return View("Index");
            }
            SessionPersister.Email = avm.Account.Email;
            SessionPersister.UserID = am.find(avm.Account.Email).User_ID;
            SessionPersister.Username = am.find(avm.Account.Email).FullName;
            SessionPersister.UserType = am.find(avm.Account.Email).User_Type;
            
            if(SessionPersister.UserType.ToString() == "Staff" || SessionPersister.UserType.ToString() == "Admin")
            {
                SessionPersister.RemainingPoints = am.findStaffPoints(avm.Account.Email);
            }
            if(SessionPersister.UserType.ToString() == "Student")
            {
                SessionPersister.TotalPoints = am.findStudent(avm.Account.Email).Total_Points;
                SessionPersister.Balance = am.findStudent(avm.Account.Email).Balance;

                Order order = db.Orders.Where(or => or.OrderStatus_ID.Equals(5) && or.User_ID.Equals(SessionPersister.UserID)).FirstOrDefault();
                if ( order == null){
                    SessionPersister.Basket = 0;
                }
                else
                {
                    SessionPersister.Basket = order.ProductCount;
                }
                
            }
            return RedirectToAction(SessionPersister.UserType + "Index", "Index");
            
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
        public ActionResult ChangePassword(PasswordViewModel pvm, ChangePasswordView cpv, AccountModel am)
        {
            string email = SessionPersister.Email.ToString();
            int userID = am.find(email).User_ID;
            if (string.IsNullOrEmpty(cpv.OldPassword) || am.login(email, cpv.OldPassword) == false)
            {
                ViewBag.Error = "Incorrect password";
                return View("ChangePassword");
            }
            if (pvm.Password != pvm.ConfirmPassword)
            {
                ViewBag.Error = "Password and confirm password must match.";
                return View("ChangePassword");
            }
            //update query
            var query = from acc in db.Accounts
                        where acc.User_ID == userID
                        select acc;

            foreach (Account acc in query)
            {
                acc.Password = Hashing.ComputeHash(pvm.Password);
            }

            try
            {
                db.SaveChanges();
                ViewBag.Message = "You have reset your password!  Please log in.";
                Logout();
                return View("Index");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View("ChangePassword");
        }



        [HttpPost]
        public ActionResult SetPassword(PasswordViewModel pvm, AccountModel am)
        {
            string email = SessionPersister.Email.ToString();
            int userId = am.find(email).User_ID;
            
            if (pvm.Password != pvm.ConfirmPassword)
            {
                ViewBag.Error = "Password and confirm password must match.";
                return View("Register");
            }

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
                    
                ViewBag.Message = "You have now registered your password!  Please log in.";
                return View("Index");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View("Register");
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Logout()
        {
            SessionPersister.Email = string.Empty;
            SessionPersister.UserType = string.Empty;
            SessionPersister.Username = string.Empty;
            SessionPersister.RemainingPoints = 0;
            SessionPersister.UserID = 0;
            return RedirectToAction("Index", "Account");


            
        }
    }
}
