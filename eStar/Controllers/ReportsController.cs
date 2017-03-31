using eStar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace eStar.Controllers
{
    public class ReportsController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult All_Students()
        {
            return View(db.Accounts.OfType<Student>().OrderByDescending(st => st.Total_Points).ToList());
        }

        public ActionResult GetTopTen()
        {
            var top10 = db.Accounts.OfType<Student>().OrderByDescending(st => st.Total_Points).Take(10).ToList();
            return Content(top10);
        }

        public ActionResult GetChartImage()
        {
            var top10 = db.Accounts.OfType<Student>().OrderByDescending(st => st.Total_Points).Take(10).ToList();
            List<string> names = new List<string>();
            List<int> points = new List<int>();

            foreach(var student in top10)
            {
                names.Add(student.FullName);
                points.Add(student.Total_Points);
            }

            var key = new Chart(width: 600, height: 600)
                .AddTitle("Top 10 Students")
                .AddSeries(
                chartType: "Column",
                name: "Employee",
                xValue: new[] { names[0], names[1], names[2], names[3], names[4], names[5], names[6], names[7], names[8], names[9] },
                yValues: new[] { points[0], points[1], points[2], points[3], points[4], points[5], points[6], points[7], points[8], points[9] });
            return File(key.ToWebImage().GetBytes(), "image/jpeg");
        }
    }
}