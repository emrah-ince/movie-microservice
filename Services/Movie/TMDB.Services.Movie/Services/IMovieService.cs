using System.Collections.Generic;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Shared.Dtos;

namespace TMDB.Services.Movie.Services
{
    public interface IMovieService
    {
        Task<Response<List<MovieDto>>> GetAllAsync();

        Task<Response<MovieDto>> CreateAsync(MovieCreateDto movieCreateDto);

        Task<Response<MovieDto>> GetByIdAsync(string id);
        Task<Response<List<MovieDto>>> GetWithPageAsync(int page);
    }
}