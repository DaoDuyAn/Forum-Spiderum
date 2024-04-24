using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.Repositories.Category;
using SocialNetwork.Infrastructure.Repositories.Post;
using SocialNetwork.Infrastructure.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.Data
{
    public class DataContext : IDataContext
    {
        public ICategoryRepository CategoryRepo { get; set; }

        public IPostRepository PostRepo { get; set; }

        public IUserRepository UserRepo { get; set; }
        public IRefreshTokenRepository RefreshTokenRepo { get; set; }
        public IRoleRepository RoleRepo { get; set; }


        public DataContext(ICategoryRepository categoryRepo, IPostRepository postRepo, IUserRepository userRepo, IRefreshTokenRepository refreshTokenRepo, IRoleRepository roleRepo)
        {
            CategoryRepo = categoryRepo;
            PostRepo = postRepo;
            UserRepo = userRepo;
            RefreshTokenRepo = refreshTokenRepo;
            RoleRepo = roleRepo;
        }
    }
}
