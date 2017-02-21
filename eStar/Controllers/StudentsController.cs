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
using System.Net.Mail;

namespace eStar.Controllers
{
    public class StudentsController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Accounts.OfType<Student>().ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Accounts.OfType<Student>().SingleOrDefault(s => s.User_ID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }
         
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type,Year_Group,Tutor_Group,Total_Points,Balance")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.User_Type = "Student";
                db.Accounts.Add(student);
                db.SaveChanges();

                //send email
                var body = "<p>Email From: eStar {0} </p><p>Hi {1}, </p><p>An account has been set up for the email address: {2} with eStar.</p><p>Please go to the eStar site and register a password. </p><p> Account details:<ul><li>Name: {3} {4} {5}</li><li>User: {6}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("bethany.fowler14@gmail.com"));
                message.From = new MailAddress("estar.smtp.fowler@gmail.com");
                message.Subject = "eStar email";
                message.Body = string.Format(body, message.From, student.First_Name, student.Email, student.Prefix, student.First_Name, student.Surname, student.User_Type);

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "estar.smtp.fowler@gmail.com",
                        Password = "16eStar17"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }


                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Accounts.OfType<Student>().SingleOrDefault(s => s.User_ID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type,Year_Group,Tutor_Group,Total_Points,Balance")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Accounts.OfType<Student>().SingleOrDefault(s => s.User_ID == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Accounts.OfType<Student>().SingleOrDefault(s => s.User_ID == id);
            db.Accounts.Remove(student);
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
