using SocialNetwork.Domain.Entities;

namespace SocialNetwork.API.Services.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryEntity>> ListCategoriesAsync();
        Task<CategoryEntity> GetCategoryBySlugAsync(string slug);
    }
}
