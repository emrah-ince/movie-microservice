using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMDB.Services.UserComment.Dtos;
using TMDB.Services.UserComment.Services;
using TMDB.Shared.ControllerBases;
using TMDB.Shared.Services;

namespace TMDB.Services.UserComment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : CustomBaseController
    {
        private readonly ICustomCommentService _customCommentService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CommentsController(ISharedIdentityService sharedIdentityService, ICustomCommentService customCommentService)
        {
            _sharedIdentityService = sharedIdentityService;
            _customCommentService = customCommentService;
        }

        [HttpGet("{movieId}")]
        public async Task<IActionResult> GetComment(string movieId)
        {
            return CreateActionResultInstance(await _customCommentService.Get(movieId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveComment(CustonCommentDto custonCommentDto)
        {
            custonCommentDto.UserId = _sharedIdentityService.GetUserId;
            var response = await _customCommentService.Save(custonCommentDto);
            return CreateActionResultInstance(response);
        }
    }
}