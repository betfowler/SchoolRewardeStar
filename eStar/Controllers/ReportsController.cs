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

        public ActionResult All_Students(DateTime? start, DateTime? end, string User_ID)
        {
            PopulateStudentDropDownList();
            if (!String.IsNullOrEmpty(User_ID))
            {

            }
            
            return View(db.Accounts.OfType<Student>().OrderByDescending(st => st.Total_Points).ToList());
        }
        public ActionResult userPointDetails(DateTime? start, DateTime? end, string User_ID, string all)
        {
            PopulateStudentDropDownList();
            List<Award> userAward = new List<Award>();
            List<Award> sAward = new List<Award>();
            if (!String.IsNullOrEmpty(User_ID))
            {
                int userID = Convert.ToInt32(User_ID);
                foreach (var stuAward in db.StudentAwards.Where(sa => sa.Student_ID.Equals(userID)))
                {
                    userAward.Add(db.Awards.Where(aw => aw.Award_ID.Equals(stuAward.Award_ID)).FirstOrDefault());
                    
                }
                if(userAward != null)
                {
                    if (!String.IsNullOrEmpty(all))
                    {
                        foreach (var award in userAward)
                        {
                            sAward.Add(award);
                        }
                    }
                    else
                    {
                        foreach (var award in userAward.Where(aw => aw.AwardDate >= start && aw.AwardDate <= end))
                        {
                            sAward.Add(award);
                        }
                    }
                }
            }
            
            return View(sAward);
        }
        private void PopulateStudentDropDownList(object selectedStudent = null)
        {
            var query = from s in db.Accounts.OfType<Student>()
                        orderby s.First_Name
                        select s;

            ViewBag.User_ID = new SelectList(query, "User_ID", "FullName", selectedStudent);
        }
        public ActionResult Tutor_Groups()
        {   
            return View(db.Accounts.OfType<Student>().ToList());
        }

        public ActionResult getTutorGraph(string names, string values, string title, int width)
        {
            List<string> name = names.Split(',').ToList<string>();
            List<string> value = values.Split(',').ToList<string>();
            var count = name.Count();

            List<string> xValues = new List<string>();
            List<int> yValues = new List<int>();

            for (var i =0; i<count; i++)
            {
                xValues.Add(name[i]);
                yValues.Add(Convert.ToInt32(value[i]));
            }
            var key = new Chart(width: width, height: 600)
                .AddTitle(title)
                .AddSeries(
                chartType: "Column",
                xValue: xValues,
                yValues: yValues);

            return File(key.ToWebImage().GetBytes(), "image/jpeg");
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