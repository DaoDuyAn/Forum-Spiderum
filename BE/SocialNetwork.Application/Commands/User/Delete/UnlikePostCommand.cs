using MediatR;
using SocialNetwork.Application.Commands.User.Create;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Delete
{
    public class UnlikePostCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
    }

    public class UnlikePostCommandHandler : IRequestHandler<UnlikePostCommand, int>
    {
        private readonly IUserRepository repo;

        public UnlikePostCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(UnlikePostCommand request, CancellationToken cancellationToken)
        {
            return await repo.UnlikePostAsync(Guid.Parse(request.UserId), Guid.Parse(request.PostId));
        }
    }
}
