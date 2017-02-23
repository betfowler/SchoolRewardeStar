using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace eStar.Controllers
{
    public class StaffsController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Staffs
        public ActionResult Index()
        {
            return View(db.Accounts.OfType<Staff>().ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Accounts.OfType<Staff>().SingleOrDefault(s => s.User_ID == id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type,Job_Role,Weekly_Points,Remaining_Points,Admin")] Staff staff)
        {
            if (ModelState.IsValid)
            {

                if (staff.Admin.Equals(true))
                    staff.User_Type = "Admin";
                else
                    staff.User_Type = "Staff";
                db.Accounts.Add(staff);
                db.SaveChanges();

                //send email
                var body = "<p>Hi {0}, </p><p>An account has been set up for the email address: {1} with eStar.</p><p>Please go to the eStar site and register a password. </p><p> Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li><li>Job Role: {5}</li><li>Weekly Point Allocation: {6}</li><li>Remaining Points: {7}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                string messageBody = string.Format(body, staff.First_Name, staff.Email, staff.Prefix, staff.Surname, staff.User_Type, staff.Job_Role, staff.Weekly_Points, staff.Remaining_Points);
                string to = "bethany.fowler14@gmail.com"; //Change to staff.Email when finished testing
                string from = "estar.smtp.fowler@gmail.com";
                string subject = "eStar Account Registration";

                await SendMessage(to, from, messageBody, subject);
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Accounts.OfType<Staff>().SingleOrDefault(s => s.User_ID == id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Edit([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type,Job_Role,Weekly_Points,Remaining_Points")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                var body = "";

                //if password reset selected
                if (Request.Form["reset"] != null)
                {
                    staff.Password = null;
                    body = "<p>Hi {0}, </p><p>The password for the email address: {1} has been reset.</p><p>Please go to the eStar site and register a new password. </p><p> Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li><li>Job Role: {5}</li><li>Weekly Point Allocation: {6}</li><li>Remaining Points: {7}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                }
                else
                    body = "<p>Hi {0}, </p><p>The following changes have been made to this account: {1} with eStar.</p><p> Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li><li>Job Role: {5}</li><li>Weekly Point Allocation: {6}</li><li>Remaining Points: {7}</li></ul></p><p>Thank you.</p><p>eStar</p>";



                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();

                //send email
                string messageBody = string.Format(body, staff.First_Name, staff.Email, staff.Prefix, staff.Surname, staff.User_Type, staff.Job_Role, staff.Weekly_Points, staff.Remaining_Points);
                string to = "bethany.fowler14@gmail.com"; //Change to staff.Email when finished testing
                string from = "estar.smtp.fowler@gmail.com";
                string subject = "eStar Account Changes";

                await SendMessage(to, from, messageBody, subject);

                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Accounts.OfType<Staff>().SingleOrDefault(s => s.User_ID == id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }


        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Accounts.OfType<Staff>().SingleOrDefault(s => s.User_ID == id);
            db.Accounts.Remove(staff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SendMessage(string to, string from, string body, string subject)
        {
            //send email
            var message = new MailMessage();
            message.Body = body;
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(from);
            message.Subject = subject;
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
