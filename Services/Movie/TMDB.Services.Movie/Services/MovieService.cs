using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Services.Movie.Settings;
using TMDB.Shared.Dtos;

namespace TMDB.Services.Movie.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMongoCollection<Model.Movie> _movieCollection;
 
        private readonly IMapper _mapper;

        public MovieService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
 
            _movieCollection = database.GetCollection<Model.Movie>(databaseSettings.MovieCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<MovieDto>>> GetAllAsync()
        {
            var movies = await _movieCollection.Find(movie => true).ToListAsync();
            return Response<List<MovieDto>>.Success(_mapper.Map<List<MovieDto>>(movies), 200);
        }

        private const int perPage = 10;

        public async Task<Response<List<MovieDto>>> GetWithPageAsync(int page)
        {
            var movies = await _movieCollection.Find(movie => true).Skip(perPage * (page-1)).Limit(perPage).ToListAsync();

            return Response<List<MovieDto>>.Success(_mapper.Map<List<MovieDto>>(movies), 200);
        }

        public async Task<Response<MovieDto>> CreateAsync(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Model.Movie>(movieCreateDto);
            await _movieCollection.InsertOneAsync(movie);

            return Response<MovieDto>.Success(_mapper.Map<MovieDto>(movie), 200);
        }

        public async Task<Response<MovieDto>> GetByIdAsync(string id)
        {
            var movie = await _movieCollection.Find<Model.Movie>(x => x.Id == id).FirstOrDefaultAsync();

            if (movie == null)
            {
                return Response<MovieDto>.Fail("Movie not found", 404);
            }

            return Response<MovieDto>.Success(_mapper.Map<MovieDto>(movie), 200);
        }

  
    }
}