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
        Task<string> AddCategoryAsync(CategoryEntity entity);
        Task<Guid> UpdateCategoryAsync(CategoryEntity request, Guid id);
        Task<bool> DeleteCategoryAsync(Guid id);
        Task<CategoryEntity> GetCategoryAsync(Expression<Func<CategoryEntity, bool>> expression);

    }
}
