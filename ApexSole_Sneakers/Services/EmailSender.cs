using System.Net;
using System.Net.Mail;
using ApexSole_Sneakers.Interfaces;

namespace ApexSole_Sneakers.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromEmail = "menhuk3@gmail.com";
            var fromPassword = @"qbiz lmab gypl excx";

            MailMessage message = new();
            message.From = new MailAddress(fromEmail);
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
