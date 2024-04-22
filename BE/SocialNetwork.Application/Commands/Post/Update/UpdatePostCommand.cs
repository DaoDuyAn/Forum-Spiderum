using MediatR;
using SocialNetwork.Application.Utilities;
using SocialNetwork.Domain.Interfaces;

namespace SocialNetwork.Application.Commands.Post.Update
{
    public class UpdatePostCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";

        public string UserId { get; set; } = "";
        public string CategoryId { get; set; } = "";
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, string>
    {

        private readonly IPostRepository repo;

        public UpdatePostCommandHandler(IPostRepository repo)
        {
            this.repo = repo;
        }
        public async Task<string> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await repo.GetAsync(c => c.Id == Guid.Parse(request.Id));

            string postSlug = post.Slug;

            int lastIndex = post.Slug.LastIndexOf('-');
            string oldPostSlug = lastIndex != -1 ? post.Slug.Substring(0, lastIndex) : "";

            string newPostSlug = AppUtilities.GenerateSlug(request.Title);

            if (oldPostSlug != newPostSlug)
            {
                Guid guid = Guid.NewGuid();
                string[] guidParts = guid.ToString().Split('-');

                postSlug = AppUtilities.GenerateSlug(request.Title) + "-" + guidParts[0];
            }

            post.Content = request.Content;
            post.Title = request.Title;
            post.Description = request.Description;
            post.CategoryId = Guid.Parse(request.CategoryId);
            post.Slug = postSlug;

            return await repo.UpdatePostAsync(post);


        }
    }
}
