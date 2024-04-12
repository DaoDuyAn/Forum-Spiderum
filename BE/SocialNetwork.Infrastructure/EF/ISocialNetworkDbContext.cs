using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.EF
{
    public interface ISocialNetworkDbContext
    {
        DbSet<AccountEntity> Accounts { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<CategoryEntity> Categories { get; set; }
        DbSet<CommentEntity> Comments { get; set; }
        DbSet<FollowerEntity> Followers { get; set; }
        DbSet<FollowingEntity> Followings { get; set; }
        DbSet<UserCategoryFollowingEntity> UserCategoryFollowings { get; set; }
        DbSet<LikeEntity> Likes { get; set; }
        DbSet<PostEntity> Posts { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<SavedPostEntity> SavedPosts { get; set; }
    }
}
