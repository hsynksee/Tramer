namespace SharedKernel.Models
{
    public class MailMessageInfo
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public List<string> To { get; set; }
        public List<string> Cc { get; set; }
        public List<string> Bcc { get; set; }
        public List<string> Attachments { get; set; }
        public MailSenderInfo MailSender { get; set; }
    }
}
