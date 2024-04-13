using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.DTOs;
using SocialNetwork.API.Services.Post;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger
            , IPostService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] AddPostRequest request)
        {
            try
            {
                var post = await _service.AddPostAsync(request);
                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the post.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            try
            {
                var post = await _service.GetPostBySlugAsync(slug);

                if (post == null)
                {
                    return NotFound();
                }

                return Ok(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the post by slug.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
