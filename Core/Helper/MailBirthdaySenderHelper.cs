using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class MailBirthdaySenderHelper
    {
        public static void BirthdaySendEmail(string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("mail.photographybella.com");
            mail.From = new MailAddress("boost@photographybella.com");
            mail.To.Add(email);
            mail.Subject = $"www.ık.com::Dogum Gunun sa";
            mail.Body += $"Dogum günün kutlu olsun kral";//template eklenecek !!
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("boost@photographybella.com", "45526201Co");
            smtpClient.Send(mail);
        }
    }
}
