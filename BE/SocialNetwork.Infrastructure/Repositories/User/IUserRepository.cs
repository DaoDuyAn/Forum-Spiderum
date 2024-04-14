using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.User
{
    public interface IUserRepository : IAsyncRepository<UserEntity>
    {
    }
}
