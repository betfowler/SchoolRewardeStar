using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStar.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult StudentIndex()
        {
            return View();
        }

        public ActionResult StaffIndex()
        {
            return View();
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