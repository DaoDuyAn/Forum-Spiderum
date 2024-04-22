using MediatR;
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
            var cate = await repo.GetAsync(c => c.Id == Guid.Parse(request.Id));

            cate.CategoryName = request.CategoryName;
            cate.CoverImagePath = request.Base64Image;
            cate.ContentAllowed = request.ContentAllowed;

            return await repo.UpdateCategoryAsync(cate);

        }
    }
}
