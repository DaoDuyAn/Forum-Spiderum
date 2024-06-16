using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IPostRepository : IAsyncRepository<PostEntity>
    {
        Task<PostEntity> AddPostAsync(PostEntity entity);
        Task<string> UpdatePostAsync(PostEntity request);
        Task<bool> DeletePostAsync(Guid id);
        Task<PostEntity> GetPostAsync(Expression<Func<PostEntity, bool>> expression);
        Task<List<PostEntity>> GetAllPostsAsync();
        Task<List<PostEntity>> GetPostsByUserIdAsync(Guid UserId);
        Task<(List<PostEntity>, int, int)> GetPostsByCategorySlugAsync(string sort, int pageIndex, string categorySlug);
        Task<(List<PostEntity>, int, int)> GetPostsAsync(string sort, int pageIndex, Guid userId);
        Task<(List<PostEntity>, int, int)> GetPostsSuggestionAsync(string postSlug);
        Task<(List<PostEntity>, int, int)> SearchPostByValueAsync(string searchValue, int page); 
        Task<(List<PostEntity>, int, int)> GetPostsByUserNameAsync(string tab, int page, string userName);
    }
}
