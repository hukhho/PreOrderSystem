using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PreorderPlatform.Service.Services.SendMailServices
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // The email template, stored as a string
            //string htmlTemplate = @"
            //<!DOCTYPE html>
            //<html>
            //<head>
            //    <title>Welcome to Pre Order Platform</title>
            //</head>
            //<body style=""font-family: Arial, sans-serif;"">
            //    <div style=""max-width: 600px; margin: auto;"">
            //        <h2 style=""color: #444444;"">Welcome to Pre Order !</h2>
            //        <p>Hello {username},</p>
            //        <p>Thank you for registering on our website. Please click the link below to activate your account:</p>
            //        <p style=""text-align: center;"">
            //            <a href=""{activationLink}"" 
            //            style=""background-color: #008CBA;
            //                    color: white;
            //                    padding: 14px 20px;
            //                    text-align: center;
            //                    text-decoration: none;
            //                    display: inline-block;"">Activate Account</a>
            //        </p>
            //        <p>If you didn't request this, you can safely ignore this email.</p>
            //        <p>Best regards,</p>
            //        <p>Pre Order Platform</p>
            //    </div>
            //</body>
            //</html>";

            //// Replace placeholders with actual values
            //string emailBody = htmlTemplate
            //    .Replace("{username}", "hungllt001")
            //    .Replace("{activationLink}", "teLink");


            var smtpClient = new SmtpClient("mail.privateemail.com")
            {
                Port = 587,  // You can also try with 465 if 587 doesn't work
                Credentials = new NetworkCredential("hungllt@preorderplatform.store", "Hung@160420"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("hungllt@preorderplatform.store"),
                To = { new MailAddress(email) },
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            //mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
