using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketResell_Service.Utilities
{
    public class EmailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> _logger)
        {
            _emailSettings = emailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendMailService");
        }

        public async Task<bool> SendVerificationEmail(string toEmail, string token, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail);
            message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlBody;
            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailSettings.Mail, _emailSettings.Password);
                await smtp.SendAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(emailsavefile);

                logger.LogInformation("Failed to send email, save at - " + emailsavefile);
                logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            logger.LogInformation("send mail to " + toEmail);
            return false;
        }
    }
}
