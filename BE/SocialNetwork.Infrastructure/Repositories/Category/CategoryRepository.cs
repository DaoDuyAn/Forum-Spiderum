using Microsoft.AspNetCore.Hosting;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using SocialNetwork.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.Category
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        SocialNetworkDbContext _dbContext;
        private static IWebHostEnvironment? _hostEnvironment;

        public CategoryRepository(SocialNetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryEntity> GetCategoryAsync(Expression<Func<CategoryEntity, bool>> expression)
        {
            var cate = await GetAsync(expression);
            if (cate.CoverImagePath != "")
            {
                cate.CoverImagePath = HandleImage.ImageToBase64(cate.CoverImagePath);
            }

            return cate;
        }
    }
}
