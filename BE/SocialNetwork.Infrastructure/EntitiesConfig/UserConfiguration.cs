using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasData(
                new UserEntity
                {
                    Id = new Guid("bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb"),
                    UserName = "duyan",
                    FullName = "Duy An",
                    Email = "duyan@gmail.com"
                },
                 new UserEntity
                 {
                     Id = new Guid("b3d6e9a2-3b26-46a9-8a10-642ab9fd8d91"),
                     UserName = "buihieu",
                     FullName = "Bùi Xuân Hiếu",
                     Email = "buihieu@gmail.com"
                 }
            );
        }
    }
}
