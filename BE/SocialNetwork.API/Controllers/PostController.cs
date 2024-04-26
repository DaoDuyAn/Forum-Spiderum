using Microsoft.AspNetCore.Mvc;
using MediatR;
using SocialNetwork.Application.Commands.Post.Delete;
using System.Net;
using SocialNetwork.Application.Queries.Post;
using SocialNetwork.Application.Commands.Post.Update;
using SocialNetwork.Application.Commands.Post.Create;
using SocialNetwork.Application.DTOs.Post;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PostController> _logger;

        public PostController(ILogger<PostController> logger
            , IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("CreatePost")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("UpdatePost")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetPostBySlug/slug/{slug}")]
        [ProducesDefaultResponseType(typeof(GetPostBySlugResponseDTO))]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            var post = await _mediator.Send(new GetPostBySlugQuery { Slug = slug });
            return Ok(post);
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var post = await _mediator.Send(new GetAllPostsQuery());
            return Ok(post);
        }

        [HttpGet("GetPostsByUserId/userId/{UserId}")]
        public async Task<IActionResult> GetPostsByUserId(string UserId)
        {
            var post = await _mediator.Send(new GetPostsByUserIdQuery { UserId = UserId });
            return Ok(post);
        }

        [HttpDelete("DeletePostById/id/{Id}")]
        public async Task<bool> DeletePostById(string Id)
        {
            return await _mediator.Send(new DeletePostByIdCommand { Id = Id });
        }
    }
}
