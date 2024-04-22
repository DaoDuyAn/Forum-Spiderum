using MediatR;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Application.Commands.Category.Create
{
    public class CreateCategoryCommand : IRequest<string>
    {
        public string CategoryName { get; set; }
        public string ContentAllowed { get; set; }
        public string Base64Image { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, string>
    {
        private readonly ICategoryRepository repo;

        public CreateCategoryCommandHandler(ICategoryRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // Tạo entity mới
            var newCategory = new CategoryEntity
            {
               CategoryName = request.CategoryName,
               ContentAllowed = request.ContentAllowed,
               CoverImagePath = request.Base64Image
            };

            return await repo.AddCategoryAsync(newCategory);
        }
    }
}
