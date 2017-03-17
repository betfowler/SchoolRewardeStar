using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eStar.Security;

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
            int userid = SessionPersister.UserID;
            List<Award> award = new List<Award>();

            var studentAwardList = db.StudentAwards.Where(sa => sa.Student_ID.Equals(userid)).ToList();
            
            foreach(var sAward in studentAwardList)
            {
                award.Add(db.Awards.Where(aw => aw.Award_ID.Equals(sAward.Award_ID)).FirstOrDefault());
            }

            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName");
            return View(award.ToList());
        }

        public ActionResult StaffIndex()
        {
            Staff staff = new Staff();

            int currentID = Convert.ToInt32(SessionPersister.UserID);

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