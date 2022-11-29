using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Services.Mail.Dtos;
using TMDB.Shared.Dtos;

namespace TMDB.Services.Mail.Services
{
    public interface IMailService
    {
        Task<Response<string>> SendAsync(MailDto dto);
    }
}
