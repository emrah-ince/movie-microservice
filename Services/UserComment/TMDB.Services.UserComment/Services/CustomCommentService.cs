using System;
using System.Text.Json;
using System.Threading.Tasks;
using TMDB.Services.UserComment.Dtos;
using TMDB.Shared.Dtos;

namespace TMDB.Services.UserComment.Services
{
    public class CustomCommentService : ICustomCommentService
    {
        private readonly RedisService _redisService;

        public CustomCommentService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<CustonCommentDto>> Get(string movieId)
        {

            var existComment = await _redisService.GetDb().StringGetAsync(movieId);

            if (String.IsNullOrEmpty(existComment))
            {
                return Response<CustonCommentDto>.Fail("Custom comment not found", 404);
            }

            return Response<CustonCommentDto>.Success(JsonSerializer.Deserialize<CustonCommentDto>(existComment), 200);
        }

        public async Task<Response<bool>> Save(CustonCommentDto custonCommentDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(custonCommentDto.MovieId, JsonSerializer.Serialize(custonCommentDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Custom comment could not  save", 500);
        }
    }
}