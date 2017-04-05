using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using eStar.Security;

namespace eStar.Controllers
{
    public class PledgesController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Pledges
        public ActionResult Index()
        {
            int id = Convert.ToInt32(SessionPersister.UserID);
            return View(db.Pledges.Where(p => p.Guardians_User_ID.Equals(id)).ToList());
        }

        // GET: Pledges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pledge pledge = db.Pledges.Find(id);
            if (pledge == null)
            {
                return HttpNotFound();
            }
            return View(pledge);
        }

        // GET: Pledges/Create
        public ActionResult Create(int guardianID)
        {
            PopulateStudentDropDownList();
            return View();
        }

        private void PopulateStudentDropDownList(object selectedStudent = null)
        {
            int id = Convert.ToInt32(SessionPersister.UserID);
            List<Student> students = new List<Student>();
            foreach(var student in db.StudentGuardians.Where(sg => sg.Guardian_User_ID.Equals(id)))
            {
                var studentid = Convert.ToInt32(student.Student_User_ID);
                students.Add(db.Accounts.OfType<Student>().Where(s => s.User_ID.Equals(studentid)).FirstOrDefault());
            }

            var query = from s in students
                        orderby s.First_Name
                        select s;

            ViewBag.User_ID = new SelectList(query, "User_ID", "FullName", selectedStudent);
        }

        // POST: Pledges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PledgeID,Students_User_ID,Guardians_User_ID,Target,Deadline,Title,Description")] Pledge pledge)
        {
            if (ModelState.IsValid)
            {
                db.Pledges.Add(pledge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pledge);
        }

        // GET: Pledges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pledge pledge = db.Pledges.Find(id);
            if (pledge == null)
            {
                return HttpNotFound();
            }
            return View(pledge);
        }

        // POST: Pledges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PledgeID,Students_User_ID,Guardians_User_ID,Target,Deadline,Title,Description")] Pledge pledge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pledge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pledge);
        }

        // GET: Pledges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pledge pledge = db.Pledges.Find(id);
            if (pledge == null)
            {
                return HttpNotFound();
            }
            return View(pledge);
        }

        // POST: Pledges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pledge pledge = db.Pledges.Find(id);
            db.Pledges.Remove(pledge);
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
