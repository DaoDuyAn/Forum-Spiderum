using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Configurations;
using SocialNetwork.Infrastructure.EntitiesConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.EF
{
    public class SocialNetworkDbContext : DbContext, ISocialNetworkDbContext
    {
        public SocialNetworkDbContext() { }

        public SocialNetworkDbContext(DbContextOptions<SocialNetworkDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LAPCN-DUYAN\\SQLEXPRESS;Trusted_Connection=True;Database=SocialNetwork;MultipleActiveResultSets=true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        #region DbSet
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<FollowerEntity> Followers { get; set; }
        public DbSet<UserCategoryFollowingEntity> UserCategoryFollowings { get; set; }
        public DbSet<LikeEntity> Likes { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SavedPostEntity> SavedPosts { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new FollowerConfiguration());
            modelBuilder.ApplyConfiguration(new UserCategoryFollowingConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            modelBuilder.ApplyConfiguration(new SavedPostConfiguration());
        }

    }
}
