using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStar.Controllers
{
    public class StudentAwardController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: StudentAward
        public ActionResult Index()
        {
            return View(db.Accounts.OfType<Student>().ToList());
        }
    }
}