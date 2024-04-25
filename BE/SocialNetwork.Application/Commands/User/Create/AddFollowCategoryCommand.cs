using MediatR;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Create
{
    public class AddFollowCategoryCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string CategoryId { get; set; }
    }

    public class AddFollowCategoryCommandHandler : IRequestHandler<AddFollowCategoryCommand, int>
    {
        private readonly IUserRepository repo;

        public AddFollowCategoryCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(AddFollowCategoryCommand request, CancellationToken cancellationToken)
        {
            return await repo.AddFollowCategoryAsync(Guid.Parse(request.UserId), Guid.Parse(request.CategoryId));
        }
    }
}
