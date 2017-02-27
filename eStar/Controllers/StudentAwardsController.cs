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
    public class StudentAwardsController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: StudentAwards
        public ActionResult Index()
        {
            return View(db.Accounts.OfType<Student>().ToList());
        }

        // GET: StudentAwards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAward studentAward = db.StudentAwards.Find(id);
            if (studentAward == null)
            {
                return HttpNotFound();
            }
            return View(studentAward);
        }

        // GET: StudentAwards/Create
        public ActionResult Create(int? studentid)
        {
            StudentAward sAward = new StudentAward
            {
                Student_ID = Convert.ToInt32(studentid)
            };
            return View(sAward);
        }

        // POST: StudentAwards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentAward_ID,Student_ID,Award_ID")] StudentAward studentAward)
        {
            if (ModelState.IsValid)
            {
                db.StudentAwards.Add(studentAward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentAward);
        }

        // GET: StudentAwards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAward studentAward = db.StudentAwards.Find(id);
            if (studentAward == null)
            {
                return HttpNotFound();
            }
            return View(studentAward);
        }

        // POST: StudentAwards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentAward_ID,Student_ID,Award_ID")] StudentAward studentAward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentAward);
        }

        // GET: StudentAwards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAward studentAward = db.StudentAwards.Find(id);
            if (studentAward == null)
            {
                return HttpNotFound();
            }
            return View(studentAward);
        }

        // POST: StudentAwards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAward studentAward = db.StudentAwards.Find(id);
            db.StudentAwards.Remove(studentAward);
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
