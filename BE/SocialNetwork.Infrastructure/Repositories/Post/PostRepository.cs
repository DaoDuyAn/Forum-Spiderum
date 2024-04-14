using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.EF;
using SocialNetwork.Infrastructure.Repositories.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.Post
{
    public class PostRepository : RepositoryBase<PostEntity>, IPostRepository
    {
        SocialNetworkDbContext _dbContext;

        public PostRepository(SocialNetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostEntity> GetPostBySlugAsync(string slug)
        {
            return await _dbContext.Posts
             .Where(p => p.Slug == slug)
             .Select(p => new PostEntity
             {
                 Id = p.Id,
                 Title = p.Title,
                 Description = p.Description,
                 Content = p.Content,
                 CreationDate = p.CreationDate,
                 ThumbnailImagePath = p.ThumbnailImagePath,
                 Slug = p.Slug,
                 UserId = p.UserId,
                 CategoryId = p.CategoryId,
                 User = new UserEntity
                 {
                     Id = p.UserId,
                     UserName = p.User.UserName,
                     FullName = p.User.FullName,
                     Description = p.User.Description,
                     AvatarImagePath = p.User.AvatarImagePath
                 },
                 Category = new CategoryEntity
                 {
                     Id = p.CategoryId,
                     CategoryName = p.Category.CategoryName,
                     Slug = p.Category.Slug
                 },
                 LikesCount = p.LikesCount,
                 CommentsCount = p.CommentsCount,
                 SavedCount = p.SavedCount
             })
             .FirstOrDefaultAsync();
        }
    }
}
