using MediatR;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Create
{
    public class AddLikePostCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
    }

    public class AddLikePostCommandHandler : IRequestHandler<AddLikePostCommand, int>
    {
        private readonly IUserRepository repo;

        public AddLikePostCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(AddLikePostCommand request, CancellationToken cancellationToken)
        {
            return await repo.AddLikePostAsync(Guid.Parse(request.UserId), Guid.Parse(request.PostId));
        }
    }
}
