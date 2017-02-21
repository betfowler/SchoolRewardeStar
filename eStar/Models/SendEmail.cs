using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace eStar.Models
{
    public class SendEmail
    {
        public static async System.Threading.Tasks.Task<string> Message(string to, string from, string body, string subject)
        {
            var email = new MailMessage();
            email.To.Add(new MailAddress(to));
            email.From = new MailAddress(from);
            email.Subject = subject;
            email.Body = body;

            email.IsBodyHtml = true;

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

                try
                {
                    await smtp.SendMailAsync(email);
                    return to;
                }
                catch
                {
                    return "Error";
                }
            }
        }
    }
}