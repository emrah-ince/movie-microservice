using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TMDB.Services.Mail.Dtos;
using TMDB.Services.Mail.Settings;
using TMDB.Shared.Dtos;

namespace TMDB.Services.Mail.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<Response<string>> SendAsync(MailDto dto)
        {
            try
            {
                SmtpClient client = new SmtpClient(_mailSettings.SmtpHost);

                client.Port = _mailSettings.Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.UseDefaultCredentials = _mailSettings.DefaultCredentials;
                NetworkCredential credential =
                    new NetworkCredential(_mailSettings.FromAddress, _mailSettings.Password);
                client.EnableSsl = _mailSettings.EnableSsl;
                client.Credentials = credential;

                MailMessage message = new MailMessage(_mailSettings.FromAddress, dto.SendMail.ToLower());
                message.Subject = _mailSettings.Subject;
                message.Body = $"Önerilen film : {dto.MovieTitle}";

                message.IsBodyHtml = _mailSettings.IsBodyHtml;
                client.Send(message);
                return Response<string>.Success("Başarılı", 200);
            }
            catch (System.Exception ex)
            {
                return Response<string>.Fail(ex.Message, 404);
            }
        }
    }
}