using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Commands.User.Create;
using SocialNetwork.Application.Commands.User.Delete;
using SocialNetwork.Application.DTOs.User;
using SocialNetwork.Application.Queries.Post;
using SocialNetwork.Application.Queries.User;
using System.Net;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetUserById/id/{Id}")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = Id });
            return Ok(user);
        }


        [HttpGet("GetUserByUserName/username/{UserName}")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserByUserName(string UserName)
        {
            var user = await _mediator.Send(new GetUserByUserNameQuery { UserName = UserName });
            return Ok(user);
        }

        [HttpGet("SearchUserByValue")]
        public async Task<IActionResult> SearchUserByValue([FromQuery(Name = "q")] string q, [FromQuery(Name = "page")] int page)
        {
            var users = await _mediator.Send(new SearchUserByValueQuery { SearchValue = q, Page = page });
            return Ok(users);
        }

        [HttpPost("AddLikePost")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> AddLikePost([FromBody] AddLikePostCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("UnlikePost")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> UnlikePost([FromBody] UnlikePostCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("AddSavedPost")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> AddSavedPost([FromBody] AddSavedPostCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("UnsavedPost")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> UnsavedPost([FromBody] UnsavedPostCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("AddFollowCategory")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> AddFollowCategory([FromBody] AddFollowCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("UnfollowCategory")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> UnfollowCategory([FromBody] UnfollowCategoryCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("AddFollowUser")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> AddFollowUser([FromBody] AddFollowUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("UnfollowUser")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<int> UnfollowUser([FromBody] UnfollowUserCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
