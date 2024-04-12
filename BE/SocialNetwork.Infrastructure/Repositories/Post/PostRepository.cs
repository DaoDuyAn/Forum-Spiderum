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
    }
}
