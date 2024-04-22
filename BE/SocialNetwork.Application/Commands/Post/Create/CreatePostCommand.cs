using MediatR;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Application.Commands.Post.Create
{
    public class CreatePostCommand : IRequest<string>
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";

        public string UserId { get; set; } = "";
        public string CategoryId { get; set; } = "";
    }

    public class CreatePostCommandCommandHandler : IRequestHandler<CreatePostCommand, string>
    {
        private readonly IPostRepository repo;

        public CreatePostCommandCommandHandler(IPostRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            // Tạo entity mới
            var newPost = new PostEntity
            {
                Title = request.Title,
                Content = request.Content,
                Slug = "",
                ThumbnailImagePath = "",
                Description = request.Description,
                UserId = Guid.Parse(request.UserId),
                CategoryId = Guid.Parse(request.CategoryId),
                CreationDate = DateTime.Now,
            };

            var post = await repo.AddPostAsync(newPost);
            return post.Slug;
        }
    }
}
