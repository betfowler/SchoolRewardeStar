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
using eStar.Security;
//using System.Web.Helpers;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.IO;

namespace eStar.Controllers
{
    public class StudentsController : Controller
    {
        private eStarContext db = new eStarContext();

        // GET: Students
        public ActionResult Index()
        {
            Student student = new Student();
            foreach (var students in db.Accounts.OfType<Student>().ToList())
            {
                students.StudentGuardians = db.StudentGuardians.Where(sg => sg.Student_User_ID.Equals(students.User_ID)).ToList();
                student = db.Accounts.OfType<Student>().Where(s => s.User_ID.Equals(students.User_ID)).FirstOrDefault();
                student.GuardianCount = students.StudentGuardians.Count();
                foreach (var guardian in students.StudentGuardians)
                {
                    guardian.Guardians = db.Accounts.OfType<Guardian>().Where(g => g.User_ID.Equals(guardian.Guardian_User_ID)).FirstOrDefault();
                }
            }
            db.SaveChanges();
            return View(db.Accounts.OfType<Student>().ToList());
        }

        public ActionResult UserProfile()
        {
            int userId = Convert.ToInt32(SessionPersister.UserID);
            var count = 1;
            foreach (var student in db.Accounts.OfType<Student>().OrderByDescending(m => m.Total_Points))
            {
                if (student.User_ID.Equals(userId))
                {
                    if (count == 1)
                    {
                        ViewBag.Position = "1st";
                    }
                    else if (count == 2)
                    {
                        ViewBag.Position = "2nd";
                    }
                    else if (count == 3)
                    {
                        ViewBag.Position = "3rd";
                    }
                    else
                    {
                        ViewBag.Position = count + "th";
                    }
                }
                count = count + 1;
            }
            List<Award> awards = new List<Award>();
            foreach(var award in db.StudentAwards.Where(sa => sa.Student_ID.Equals(userId)))
            {
                awards.Add(db.Awards.Where(aw => aw.Award_ID.Equals(award.Award_ID)).FirstOrDefault());
            }

            ViewBag.Awards = awards.ToList();
            ViewBag.Subjects = db.Subjects.ToList();
            ViewBag.Categories = db.RewardCategories.ToList();
            string subjectName = "";
            string catName = "";
            int[] number = new int[db.Subjects.Count()];
            int[] catNumber = new int[db.RewardCategories.Count()];
            for(var i=0; i<db.Subjects.Count(); i++)
            {  
                number[i] = 0;
            }
            for (var i = 0; i < db.RewardCategories.Count(); i++)
            {
                catNumber[i] = 0;
            }
            foreach (var subject in db.Subjects.ToList())
            {
                subjectName = subjectName + subject.Subject_Name + ",";
            }
            foreach (var category in db.RewardCategories.ToList())
            {
                catName = catName + category.Reward_Category + ",";
            }
            ViewBag.Number = number;
            ViewBag.SubjectNames = subjectName;
            ViewBag.CatNumber = catNumber;
            ViewBag.CatNames = catName;
            return View();
        }

        public ActionResult pieChart(string names, string values)
        {
            names = names.Remove(names.Length - 1);
            values = values.Remove(values.Length - 1);
            List<string> name = names.Split(',').ToList<string>();
            List<string> value = values.Split(',').ToList<string>();
            var val = name.Count();
            int count = Convert.ToInt32(val);

            List<string> xValues = new List<string>();
            List<int> yValues = new List<int>();

            for (var i = 0; i < count; i++)
            {
                if(Convert.ToInt32(value[i]) != 0){
                    xValues.Add(name[i]);
                    yValues.Add(Convert.ToInt32(value[i]));
                }
            }

            val = xValues.Count();
            count = Convert.ToInt32(val);

            Chart chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());
            chart.Height = 700;
            chart.Width = 900;

            chart.Series.Add(new Series("Data"));
            chart.Legends.Add(new Legend("Stores"));
            chart.Series["Data"].ChartType = SeriesChartType.Pie;
            chart.Series["Data"]["PieLabelStyle"] = "Outside";
            chart.Series["Data"]["PieLineColor"] = "Black";
            for (var i = 0; i < count; i++)
            {
                chart.Series["Data"].Points.AddXY(
                    xValues[i],
                    yValues[i]);
            }
            chart.Series["Data"].Font = new Font("Segoe UI", 12.0f, FontStyle.Bold);
            chart.Series["Data"].ChartType = SeriesChartType.Pie;
            chart.Series["Data"]["PieLabelStyle"] = "Outside";
            chart.Series["Data"].Label = "#VALX: #PERCENT{P2}";
            chart.Legends["Stores"].Docking = Docking.Bottom;

            var returnStream = new MemoryStream();
            chart.ImageType = ChartImageType.Png;
            chart.SaveImage(returnStream);
            returnStream.Position = 0;
            return new FileStreamResult(returnStream, "image/png");
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
            else
            {
                var userID = Convert.ToInt32(id);
                foreach (var row in db.StudentGuardians.Where(sg => sg.Student_User_ID.Equals(userID)).ToList())
                {
                    student.StudentGuardians.Add(row);
                }
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            Student student = new Student();
            student.Balance = 0;
            student.Total_Points = 0;
            return View(student);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type,Year_Group,Tutor_Group,Total_Points,Balance")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.User_Type = "Student";
                db.Accounts.Add(student);
                db.SaveChanges();

                //send email
                var body = "<p>Hi {0}, </p><p>An account has been set up for the email address: {1} with eStar.</p><p>Please go to the eStar site and register a password. </p><p> Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li><li>Year Group: {5}</li><li>Tutor Group: {6}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                string messageBody = string.Format(body, student.First_Name, student.Email, student.Prefix, student.Surname, student.User_Type, student.Year_Group, student.Tutor_Group);
                string to = "bethany.fowler14@gmail.com"; //Change to student.Email when finished testing
                string from = "estar.smtp.fowler@gmail.com";
                string subject = "eStar Account Registration";

                await SendMessage(to, from, messageBody, subject);

                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult removeStudentGuardian(int studentID, int guardianID)
        {
            var studentguardianid = db.StudentGuardians.Where(sg => sg.Student_User_ID.Equals(studentID) && sg.Guardian_User_ID.Equals(guardianID)).FirstOrDefault().StudentGuardianID;
            return RedirectToAction("Delete", "StudentGuardians", new { id = studentguardianid });
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
            else
            {
                var userID = Convert.ToInt32(id);
                foreach (var row in db.StudentGuardians.Where(sg => sg.Student_User_ID.Equals(userID)).ToList())
                {
                    student.StudentGuardians.Add(row);
                }
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "User_ID,Email,Password,Prefix,First_Name,Surname,User_Type,Year_Group,Tutor_Group,Total_Points,Balance")] Student student)
        {
            if (ModelState.IsValid)
            {
                var body = "";

                if(Request.Form["reset"] != null)
                {
                    student.Password = null;
                    body = "<p>Hi {0}, </p><p>The password for the email address: {1} has been reset.</p><p>Please go to the eStar site and register a new password. </p><p> Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li><li>Year Group: {5}</li><li>Tutor Group: {6}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                }
                else
                {
                    body = "<p>Hi {0}, </p><p>The following changes have been made to the account: {1} with eStar.</p><p>Account details:<ul><li>Name: {2} {0} {3}</li><li>User type: {4}</li><li>Year Group: {5}</li><li>Tutor Group: {6}</li><li>Total Points: {7}</li><li>Current Balance: {8}</li></ul></p><p>Thank you.</p><p>eStar</p>";
                }

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                //send email
                string messageBody = string.Format(body, student.First_Name, student.Email, student.Prefix, student.Surname, student.User_Type, student.Year_Group, student.Tutor_Group, student.Total_Points, student.Balance);
                string to = "bethany.fowler14@gmail.com"; //Change to student.Email when finished testing
                string from = "estar.smtp.fowler@gmail.com";
                string subject = "eStar Account Changes";

                await SendMessage(to, from, messageBody, subject);

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
