using MediatR;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Application.Commands.Category.Delete
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository repo;

        public DeleteCategoryCommandHandler(ICategoryRepository repo)
        {
            this.repo = repo;
        }


        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return await repo.DeleteCategoryAsync(Guid.Parse(request.Id));
        }
    }
}
