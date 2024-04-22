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
    }
}
