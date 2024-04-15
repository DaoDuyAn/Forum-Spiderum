using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.DTOs;
using SocialNetwork.API.Services.Post;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
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

        [HttpPost("AddPost")]
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

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request)
        {
            try
            {
                var post = await _service.UpdatePostAsync(request);

                if (post == null)
                {
                    return NotFound(StatusCodes.Status404NotFound);
                }

                return Ok(post);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetPostBySlug/slug/{slug}")]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            try
            {
                var request = new GetPostBySlugRequest { Slug = slug };
                var post = await _service.GetPostBySlugAsync(request);

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

        [HttpDelete("DeletePostById/id/{Id}")]
        public async Task<bool> DeletePostById(string Id)
        {
            return await _service.DeletePostAsync(Guid.Parse(Id));
        }
    }
}
