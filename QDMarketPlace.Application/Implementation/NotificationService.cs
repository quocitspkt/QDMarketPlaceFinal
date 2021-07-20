using QDMarketPlace.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace QDMarketPlace.Application.Implementation
{
    public class NotificationService: INotification
    {
        public NotificationService()
        {

        }
        public void SendNotification(string subject, string body, string to)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("quocitspkt@idrive.vn"));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("quocitspkt@idrive.vn", "Dauchodau1");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
