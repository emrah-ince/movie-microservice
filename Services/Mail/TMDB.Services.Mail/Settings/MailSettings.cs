namespace TMDB.Services.Mail.Settings
{
    public class MailSettings
    {
        public int Port { get; set; }
        public string SmtpHost { get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }
        public bool DefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Subject { get; set; }
    }
}