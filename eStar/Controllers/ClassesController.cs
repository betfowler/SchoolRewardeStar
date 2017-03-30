using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using eStar.ViewModels;

namespace eStar.Controllers
{
    public class ClassesController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Classes
        public ActionResult Index()
        {
            int currentID = Convert.ToInt32(Session["UserID"]);

            if(Session["UserType"].ToString() == "Staff")
            {
                List<Class> userClasses = new List<Class>();
                List<int> classes = new List<int>();
                foreach(var @class in db.ClassStaffs)
                {
                    if(@class.User_ID == currentID)
                    {
                        classes.Add(@class.Class_ID);
                    }
                }

                if(classes.Count > 0)
                {
                    for(var i=0; i < classes.Count; i++)
                    {
                        int classID = Convert.ToInt32(classes[i]);
                        userClasses.Add(db.Classes.Where(cl => cl.Class_ID.Equals(classID)).FirstOrDefault());
                    }
                    return View(userClasses.ToList());
                }
            }
            return View(db.Classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }

            EnrolmentViewModel evm = new EnrolmentViewModel();
            List<Student> students = new List<Student>();
            List<Staff> staff = new List<Staff>();
            var classID = Convert.ToInt32(id);
            evm.Class = db.Classes.Find(classID);
            foreach(var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(classID)))
            {
                students.Add(enrolment.Student);
            }
            foreach(var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(classID)))
            {
                staff.Add(classStaff.Staff);
            }
            evm.Students = students;
            evm.Staff = staff;

            return View(evm);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            EnrolmentViewModel evm = new EnrolmentViewModel();
            evm.Staff = db.Accounts.OfType<Staff>().ToList();
            evm.Students = db.Accounts.OfType<Student>().ToList();
            return View(evm);
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_ID,Class_Name")] Class @class, List<int?> student, List<int?> staff)
        {
                Enrolment enrolment = new Enrolment();
                ClassStaff classStaff = new ClassStaff();
                db.Classes.Add(@class);
                db.SaveChanges();

                foreach (var row in student)
                {
                    var id = Convert.ToInt32(row);
                    enrolment.Class_ID = @class.Class_ID;
                    enrolment.User_ID = id;
                    db.Enrolments.Add(enrolment);
                    db.SaveChanges();
                }
                foreach(var row in staff)
                {
                    var id = Convert.ToInt32(row);
                    classStaff.Class_ID = @class.Class_ID;
                    classStaff.User_ID = id;
                    db.ClassStaffs.Add(classStaff);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }

            EnrolmentViewModel evm = new EnrolmentViewModel();
            string students = "";
            string staff = "";
            var classID = Convert.ToInt32(id);
            evm.Class = db.Classes.Find(classID);
            foreach (var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(classID)))
            {
                students = students + enrolment.User_ID + ",";
            }
            foreach (var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(classID)))
            {
                staff = staff + classStaff.User_ID + ",";
            }
            evm.Staff = db.Accounts.OfType<Staff>().ToList();
            evm.Students = db.Accounts.OfType<Student>().ToList();
            ViewBag.Students = students;
            ViewBag.Staff = staff;
            return View(evm);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Class_ID,Class_Name")] Class @class, List<int?> student, List<int?> staff)
        {
            Enrolment enrolment = new Enrolment();
            ClassStaff classStaff = new ClassStaff();
            db.Entry(@class).State = EntityState.Modified;
            db.SaveChanges();

            foreach (var row in student)
            {
                var id = Convert.ToInt32(row);
                enrolment.Class_ID = @class.Class_ID;
                enrolment.User_ID = id;
                db.Entry(enrolment).State = EntityState.Modified;
                db.SaveChanges();
            }
            foreach (var row in staff)
            {
                var id = Convert.ToInt32(row);
                classStaff.Class_ID = @class.Class_ID;
                classStaff.User_ID = id;
                db.ClassStaffs
                db.ClassStaffs.Add(classStaff);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }

            EnrolmentViewModel evm = new EnrolmentViewModel();
            List<Student> students = new List<Student>();
            List<Staff> staff = new List<Staff>();
            var classID = Convert.ToInt32(id);
            evm.Class = db.Classes.Find(classID);
            foreach (var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(classID)))
            {
                students.Add(enrolment.Student);
            }
            foreach (var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(classID)))
            {
                staff.Add(classStaff.Staff);
            }
            evm.Students = students;
            evm.Staff = staff;
            return View(evm);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            foreach (var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(id)))
            {
                db.Enrolments.Remove(enrolment);
            }
            foreach (var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(id)))
            {
                db.ClassStaffs.Remove(classStaff);
            }
            db.Classes.Remove(@class);
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
