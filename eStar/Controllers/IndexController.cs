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

            award = award.OrderByDescending(aw => aw.AwardDate).Take(5).ToList();

            ViewBag.ProductCategory_ID = new SelectList(db.ProductCategories, "ProductCategory_ID", "CategoryName");
            return View(award.ToList());
        }

        public ActionResult StaffIndex()
        {
            List<Class> classes = new List<Class>();
            int currentID = Convert.ToInt32(SessionPersister.UserID);

            var classList = db.ClassStaffs.Where(cs => cs.User_ID.Equals(currentID)).ToList();

            foreach(var @class in classList)
            {
                classes.Add(db.Classes.Where(cl => cl.Class_ID.Equals(@class.Class_ID)).FirstOrDefault());
            }

            ViewBag.classID = new SelectList(db.Classes, "Class_ID", "Class_Name");
            return View(classes);
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