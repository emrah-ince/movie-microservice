using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TMDB.Services.Movie.Dtos;
using TMDB.Services.Movie.Model;
using TMDB.Services.Movie.Settings;

namespace TMDB.Services.Movie.Services
{
    public class SaveMovieListBacgroundWorker : BackgroundService
    {
        private readonly ILogger<SaveMovieListBacgroundWorker> _logger;
        private readonly IMongoCollection<Model.Movie> _movieCollection;
        private readonly IMongoCollection<MovieDetail> _movieDetailCollection;
        private readonly IMapper _mapper;

        private const string URL = "https://api.themoviedb.org/3/movie/popular";
        private string urlParameters = "?api_key=5abd992f12f33b0d0efb1dd649141f3c&language=en-US&page=";
        private int page = 1;

        public SaveMovieListBacgroundWorker(ILogger<SaveMovieListBacgroundWorker> logger, IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _movieCollection = database.GetCollection<Model.Movie>(databaseSettings.MovieCollectionName);
            _movieDetailCollection = database.GetCollection<MovieDetail>(databaseSettings.MovieDetailCollectionName);

            _logger = logger;
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                HttpClient client = new HttpClient();

                while (!stoppingToken.IsCancellationRequested)
                {
                    for (int i = 1; i <= 500; i++)
                    {
                        HttpResponseMessage response = await client.GetAsync(URL + urlParameters + i);

                        if (!response.IsSuccessStatusCode)
                            break;

                        response.EnsureSuccessStatusCode();

                        string responseJson = await response.Content.ReadAsStringAsync();

                        var moviResponseDto = JsonSerializer.Deserialize<BaseJsonMovieResponseDto<JsonMovieResponseDto>>(responseJson);

                        foreach (var item in moviResponseDto.results)
                        {
                            var getMovie = await _movieCollection.Find<Model.Movie>(x => x.OriginalTitle == item.original_title).FirstOrDefaultAsync();

                            if (getMovie != null)
                                continue;

                            var movie = _mapper.Map<Model.Movie>(item);
                            movie.Id = null;

                            await _movieCollection.InsertOneAsync(movie);

                            var movieDetail = _mapper.Map<Model.MovieDetail>(item);
                            movieDetail.Id = null;
                            var saveMovie = await _movieCollection.Find<Model.Movie>(x => x.OriginalTitle == movie.OriginalTitle).FirstOrDefaultAsync();
                            if (saveMovie == null)
                                continue;

                            movieDetail.MovieId = saveMovie.Id;
        
                            await _movieDetailCollection.InsertOneAsync(movieDetail);

                            _logger.LogInformation($"{movie.OriginalTitle} save : time : {DateTimeOffset.Now}");
                        }
                    }

                    _logger.LogInformation("Worker running at : {time}", DateTimeOffset.Now);
                    await Task.Delay(60000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Movie get list error : {ex.Message}");
            }
        }
    }
}