using EMS.API.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EMS.API.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(
    string toEmail,
    string subject,
    string body)
        {
            Console.WriteLine($"Sending email to: {toEmail}");
            Console.WriteLine($"Subject: {subject}");

            using var client = new SmtpClient(
                _emailSettings.SmtpServer,
                _emailSettings.Port);

            client.Credentials = new NetworkCredential(
                _emailSettings.SenderEmail,
                _emailSettings.SenderPassword);

            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail), 
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);

            Console.WriteLine("Email sent successfully.");
        }
    }
}