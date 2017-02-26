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
    public class RewardCategoriesController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: RewardCategories
        public ActionResult Index()
        {
            return View(db.RewardCategories.ToList());
        }

        // GET: RewardCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RewardCategory rewardCategory = db.RewardCategories.Find(id);
            if (rewardCategory == null)
            {
                return HttpNotFound();
            }
            return View(rewardCategory);
        }

        // GET: RewardCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RewardCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reward_Category_ID,Reward_Category")] RewardCategory rewardCategory)
        {
            if (ModelState.IsValid)
            {
                db.RewardCategories.Add(rewardCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rewardCategory);
        }

        // GET: RewardCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RewardCategory rewardCategory = db.RewardCategories.Find(id);
            if (rewardCategory == null)
            {
                return HttpNotFound();
            }
            return View(rewardCategory);
        }

        // POST: RewardCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reward_Category_ID,Reward_Category")] RewardCategory rewardCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rewardCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rewardCategory);
        }

        // GET: RewardCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RewardCategory rewardCategory = db.RewardCategories.Find(id);
            if (rewardCategory == null)
            {
                return HttpNotFound();
            }
            return View(rewardCategory);
        }

        // POST: RewardCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RewardCategory rewardCategory = db.RewardCategories.Find(id);
            db.RewardCategories.Remove(rewardCategory);
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
