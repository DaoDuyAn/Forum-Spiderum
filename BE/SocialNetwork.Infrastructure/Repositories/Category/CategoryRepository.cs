using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.Category
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        SocialNetworkDbContext _dbContext;

        public CategoryRepository(SocialNetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
