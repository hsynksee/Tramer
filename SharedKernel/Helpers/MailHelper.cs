using SharedKernel.Models;
using System.Net;
using System.Net.Mail;

namespace SharedKernel.Helpers
{
    public  static class MailHelper
    {
        public static void SendEmail(MailMessageInfo sentMail)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (MailMessage mail = new MailMessage())
            {

                mail.Body = sentMail.Body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;


                mail.From = new MailAddress(sentMail.MailSender.Email);
                mail.Sender = new MailAddress(sentMail.MailSender.Email);

                mail.Subject = sentMail.Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                AddRecipients(mail.To, sentMail.To);
                if (sentMail.Cc != null)
                    AddRecipients(mail.CC, sentMail.Cc);
                if (sentMail.Bcc != null)
                    AddRecipients(mail.Bcc, sentMail.Bcc);

                if (sentMail.Attachments != null)
                {
                    foreach (var attachment in sentMail.Attachments)
                        mail.Attachments.Add(new Attachment(attachment));
                }


                using (SmtpClient smtpClient = new SmtpClient(sentMail.MailSender.Host, sentMail.MailSender.Port))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential(sentMail.MailSender.Email, sentMail.MailSender.Password);
                    smtpClient.Send(mail);
                }
            }
        }

        private static void AddRecipients(MailAddressCollection collection, List<string> addresses)
        {
            foreach (var address in addresses)
            {
                collection.Add(address);
            }
        }
    }
}
