using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.DTOs;
using MediatR;
using SocialNetwork.Application.Commands.Post.Delete;
using System.Net;
using SocialNetwork.Application.Queries.Post;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("UpdatePost")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetPostBySlug/slug/{slug}")]
        [ProducesResponseType(typeof(GetPostBySlugResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostBySlug(string slug)
        {
            var post = await _mediator.Send(new GetPostBySlugQuery { Slug = slug });
            return Ok(post);
        }

        [HttpDelete("DeletePostById/id/{Id}")]
        public async Task<bool> DeletePostById(string Id)
        {
            return await _mediator.Send(new DeletePostByIdCommand { Id = Id });
        }
    }
}
