using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.Account.Update
{
    public class ChangePasswordCommand : IRequest<int>
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }
        public string userId { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, int>
    {
        private readonly IAccountRepository repo;

        public ChangePasswordCommandHandler(IAccountRepository repo)
        {
            this.repo = repo;
        }

        public async Task<int> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await repo.ChangePasswordAsync(request.oldPassword, request.newPassword, request.confirmPassword, Guid.Parse(request.userId));  
            return res;
        }
    }
}
