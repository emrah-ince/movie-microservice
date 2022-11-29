using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Services.Movie.Services;
using TMDB.Shared.ControllerBases;

namespace TMDB.Services.Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieDetailsController : CustomBaseController
    {
        private readonly IMovieDetailService _movieDetailService;

        public MovieDetailsController(IMovieDetailService movieDetailService)
        {
            _movieDetailService = movieDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _movieDetailService.GetAllAsync();

            return CreateActionResultInstance(response);
        }

        //moviedetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _movieDetailService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieDetailCreateDto movieDetailCreateDto)
        {
            var response = await _movieDetailService.CreateAsync(movieDetailCreateDto);

            return CreateActionResultInstance(response);
        }
    }
}