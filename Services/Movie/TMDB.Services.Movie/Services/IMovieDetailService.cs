using System.Collections.Generic;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Shared.Dtos;

namespace TMDB.Services.Movie.Services
{
    public interface IMovieDetailService
    {
        Task<Response<List<MovieDetailDto>>> GetAllAsync();

        Task<Response<MovieDetailDto>> GetByIdAsync(string id);

        Task<Response<MovieDetailDto>> CreateAsync(MovieDetailCreateDto movieDetailCreateDto);
    }
}