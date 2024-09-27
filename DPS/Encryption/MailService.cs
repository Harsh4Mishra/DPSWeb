using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Configuration;

namespace DPS.Encryption
{
    public class MailService
    {
        public void SendMail(string subject, string body, string to, string cc = null, string bcc = null, Dictionary<string, byte[]> attachments = null)
        {
            string _smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            int _smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            string _smtpUser = ConfigurationManager.AppSettings["SMTPUser"].ToString();
            string _smtpPass = ConfigurationManager.AppSettings["SMTPPass"].ToString();

            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_smtpUser);
                message.To.Add(new MailAddress(to));

                if (!string.IsNullOrEmpty(cc))
                {
                    message.CC.Add(new MailAddress(cc));
                }

                if (!string.IsNullOrEmpty(bcc))
                {
                    message.Bcc.Add(new MailAddress(bcc));
                }

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true; // or false if you are sending plain text

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        message.Attachments.Add(new Attachment(new System.IO.MemoryStream(attachment.Value), attachment.Key));
                    }
                }

                using (var client = new SmtpClient(_smtpServer, (int)_smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    client.EnableSsl = true;
                    client.Send(message);
                }
            }
        }
    }
}