using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IUserRepository : IAsyncRepository<UserEntity>
    {
        Task<UserEntity> GetUserAsync(Expression<Func<UserEntity, bool>> expression);
        Task<(List<UserEntity>, int, int)> SearchUserByValueAsync(string searchValue, int page);
        Task<int> AddLikePostAsync(Guid UserId, Guid PostId);
        Task<int> UnlikePostAsync(Guid UserId, Guid PostId);

        Task<int> AddSavedPostAsync(Guid UserId, Guid PostId);
        Task<int> UnsavedPostAsync(Guid UserId, Guid PostId);

        Task<int> AddFollowCategoryAsync(Guid UserId, Guid CategoryId);
        Task<int> UnfollowCategoryAsync(Guid UserId, Guid CategoryId);

        Task<int> AddFollowUserAsync(Guid UserId, Guid FollowerId);
        Task<int> UnfollowUserAsync(Guid UserId, Guid FollowerId);
    }
}
