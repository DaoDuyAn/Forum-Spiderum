using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Services.Category;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using SocialNetwork.Infrastructure.Repositories;
using SocialNetwork.Infrastructure.Repositories.Category;

namespace SocialNetwork.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
                .AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
           , IConfiguration configuration)
        {
            return services.AddDbContext<SocialNetworkDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("SocialNetworkConnectionString")));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            return services
                .AddScoped<ICategoryService, CategoryService>();
        }
    }
}
