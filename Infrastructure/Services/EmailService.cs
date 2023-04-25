using Application.Common.Interfaces;
using Application.Helpers;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<AppSettings> appSettings;

        public EmailService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
        }

        public void Send(string to, string subject, string html, string? from = null)
        {
            // create message
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(from ?? appSettings.Value.EmailFrom));
            
            email.To.Add(MailboxAddress.Parse(to));
            
            email.Subject = subject;
            
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            smtp.Connect(appSettings.Value.SmtpHost, appSettings.Value.SmtpPort, SecureSocketOptions.StartTls);
            
            smtp.Authenticate(appSettings.Value.SmtpUser, appSettings.Value.SmtpPass);
            
            smtp.Send(email);

            smtp.Disconnect(true);
        }
    }
}
