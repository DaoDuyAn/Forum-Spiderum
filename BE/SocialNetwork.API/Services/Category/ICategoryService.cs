using SocialNetwork.API.DTOs;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.API.Services.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryEntity>> ListCategoriesAsync();
        Task<CategoryEntity> GetCategoryBySlugAsync(string slug);
        Task<CategoryEntity> GetCategoryByIdAsync(Guid Id);
        Task<CategoryEntity> AddCategoryAsync(AddCategoryRequest model);
        Task<CategoryEntity> UpdateCategoryAsync(UpdateCategoryRequest model);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
