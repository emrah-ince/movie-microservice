using AutoMapper;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Services.Movie.Model;
using TMDB.Services.Movie.Settings;
using TMDB.Shared.Dtos;

namespace TMDB.Services.Movie.Services
{
    public class MovieDetailService : IMovieDetailService
    {
        private readonly IMongoCollection<Model.Movie> _movieCollection;
        private readonly IMongoCollection<MovieDetail> _movieDetailCollection;
        private readonly IMapper _mapper;

        public MovieDetailService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _movieCollection = database.GetCollection<Model.Movie>(databaseSettings.MovieCollectionName);

            _movieDetailCollection = database.GetCollection<MovieDetail>(databaseSettings.MovieDetailCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<MovieDetailDto>>> GetAllAsync()
        {
            var movieDetails = await _movieDetailCollection.Find(movieDetail => true).ToListAsync();

            if (movieDetails.Any())
            {
                foreach (var movieDetail in movieDetails)
                {
                    movieDetail.Movie = await _movieCollection.Find<Model.Movie>(x => x.Id == movieDetail.MovieId).FirstAsync();
                }
            }
            else
            {
                movieDetails = new List<MovieDetail>();
            }

            return Response<List<MovieDetailDto>>.Success(_mapper.Map<List<MovieDetailDto>>(movieDetails), 200);
        }

        public async Task<Response<MovieDetailDto>> GetByIdAsync(string id)
        {
            var movieDetail = await _movieDetailCollection.Find<MovieDetail>(x => x.Id == id).FirstOrDefaultAsync();

            if (movieDetail == null)
            {
                return Response<MovieDetailDto>.Fail("Movie Detail not found", 404);
            }
            movieDetail.Movie = await _movieCollection.Find<Model.Movie>(x => x.Id == movieDetail.MovieId).FirstAsync();

            return Response<MovieDetailDto>.Success(_mapper.Map<MovieDetailDto>(movieDetail), 200);
        }

        public async Task<Response<MovieDetailDto>> CreateAsync(MovieDetailCreateDto movieDetailCreateDto)
        {
            var newMovieDetail = _mapper.Map<MovieDetail>(movieDetailCreateDto);

            await _movieDetailCollection.InsertOneAsync(newMovieDetail);

            return Response<MovieDetailDto>.Success(_mapper.Map<MovieDetailDto>(newMovieDetail), 200);
        }
    }
}