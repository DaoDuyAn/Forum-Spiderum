using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.RefreshToken
{
    public class RefreshTokenRepository : RepositoryBase<RefreshTokenEntity>, IRefreshTokenRepository
    {
        SocialNetworkDbContext _dbContext;
        public RefreshTokenRepository(SocialNetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
