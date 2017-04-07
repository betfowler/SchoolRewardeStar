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
        public ActionResult Create(string staffSearch, string studentSearch, string className, string warning, string student, string staff)
        {
            EnrolmentViewModel evm = new EnrolmentViewModel();
            evm.Staff = db.Accounts.OfType<Staff>().ToList();
            evm.Students = db.Accounts.OfType<Student>().ToList();
            ViewBag.className = className;
            ViewBag.Students = student;
            ViewBag.Staff = staff;
            if (!String.IsNullOrEmpty(staffSearch))
            {
                var search = staffSearch.ToUpper();
                evm.Staff = db.Accounts.OfType<Staff>().Where(st => st.First_Name.ToUpper().Contains(search) || st.Surname.ToUpper().Contains(search)).ToList();
            }
            if (!String.IsNullOrEmpty(studentSearch))
            {
                var search = studentSearch.ToUpper();
                evm.Students = db.Accounts.OfType<Student>().Where(st => st.First_Name.ToUpper().Contains(search) || st.Surname.ToUpper().Contains(search)).ToList();
            }
            if (!String.IsNullOrEmpty(warning))
            {
                ViewBag.Error = warning;
            }
            if (!String.IsNullOrEmpty(staff))
            {
                List<string> staffMembers = staff.Split(',').ToList<string>();
                foreach(var name in staffMembers)
                {
                    var exists = false;
                    var id = Convert.ToInt32(name);
                    foreach(var listedStaff in evm.Staff)
                    {
                        if(listedStaff.User_ID == id)
                        {
                            exists = true;
                        }
                    }
                    if(exists == false)
                    {
                        evm.Staff.Add(db.Accounts.OfType<Staff>().Where(st => st.User_ID.Equals(id)).FirstOrDefault());
                    }
                }
            }
            if (!String.IsNullOrEmpty(student))
            {
                List<string> studentMembers = student.Split(',').ToList<string>();
                foreach (var name in studentMembers)
                {
                    var exists = false;
                    var id = Convert.ToInt32(name);
                    foreach (var listedStudent in evm.Students)
                    {
                        if (listedStudent.User_ID == id)
                        {
                            exists = true;
                        }
                    }
                    if (exists == false)
                    {
                        evm.Students.Add(db.Accounts.OfType<Student>().Where(st => st.User_ID.Equals(id)).FirstOrDefault());
                    }
                }
            }

            return View(evm);
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Class_ID,Class_Name")] Class @class, List<int?> student, List<int?> staff)
        {
            var name = db.Classes.Where(cl => cl.Class_Name.Equals(@class.Class_Name)).FirstOrDefault();
            if (@class.Class_Name == null || name != null || student == null || staff == null)
            {
                var warning = "";
                if(@class.Class_Name == null)
                {
                    warning = "Please enter a class name";
                }
                else if(name != null)
                {
                    warning = "The class name '" + @class.Class_Name + "' already exists";
                }
                else if(student == null)
                {
                    warning = "Please add students to the class";
                }
                else
                {
                    warning = "Please add staff to the class";
                }
                
                var students = "";
                var staffs = "";
                if(student != null)
                {
                    foreach (var s in student)
                    {
                        students = students + s + ",";
                    }
                    students = students.Remove(students.Length - 1);
                }
                if(staff != null)
                {
                    foreach (var s in staff)
                    {
                        staffs = staffs + s + ",";
                    }
                    staffs = staffs.Remove(staffs.Length - 1);
                }
                return RedirectToAction("Create", new { warning= warning, className = @class.Class_Name, student = students, staff = staffs});
            }
            else
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
                foreach (var row in staff)
                {
                    var id = Convert.ToInt32(row);
                    classStaff.Class_ID = @class.Class_ID;
                    classStaff.User_ID = id;
                    db.ClassStaffs.Add(classStaff);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id, string staffSearch, string studentSearch, string className, string warning, string student, string staff)
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
            evm.Staff = db.Accounts.OfType<Staff>().ToList();
            evm.Students = db.Accounts.OfType<Student>().ToList();
            var classID = Convert.ToInt32(id);
            evm.Class = db.Classes.Find(classID);

            if (String.IsNullOrEmpty(className))
            {
                student = "";
                staff = "";
                foreach (var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(classID)))
                {
                    student = student + enrolment.User_ID + ",";
                }
                foreach (var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(classID)))
                {
                    staff = staff + classStaff.User_ID + ",";
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(staffSearch))
                {
                    var search = staffSearch.ToUpper();
                    evm.Staff = db.Accounts.OfType<Staff>().Where(st => st.First_Name.ToUpper().Contains(search) || st.Surname.ToUpper().Contains(search)).ToList();
                }
                if (!String.IsNullOrEmpty(studentSearch))
                {
                    var search = studentSearch.ToUpper();
                    evm.Students = db.Accounts.OfType<Student>().Where(st => st.First_Name.ToUpper().Contains(search) || st.Surname.ToUpper().Contains(search)).ToList();
                }
                if (!String.IsNullOrEmpty(staff))
                {
                    if (staff.EndsWith(","))
                    {
                        staff = staff.Remove(staff.Length - 1);
                    }
                    List<string> staffMembers = staff.Split(',').ToList<string>();
                    foreach (var name in staffMembers)
                    {
                        var exists = false;
                        var sid = Convert.ToInt32(name);
                        foreach (var listedStaff in evm.Staff)
                        {
                            if (listedStaff.User_ID == sid)
                            {
                                exists = true;
                            }
                        }
                        if (exists == false)
                        {
                            evm.Staff.Add(db.Accounts.OfType<Staff>().Where(st => st.User_ID.Equals(sid)).FirstOrDefault());
                        }
                    }
                }
                if (!String.IsNullOrEmpty(student))
                {
                    if (student.EndsWith(","))
                    {
                        student = student.Remove(student.Length - 1);
                    }
                    List<string> studentMembers = student.Split(',').ToList<string>();
                    foreach (var name in studentMembers)
                    {
                        var exists = false;
                        var sid = Convert.ToInt32(name);
                        foreach (var listedStudent in evm.Students)
                        {
                            if (listedStudent.User_ID == sid)
                            {
                                exists = true;
                            }
                        }
                        if (exists == false)
                        {
                            evm.Students.Add(db.Accounts.OfType<Student>().Where(st => st.User_ID.Equals(sid)).FirstOrDefault());
                        }
                    }
                }
            }
            
            ViewBag.Students = student;
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
            db.Entry(@class).State = EntityState.Modified;
            db.SaveChanges();

            //if student/staff removed
            foreach(var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(@class.Class_ID)))
            {
                var removed = true;
                foreach (var studentId in student)
                {
                    if(Convert.ToInt32(enrolment.User_ID) == studentId)
                    {
                        removed = false;
                    }
                }

                if(removed == true)
                {
                    db.Enrolments.Remove(enrolment);
                }
            }

            foreach(var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(@class.Class_ID)))
            {
                var removed = true;
                foreach(var staffId in staff)
                {
                    if(Convert.ToInt32(classStaff.User_ID) == staffId)
                    {
                        removed = false;
                    }
                }

                if(removed == true)
                {
                    db.ClassStaffs.Remove(classStaff);
                }
            }

            //if student/staff added
            foreach(var studentId in student)
            {
                var @new = true;
                foreach(var enrolment in db.Enrolments.Where(e => e.Class_ID.Equals(@class.Class_ID)))
                {
                    if(Convert.ToInt32(enrolment.User_ID) == studentId)
                    {
                        @new = false;
                    }
                }
                if(@new == true)
                {
                    Enrolment enroll = new Enrolment();
                    enroll.User_ID = Convert.ToInt32(studentId);
                    enroll.Class_ID = @class.Class_ID;
                    db.Enrolments.Add(enroll);
                    db.SaveChanges();
                }
            }

            foreach (var staffId in staff)
            {
                var @new = true;
                foreach (var classStaff in db.ClassStaffs.Where(cs => cs.Class_ID.Equals(@class.Class_ID)))
                {
                    if (Convert.ToInt32(classStaff.User_ID) == staffId)
                    {
                        @new = false;
                    }
                }
                if (@new == true)
                {
                    ClassStaff staffClass = new ClassStaff();
                    staffClass.User_ID = Convert.ToInt32(staffId);
                    staffClass.Class_ID = @class.Class_ID;
                    db.ClassStaffs.Add(staffClass);
                    db.SaveChanges();
                }
            }

            db.SaveChanges();

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
