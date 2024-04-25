using MediatR;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Create
{

    public class AddFollowUserCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string FollowerId { get; set; }
    }

    public class AddFollowUserCommandHandler : IRequestHandler<AddFollowUserCommand, int>
    {
        private readonly IUserRepository repo;

        public AddFollowUserCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(AddFollowUserCommand request, CancellationToken cancellationToken)
        {
            return await repo.AddFollowUserAsync(Guid.Parse(request.UserId), Guid.Parse(request.FollowerId));
        }
    }
}
