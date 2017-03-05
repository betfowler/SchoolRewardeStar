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
    public class ClassesController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Classes
        public ActionResult Index()
        {
            int currentID = Convert.ToInt32(Session["UserID"]);

            if(Session["UserType"].ToString() == "Staff")
            {
                var userClasses = db.Accounts.OfType<Staff>().Where(acc => acc.User_ID.Equals(currentID)).FirstOrDefault().ClassStaff.ToList();
                return View(userClasses);
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
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult getStudents(string className)
        {
            int classID = db.Classes.Where(cl => cl.Class_Name.Equals(className)).FirstOrDefault().Class_ID;
            var students = db.Accounts.OfType<Student>().ToList();
            //want to pass className and students to the view
            //getStaff to save to enrolments
            return View(students);
        }

        public ActionResult getStaff(List<int?> student, int classID)
        {
            var staff = db.Accounts.OfType<Staff>().ToList();
            //save selected students and classID to enrolments
            if(student != null)
            {
                //create enrolments
                for(var i=0; i<student.Count; i++)
                {
                    Enrolment enrolment = new Enrolment();
                    enrolment.Class.Class_ID = classID;
                    enrolment.Student.User_ID = Convert.ToInt32(student[i]);
                    //db.Enrolments.Add(enrolment);
                }

                //db.SaveChanges();
                return View(staff);

            }
            return RedirectToAction("getStudents", new { className = classID });
        }

        public ActionResult completeClass(List<int?>staff, int classID)
        {
            if(staff != null)
            {
                for(var i=0; i<staff.Count; i++)
                {
                    ClassStaff classStaff = new ClassStaff();
                    classStaff.Class.Class_ID = classID;
                    classStaff.Staff.User_ID = Convert.ToInt32(staff);
                    //db.ClassStaffs.Add(classStaff);
                }
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("getStaff", new { className = classID });
        }


        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_ID,Class_Name")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                db.SaveChanges();
                return RedirectToAction("getStudents", new { className = @class.Class_Name}); 
            }

            return View(@class);
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
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Class_ID,Class_Name")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@class);
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
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
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
