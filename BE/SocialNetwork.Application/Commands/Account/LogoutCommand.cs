using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.Account
{
    public class LogoutCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; }
    }

    public class LogoutnCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IRefreshTokenRepository repo;
        private readonly IMapper _mapper;

        public LogoutnCommandHandler(IRefreshTokenRepository repo, IMapper mapper)
        {
            this.repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var res = await repo.DeleteAsync(tk => tk.Token == request.RefreshToken);
            return res;
        }
    }
}
