using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class UserEmailConfirmationHelper
    {
        public static void EmailConfirmationSend(string link, string email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("mail.photographybella.com");
            mail.From = new MailAddress("boost@photographybella.com");
            mail.To.Add(email);
            mail.Subject = $"www.ık.com::Email Doğrulama";
            mail.Body = "<h2>Email adresinizi doğrulamak için aşağıdaki linke tıklayınız.</h2><hr/>";
            mail.Body += $"<a href='{link}'>Email doğrulama linki </a>";
            mail.IsBodyHtml = true;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential("boost@photographybella.com", "45526201Co");
            smtpClient.Send(mail);
        }
    }
}
