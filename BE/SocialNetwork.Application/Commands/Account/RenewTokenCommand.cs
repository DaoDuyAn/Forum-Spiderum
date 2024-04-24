using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.Account
{
    public class RenewTokenCommand : IRequest<AuthResponseDTO>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }


    public class RenewTokenCommandHandler : IRequestHandler<RenewTokenCommand, AuthResponseDTO>
    {
        private readonly IAccountRepository repo;
        private readonly IMapper _mapper;

        public RenewTokenCommandHandler(IAccountRepository repo, IMapper mapper)
        {
            this.repo = repo;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> Handle(RenewTokenCommand request, CancellationToken cancellationToken)
        {
            var apiResponse = await repo.RenewTokenAsync(request.UserId, request.UserName, request.AccessToken, request.RefreshToken);
            var authDTO = _mapper.Map<AuthResponseDTO>(apiResponse);
            return authDTO;
        }
    }
}
