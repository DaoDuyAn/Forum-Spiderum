using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Interfaces
{
    public interface ICategoryRepository : IAsyncRepository<CategoryEntity>
    {
        Task<CategoryEntity> GetCategoryAsync(Expression<Func<CategoryEntity, bool>> expression);
    }
}
