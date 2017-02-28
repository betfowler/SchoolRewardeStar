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
        public ActionResult Index()
        {
            return View(db.Accounts.OfType<Student>().ToList());
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
        public ActionResult Create(int? id)
        {
            Award award = new Award();
            award.StudentCount = 1;

            award.Students = new List<int>();
            award.Students.Add(Convert.ToInt32(id));
            award.Reward_Comment = "This is a test";
            return View(award);
        }

        // POST: Awards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Award_ID,Staff_ID,Num_Points,Reward_Category_ID,Reward_Comment,Students,StudentCount")] Award award)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            award.Staff_ID = userID;
            if (ModelState.IsValid)
            {
                //find staff balance
                var balance = db.Accounts.Where(acc => acc.User_ID.Equals(userID)).FirstOrDefault();


                db.Awards.Add(award);
                db.SaveChanges();

                //create new StudentAwards
                StudentAward sAward = new StudentAward();
                sAward.Award_ID = award.Award_ID;
                sAward.Student_ID = award.Students[0];
                db.StudentAwards.Add(sAward);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(award);
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
