using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eStar.Models;
using System.Threading.Tasks;
using System.Net.Mail;

namespace eStar.Controllers
{
    public class GuardiansController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Guardians
        public ActionResult Index()
        {
            return View(db.Accounts.OfType<Guardian>().ToList());
        }

        // GET: Guardians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = db.Accounts.OfType<Guardian>().SingleOrDefault(g => g.User_ID == id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }

        // GET: Guardians/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guardians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                guardian.User_Type = "Guardian";
                db.Accounts.Add(guardian);
                db.SaveChanges();

                //send email
                var body = "<p>Hi {0}, </p><p>An account has been set up for the email address: {1} with eStar.</p><p>Please go to the eStar site and register a password. </p><p> Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                string messageBody = string.Format(body, guardian.First_Name, guardian.Email, guardian.Prefix, guardian.Surname, guardian.User_Type);
                string to = "bethany.fowler14@gmail.com"; //change to guardian.Email when finished testing
                string from = "estar.smtp.fowler@gmail.com";
                string subject = "eStar Account Registration";

                await SendMessage(to, from, messageBody, subject);

                return RedirectToAction("Index");
            }

            return View(guardian);
        }

        // GET: Guardians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = db.Accounts.OfType<Guardian>().SingleOrDefault(g => g.User_ID == id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }

        // POST: Guardians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                //if password reset selected
                if (Request.Form["reset"] != null)
                {
                    guardian.Password = null;
                }
                db.Entry(guardian).State = EntityState.Modified;
                db.SaveChanges();

                //send email
                var body = "<p>Hi {0}, </p><p>The following changes have been made to the account: {1} with eStar.</p><p>Account details:<ul><li>Name: {2} {0} {3}</li><li>User: {4}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                string messageBody = string.Format(body, guardian.First_Name, guardian.Email, guardian.Prefix, guardian.Surname, guardian.User_Type);
                string to = "bethany.fowler14@gmail.com"; //change to guardian.Email when finished testing
                string from = "estar.smtp.fowler@gmail.com";
                string subject = "eStar Account Changes";

                await SendMessage(to, from, messageBody, subject);

                return RedirectToAction("Index");
            }
            return View(guardian);
        }

        // GET: Guardians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guardian guardian = db.Accounts.OfType<Guardian>().SingleOrDefault(g => g.User_ID == id);
            if (guardian == null)
            {
                return HttpNotFound();
            }
            return View(guardian);
        }

        // POST: Guardians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guardian guardian = db.Accounts.OfType<Guardian>().SingleOrDefault(g => g.User_ID == id);
            db.Accounts.Remove(guardian);
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
