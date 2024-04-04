using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.HasData(
                new PostEntity
                {
                    Id = new Guid("e7d47232-8178-487a-8a39-57dfe86b0ad7"),
                    Title = "Hôm qua Eden giải nghệ.",
                    Description = "Này kia",
                    Content = "Hi, tôi là fan hâm mộ của Chelsea Fc được đến nay 11 năm.",
                    CategoryId = new Guid("6f906c89-37a4-4dfc-bd4f-2c2b6b3b17af"),
                    UserId = new Guid("b3d6e9a2-3b26-46a9-8a10-642ab9fd8d91"),
                }    
            );
        }
    }
}
