using MediatR;
using SocialNetwork.Application.Commands.User.Create;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Create
{
    public class AddSavedPostCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
    }

    public class AddSavedPostCommandHandler : IRequestHandler<AddSavedPostCommand, int>
    {
        private readonly IUserRepository repo;

        public AddSavedPostCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(AddSavedPostCommand request, CancellationToken cancellationToken)
        {
            return await repo.AddSavedPostAsync(Guid.Parse(request.UserId), Guid.Parse(request.PostId));
        }
    }

}
