using MediatR;
using SocialNetwork.Application.Commands.Post.Delete;
using SocialNetwork.Application.DTOs.Post;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Queries.Post
{
    public class GetPostBySlugQuery : IRequest<GetPostBySlugResponseDTO>
    {
        public string Slug { get; set; }
    }

    public class GetPostBySlugQueryHandler : IRequestHandler<GetPostBySlugQuery, GetPostBySlugResponseDTO>
    {
        private readonly IPostRepository postRepo;
        private readonly IDataContext dataContext;

        public GetPostBySlugQueryHandler(IPostRepository postRepo, IDataContext dataContext)
        {
            this.postRepo = postRepo;
            this.dataContext = dataContext;
        }

        public async Task<GetPostBySlugResponseDTO> Handle(GetPostBySlugQuery request, CancellationToken cancellationToken)
        {
            var post = await postRepo.GetAsync(p => p.Slug == request.Slug);
            var cate = await dataContext.CategoryRepo.GetAsync(c => c.Id == post.CategoryId);
            var user = await dataContext.UserRepo.GetAsync(u => u.Id == post.UserId);

            var response = new GetPostBySlugResponseDTO
            {
                PostInfo = new PostInfo
                {
                    Id = post.Id,
                    Title = post.Title,
                    Description = post.Description,
                    Content = post.Content,
                    CreationDate = post.CreationDate.ToString("dd/MM/yyyy"),
                    ThumbnailImagePath = post.ThumbnailImagePath,
                    Slug = post.Slug,
                    LikesCount = post.LikesCount,
                    CommentsCount = post.CommentsCount,
                    SavedCount = post.SavedCount
                },
                UserInfo = new UserInfo
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Description = user.Description,
                    AvatarImagePath = user.AvatarImagePath
                },
                PostCategoryInfo = new PostCategoryInfo
                {
                    Id = cate.Id,
                    CategoryName = cate.CategoryName,
                    Slug = cate.Slug,
                }
            };

            return response;
        }
    }

  


}
