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
    public class StudentGuardiansController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: StudentGuardians
        public ActionResult Index()
        {
            return View(db.StudentGuardians.ToList());
        }

        // GET: StudentGuardians/Create
        public ActionResult Create(int? studentId, int? guardianId)
        {
            StudentGuardian studentGuardian = new StudentGuardian();
            var studentID = Convert.ToInt32(studentId);
            var guardianID = Convert.ToInt32(guardianId);
            studentGuardian.Student_User_ID = studentID;
            studentGuardian.Guardian_User_ID = guardianID;
            return View(studentGuardian);
        }

        //create with studentID
        public ActionResult CreateStudentGuardian(int studentId, string searchString)
        {
            ViewBag.Search = searchString;
            List<Guardian> guardians = new List<Guardian>();
            StudentGuardian studentGuardian = new StudentGuardian();
            studentGuardian.Student_User_ID = studentId;
            ViewBag.StudentName = db.Accounts.OfType<Student>().Where(s => s.User_ID.Equals(studentId)).FirstOrDefault().FullName;

            foreach(var guardian in db.Accounts.OfType<Guardian>())
            {
                var checkExisting = false;
                if(db.Accounts.OfType<Student>().Where(s => s.User_ID.Equals(studentId)).FirstOrDefault().GuardianCount != 0)
                {
                    foreach (var studentguardian in db.StudentGuardians.Where(sg => sg.Student_User_ID.Equals(studentId)))
                    {
                        if (guardian.User_ID == studentguardian.Guardian_User_ID)
                        {
                            checkExisting = true;
                        }
                    }
                }

                if(checkExisting == false || db.Accounts.OfType<Student>().Where(s => s.User_ID.Equals(studentId)).FirstOrDefault().GuardianCount == 0)
                {
                    guardians.Add(guardian);
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();
                guardians = guardians.Where(g => g.FullName.ToUpper().Contains(search)).ToList();
            }

            ViewBag.Guardians = guardians;

            return View(studentGuardian);
        }

        //create with guardianID
        public ActionResult CreateGuardianStudent(int guardianId, string searchString)
        {
            ViewBag.Search = searchString;
            List<Student> students = new List<Student>();
            StudentGuardian studentGuardian = new StudentGuardian();
            studentGuardian.Guardian_User_ID = guardianId;
            ViewBag.GuardianName = db.Accounts.OfType<Guardian>().Where(s => s.User_ID.Equals(guardianId)).FirstOrDefault().FullName;

            foreach (var s in db.Accounts.OfType<Student>())
            {
                var checkExisting = false;
                if (db.Accounts.OfType<Guardian>().Where(g => g.User_ID.Equals(guardianId)).FirstOrDefault().StudentCount != 0)
                {
                    foreach (var studentguardian in db.StudentGuardians.Where(sg => sg.Guardian_User_ID.Equals(guardianId)))
                    {
                        if (s.User_ID == studentguardian.Student_User_ID)
                        {
                            checkExisting = true;
                        }
                    }
                }

                if (checkExisting == false || db.Accounts.OfType<Guardian>().Where(g => g.User_ID.Equals(guardianId)).FirstOrDefault().StudentCount == 0)
                {
                    students.Add(s);
                }
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToUpper();
                students = students.Where(g => g.FullName.ToUpper().Contains(search)).ToList();
            }

            ViewBag.Students = students;

            return View(studentGuardian);
        }

        // POST: StudentGuardians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentGuardianID,Student_User_ID,Guardian_User_ID")] StudentGuardian studentGuardian)
        {
            if (ModelState.IsValid)
            {
                db.StudentGuardians.Add(studentGuardian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentGuardian);
        }

        // GET: StudentGuardians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            if (studentGuardian == null)
            {
                return HttpNotFound();
            }
            return View(studentGuardian);
        }

        // POST: StudentGuardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGuardian studentGuardian = db.StudentGuardians.Find(id);
            db.StudentGuardians.Remove(studentGuardian);
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
