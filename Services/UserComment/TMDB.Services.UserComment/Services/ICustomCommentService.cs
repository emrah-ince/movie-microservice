using System.Threading.Tasks;
using TMDB.Services.UserComment.Dtos;
using TMDB.Shared.Dtos;

namespace TMDB.Services.UserComment.Services
{
    public interface ICustomCommentService
    {
        Task<Response<CustonCommentDto>> Get(string movieId);

        Task<Response<bool>> Save(CustonCommentDto custonCommentDto);
    }
}