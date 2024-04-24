using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Commands.User.Create;
using SocialNetwork.Application.Commands.User.Delete;
using SocialNetwork.Application.DTOs.User;
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
        [ProducesResponseType(typeof(GetUserByIdResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = Id });
            return Ok(user);
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
    }
}
