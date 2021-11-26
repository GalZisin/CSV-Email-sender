using CSVEmailSender.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailSender()
        {
            _emailConfig = MainAppPath.emailConfiguration;
        }
     
        public async Task SendEmailAsync(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments, string path)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(To);
            emailMessage.Subject = subject;
      
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", content) };


            var bodyBuilder = new BodyBuilder();
            byte[] fileBytes = File.ReadAllBytes(path);

            bodyBuilder.Attachments.Add(path, fileBytes, ContentType.Parse("application/zip"));

            emailMessage.Body = bodyBuilder.ToMessageBody();

            await SendAsync(emailMessage);
        }

     


        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (SmtpClient client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
