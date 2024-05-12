using MediatR;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Application.Commands.Category.Update
{
    public class UpdateCategoryCommand : IRequest<Guid>
    {
        public string Id { get; set; }
        public string CategoryName { get; set; } = "";
        public string ContentAllowed { get; set; } = "";
        public string Base64Image { get; set; } = "";
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Guid>
    {

        private readonly ICategoryRepository repo;

        public UpdateCategoryCommandHandler(ICategoryRepository repo)
        {
            this.repo = repo;
        }
        public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new CategoryEntity
            {
                CategoryName = request.CategoryName,
                ContentAllowed = request.ContentAllowed,
                CoverImagePath = request.Base64Image
            };

            return await repo.UpdateCategoryAsync(newCategory, Guid.Parse(request.Id));

        }
    }
}
