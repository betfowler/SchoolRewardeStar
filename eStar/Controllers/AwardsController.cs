﻿using System;
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

        public ActionResult ClassAwards(string classRadio, string sortOrder, string searchString)
        {
            List<Class> classes = new List<Class>();
            ViewBag.ClassNameSortParm = String.IsNullOrEmpty(sortOrder) ? "className_desc" : "";
            ViewBag.Search = searchString;


            if (classRadio == "userClasses")
            {
                int currentID = Convert.ToInt32(Session["UserID"]);
                List<int> classIDs = new List<int>();
                foreach(var row in db.ClassStaffs)
                {
                    if (row.User_ID == currentID)
                    {
                        classIDs.Add(row.Class_ID);
                    }
                }

                foreach(var @class in classIDs)
                {
                    classes.Add(db.Classes.Where(cl => cl.Class_ID.Equals(@class)).FirstOrDefault());
                }

                ViewBag.CheckedMy = "checked";

            }
            else
            {
                classes = db.Classes.ToList();
                ViewBag.CheckedAll = "checked";
                
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();

                classes = classes.Where(cl => cl.Class_Name.ToUpper().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "className_desc":
                    classes = classes.OrderByDescending(cl => cl.Class_Name).ToList();
                    break;
                default:
                    classes = classes.OrderBy(cl => cl.Class_Name).ToList();
                    break;
            }

            return View(classes);
        }

        // GET: Awards
        public ActionResult Index(string sortOrder, string searchString, int? classID, string yearGroup, string tutorGroup)
        {
            if (classID == 0)
                classID = null;

            ViewBag.ClassID = Convert.ToInt32(classID);
            ViewBag.Search = searchString;
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "FirstName_desc" : "FirstName";
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "Surname_desc" : "";
            ViewBag.YearGroupSortParm = sortOrder == "YearGroup" ? "YearGroup_desc" : "YearGroup";
            ViewBag.TutorGroupSortParm = sortOrder == "TutorGroup" ? "TutorGroup_desc" : "TutorGroup";
            ViewBag.ClassName = "All Students";
            

            List<Student> students = new List<Student>();
            if (classID != null)
            {

                List<int> studentIDs = new List<int>();
                List<Student> studentList = new List<Student>();
                int class_ID = Convert.ToInt32(classID);

                //get class name if ID is not null
                ViewBag.ClassName = db.Classes.Where(cl => cl.Class_ID.Equals(class_ID)).FirstOrDefault().Class_Name;

                foreach (var enrol in db.Enrolments.Where(en => en.Class_ID.Equals(class_ID)))
                {
                    studentIDs.Add(enrol.User_ID);
                }

                foreach (var student in studentIDs)
                {
                    studentList.Add(db.Accounts.OfType<Student>().Where(acc => acc.User_ID.Equals(student)).FirstOrDefault());

                }

                students = studentList;
            }
            else
                students = db.Accounts.OfType<Student>().ToList();

            if(yearGroup == "all" || yearGroup == null)
            {
                ViewBag.YearAll = "checked";
            }
            else
            {
                if(yearGroup == "7")
                {
                    ViewBag.year7 = "checked";
                    students = students.Where(st => st.Year_Group.Equals("7")).ToList();
                }
                if (yearGroup == "8")
                {
                    ViewBag.year8 = "checked";
                    students = students.Where(st => st.Year_Group.Equals("8")).ToList();
                }
                if (yearGroup == "9")
                {
                    ViewBag.year9 = "checked";
                    students = students.Where(st => st.Year_Group.Equals("9")).ToList();
                }
                if (yearGroup == "10")
                {
                    ViewBag.year10 = "checked";
                    students = students.Where(st => st.Year_Group.Equals("10")).ToList();
                }
                if (yearGroup == "11")
                {
                    ViewBag.year11 = "checked";
                    students = students.Where(st => st.Year_Group.Equals("11")).ToList();
                }
            }

            if (tutorGroup == "all" || tutorGroup == null)
            {
                ViewBag.TutorAll = "checked";
            }
            else
            {
                if (tutorGroup == "stella")
                {
                    ViewBag.Stella = "checked";
                    students = students.Where(st => st.Tutor_Group.Equals("Stella")).ToList();
                }
                if (tutorGroup == "etoile")
                {
                    ViewBag.Etoile = "checked";
                    students = students.Where(st => st.Tutor_Group.Equals("Étoile")).ToList();
                }
                if (tutorGroup == "estrella") 
                {
                    ViewBag.Estrella = "checked";
                    students = students.Where(st => st.Tutor_Group.Equals("Estrella")).ToList();
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();

                students = students.Where(s => s.FullName.ToUpper().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "FirstName_desc":
                    students = students.OrderByDescending(s => s.First_Name).ToList();
                    break;
                case "FirstName":
                    students = students.OrderBy(s => s.First_Name).ToList();
                    break;
                case "Surname_desc":
                    students = students.OrderByDescending(s => s.Surname).ToList();
                    break;
                case "YearGroup_desc":
                    students = students.OrderByDescending(s => s.Year_Group).ToList();
                    break;
                case "YearGroup":
                    students = students.OrderBy(s => s.Year_Group).ToList();
                    break;
                case "TutorGroup_desc":
                    students = students.OrderByDescending(s => s.Tutor_Group).ToList();
                    break;
                case "TutorGroup":
                    students = students.OrderBy(s => s.Tutor_Group).ToList();
                    break;
                default:
                    students = students.OrderBy(s => s.Surname).ToList();
                    break;
            }

            return View(students);
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
        public ActionResult Create(List<int?> student, int? classID)
        {
            if (student != null)
            {
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

                return View(award);
            }

            if(classID != null)
            {
                int class_ID = Convert.ToInt32(classID);
                Award award = new Award();
                AccountModel am = new AccountModel();
                award.Students = new List<int>();
                award.StudentNames = new List<string>();
                award.StudentCount = 0;
                
                foreach(var row in db.Enrolments.Where(en => en.Class_ID.Equals(class_ID)))
                {
                    award.Students.Add(row.User_ID);
                    award.StudentNames.Add(am.findUsingID(row.User_ID).FullName);
                    award.StudentCount = award.StudentCount + 1;
                }

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
