using Microsoft.AspNetCore.Mvc;
using MediatR;
using SocialNetwork.Application.Commands.Post.Delete;
using System.Net;
using SocialNetwork.Application.Queries.Post;
using SocialNetwork.Application.Commands.Post.Update;
using SocialNetwork.Application.Commands.Post.Create;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Application.Queries.User;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpPost("CreatePost")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
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

        [HttpGet("GetPosts")]
        public async Task<IActionResult> GetPosts([FromQuery(Name = "sort")] string sort, [FromQuery(Name = "page_idx")] int page_idx, [FromQuery(Name = "userId")] string userId)
        {
            var posts = await _mediator.Send(new GetPostsQuery { Sort = sort, PageIndex = page_idx, UserId = userId });
            return Ok(posts);
        }

        [HttpGet("GetPostsByCategory")]
        public async Task<IActionResult> GetPostsByCategory([FromQuery(Name = "sort")] string sort, [FromQuery(Name = "page_idx")] int page_idx, [FromQuery(Name = "slug")] string slug)
        {
            var posts = await _mediator.Send(new GetPostsByCategoryQuery { Sort = sort, PageIndex = page_idx, CategorySlug = slug });
            return Ok(posts);
        }

        [HttpGet("GetPostsByUserName")]
        public async Task<IActionResult> GetPostsByUserName([FromQuery(Name = "tab")] string tab, [FromQuery(Name = "page")] int page, [FromQuery(Name = "userName")] string userName)
        {
            var posts = await _mediator.Send(new GetPostsByUserNameQuery { Tab = tab, Page = page, UserName = userName });
            return Ok(posts);
        }

        [HttpGet("SearchPostByValue")]
        public async Task<IActionResult> SearchPostByValue([FromQuery(Name = "q")] string q, [FromQuery(Name = "page")] int page)
        {
            var posts = await _mediator.Send(new SearchPostByValueQuery { SearchValue = q, Page = page });
            return Ok(posts);
        }

        [Authorize]
        [HttpDelete("DeletePostById/id/{Id}")]
        public async Task<bool> DeletePostById(string Id)
        {
            return await _mediator.Send(new DeletePostByIdCommand { Id = Id });
        }
    }
}
