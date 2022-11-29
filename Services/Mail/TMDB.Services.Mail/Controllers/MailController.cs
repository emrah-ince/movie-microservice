using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMDB.Services.Mail.Dtos;
using TMDB.Services.Mail.Services;
using TMDB.Shared.ControllerBases;

namespace TMDB.Services.Mail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : CustomBaseController
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send(MailDto mailDto)
        {
            var response = await _mailService.SendAsync(mailDto);

            return CreateActionResultInstance(response);
        }
    }
}