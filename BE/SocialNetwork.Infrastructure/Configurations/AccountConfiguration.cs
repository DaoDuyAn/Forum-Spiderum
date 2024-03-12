using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasData(
                new AccountEntity
                {
                    Id = new Guid("7a2346c5-45b6-48f2-9dbd-aae06a701701"),
                    UserName = "duyan",
                    Password = "123123",
                    RoleId = new Guid("a600280d-e519-41d5-998f-82fd428af9f3"),
                    UserId = new Guid("bf33c1a4-8330-4c60-ba0b-2a91f6fb95bb"),
                },
                new AccountEntity
                {
                    Id = new Guid("7a2346c5-45b6-48f2-9dbd-aae06a701701"),
                    UserName = "buihieu",
                    Password = "123123",
                    RoleId = new Guid("a02a928b-425c-45dc-9441-82cae13dc44a"),
                    UserId = new Guid("b3d6e9a2-3b26-46a9-8a10-642ab9fd8d91"),
                }
            );
        }
    }
}
