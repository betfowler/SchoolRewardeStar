using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;

namespace eStar.Controllers
{
    public class AwardsController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Awards
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Surname_desc" : "";
            ViewBag.YearGroupSortParm = sortOrder == "YearGroup" ? "YearGroup_desc" : "YearGroup";
            ViewBag.TutorGroupSortParm = sortOrder == "TutorGroup" ? "TutorGroup_desc" : "TutorGroup";

            var students = from s in db.Accounts.OfType<Student>()
                           select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();

                students = students.Where(s => s.Surname.ToUpper().Contains(search)
                || s.First_Name.ToUpper().Contains(search)
                || s.Year_Group.ToUpper().Contains(search)
                || s.Tutor_Group.ToUpper().Contains(search));
            }

            switch (sortOrder)
            {
                case "FirstName_desc":
                    students = students.OrderByDescending(s => s.First_Name);
                    break;
                case "FirstName":
                    students = students.OrderBy(s => s.First_Name);
                    break;
                case "Surname_desc":
                    students = students.OrderByDescending(s => s.Surname);
                    break;
                case "YearGroup_desc":
                    students = students.OrderByDescending(s => s.Year_Group);
                    break;
                case "YearGroup":
                    students = students.OrderBy(s => s.Year_Group);
                    break;
                case "TutorGroup_desc":
                    students = students.OrderByDescending(s => s.Tutor_Group);
                    break;
                case "TutorGroup":
                    students = students.OrderBy(s => s.Tutor_Group);
                    break;
                default:
                    students = students.OrderBy(s => s.Surname);
                    break;
            }

            return View(students.ToList());
        }

        // GET: Awards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // GET: Awards/Create
        public ActionResult Create(List<int?> student)
        {
            if (student != null)
            {
                //add students to award
                int selectedStudents = student.Count;
                Award award = new Award();
                AccountModel am = new AccountModel();
                award.StudentCount = selectedStudents;
                award.Students = new List<int>();
                award.StudentNames = new List<string>();

                for(var i = 0; i < selectedStudents; i++)
                {
                    int studentID = Convert.ToInt32(student[i]);
                    award.Students.Add(studentID);
                    award.StudentNames.Add(am.findUsingID(studentID).FullName);
                }

                award.Reward_Comment = "This is a test";
                return View(award);
            }

            return RedirectToAction("Index");
        }

        // POST: Awards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Award_ID,Staff_ID,Num_Points,Reward_Category_ID,Reward_Comment,Students,StudentCount")] Award award)
        {
            //get staff userID
            int userID = Convert.ToInt32(Session["UserID"]);
            award.Staff_ID = userID;

                //find staff remaining points
                int remainingPoints = db.Accounts.Where(acc => acc.User_ID.Equals(userID)).OfType<Staff>().FirstOrDefault().Remaining_Points;
                int points = award.Num_Points;
                int checkPoints = points * award.StudentCount;

                //if staff has enough points
                if(remainingPoints >= checkPoints)
                {
                    //update staff with new points value
                    Staff staff = db.Accounts.OfType<Staff>().SingleOrDefault(s => s.User_ID == userID);
                    staff.Remaining_Points = remainingPoints - points;
                    staff.Awards.Add(award);

                    db.Entry(staff).State = EntityState.Modified;

                    //save award
                    db.Awards.Add(award);

                    //for number of students selected
                    for (var i = 0; i < award.StudentCount; i++)
                    {
                        int studentID = Convert.ToInt32(award.Students[i]);
                        //create new StudentAwards
                        StudentAward sAward = new StudentAward();
                        sAward.Award_ID = award.Award_ID;
                        sAward.Student_ID = studentID;
                        db.StudentAwards.Add(sAward);

                        //update student with new points value
                        Student student = db.Accounts.OfType<Student>().SingleOrDefault(s => s.User_ID == studentID);
                        int currentBalance = student.Balance + points;
                        int totalPoints = student.Total_Points + points;
                        student.Balance = currentBalance;
                        student.Total_Points = totalPoints;

                        db.Entry(student).State = EntityState.Modified;

                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "You do not have enough points left this week to make this award, you have " + remainingPoints + " left.";
                    return View(award);
                }
        }

        // GET: Awards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // POST: Awards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Award_ID,Staff_ID,Num_Points,Reward_Category_ID,Reward_Comment")] Award award)
        {
            if (ModelState.IsValid)
            {
                db.Entry(award).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(award);
        }

        // GET: Awards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        // POST: Awards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Award award = db.Awards.Find(id);
            db.Awards.Remove(award);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
