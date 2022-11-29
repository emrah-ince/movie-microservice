using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Services.Movie.Services;
using TMDB.Shared.ControllerBases;

namespace TMDB.Services.Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : CustomBaseController
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _movieService.GetAllAsync();

            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/GetWithPage/{page}")]
        public async Task<IActionResult> GetWithPage(int page)
        {
            var response = await _movieService.GetWithPageAsync(page);

            return CreateActionResultInstance(response);
        }

        //movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _movieService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreateDto courseCreateDto)
        {
            var response = await _movieService.CreateAsync(courseCreateDto);

            return CreateActionResultInstance(response);
        }
    }
}