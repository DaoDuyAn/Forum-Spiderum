using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.User
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        SocialNetworkDbContext _dbContext;

        public UserRepository(SocialNetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
