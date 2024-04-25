using MediatR;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Commands.User.Delete
{

    public class UnfollowCategoryCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string CategoryId { get; set; }
    }

    public class UnfollowCategoryCommandHandler : IRequestHandler<UnfollowCategoryCommand, int>
    {
        private readonly IUserRepository repo;

        public UnfollowCategoryCommandHandler(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<int> Handle(UnfollowCategoryCommand request, CancellationToken cancellationToken)
        {
            return await repo.UnfollowCategoryAsync(Guid.Parse(request.UserId), Guid.Parse(request.CategoryId));
        }
    }
}
