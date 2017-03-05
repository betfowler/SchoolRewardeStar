using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStar.Controllers
{
    
    public class IndexController : Controller
    {
        private eStarContext db = new eStarContext();

        public ActionResult Index()
        {
            return View();
        }
        // GET: Index
        public ActionResult StudentIndex()
        {
            return View();
        }

        public ActionResult StaffIndex()
        {
            Staff staff = new Staff();

            int currentID = Convert.ToInt32(Session["UserID"]);

            staff.Awards = db.Accounts.OfType<Staff>().Where(acc => acc.User_ID.Equals(currentID)).FirstOrDefault().Awards.ToList();

            return View(staff);
        }

        public ActionResult GuardianIndex()
        {
            return View();
        }

        public ActionResult AdminIndex()
        {
            return View();
        }
    }
}