using AutoMapper;
using MediatR;
using SocialNetwork.Application.DTOs.Account;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.Account.Create
{
    public class CreatAccountCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string FullName { get; set; }    
        public string Password { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
    }

    public class CreatAccountCommandHandler : IRequestHandler<CreatAccountCommand, int>
    {
        private readonly IAccountRepository repo;

        public CreatAccountCommandHandler(IAccountRepository repo, IMapper mapper, IDataContext context)
        {
            this.repo = repo;
        }

        public async Task<int> Handle(CreatAccountCommand request, CancellationToken cancellationToken)
        {
            return await repo.AddAccountAsync(request.UserName, request.Password, request.FullName, request.Phone, Guid.Parse(request.RoleId));
        }
    }
}
