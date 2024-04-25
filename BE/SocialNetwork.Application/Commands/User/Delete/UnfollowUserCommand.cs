using MediatR;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Delete
{

    public class UnfollowUserCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string FollowerId { get; set; }
    }

    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, int>
    {
        private readonly IUserRepository repo;

        public UnfollowUserCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
        {
            return await repo.UnfollowUserAsync(Guid.Parse(request.UserId), Guid.Parse(request.FollowerId));
        }
    }
}
