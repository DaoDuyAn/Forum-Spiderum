using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.EntitiesConfig
{
    public class UserCategoryFollowingConfiguration : IEntityTypeConfiguration<UserCategoryFollowingEntity>
    {
        public void Configure(EntityTypeBuilder<UserCategoryFollowingEntity> builder)
        {
            builder
              .HasOne(f => f.User)
              .WithMany()
              .HasForeignKey(f => f.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(f => f.Category)
              .WithMany()
              .HasForeignKey(f => f.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }

  
}
