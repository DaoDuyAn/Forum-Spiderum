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
    }
}
