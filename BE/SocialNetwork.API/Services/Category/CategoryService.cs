using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Repositories.Category;

namespace SocialNetwork.API.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepo;

        public CategoryService(ICategoryRepository roomRepository)
        {
            this.categoryRepo = roomRepository;
        }

        public async Task<CategoryEntity> GetCategoryBySlugAsync(string slug)
        {
            return await categoryRepo.GetAsync(c => c.Slug == slug);
        }

        public async Task<List<CategoryEntity>> ListCategoriesAsync()
        {
            return await categoryRepo.ListAsync();
        }
    }
}
