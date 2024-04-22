using SocialNetwork.API.DTOs;
using SocialNetwork.API.DTOs.Category;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.API.Services.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryEntity>> ListCategoriesAsync();
        Task<CategoryEntity> GetCategoryBySlugAsync(GetCategoryBySlugRequest request);
        Task<CategoryEntity> GetCategoryByIdAsync(GetCategoryByIdRequest request);
        Task<CategoryEntity> AddCategoryAsync(AddCategoryRequest request);
        Task<int> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
