﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.Commands.Account;
using SocialNetwork.Application.Commands.Account.Create;
using SocialNetwork.Application.Commands.Account.Update;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Infrastructure.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync([FromBody] LogoutCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpPost("RenewToken")]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> RenewToken([FromBody] RenewTokenCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("SignUp")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> CreateAccount([FromBody] CreatAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    } 
}
