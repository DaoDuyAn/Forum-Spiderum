using MediatR;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Delete
{

    public class UnsavedPostCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
    }

    public class UnsavedPostCommandHandler : IRequestHandler<UnsavedPostCommand, int>
    {
        private readonly IUserRepository repo;

        public UnsavedPostCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(UnsavedPostCommand request, CancellationToken cancellationToken)
        {
            return await repo.UnsavedPostAsync(Guid.Parse(request.UserId), Guid.Parse(request.PostId));
        }
    }
}
