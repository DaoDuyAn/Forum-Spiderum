using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.Account
{
    public class LoginCommand : IRequest<AuthResponseDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
    {
        private readonly IAccountRepository repo;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IAccountRepository repo, IMapper mapper)
        {
            this.repo = repo;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var apiResponse = await repo.LoginAsync(request.UserName, request.Password);
            var authDTO = _mapper.Map<AuthResponseDTO>(apiResponse);
            return authDTO;
        }
    }
}
