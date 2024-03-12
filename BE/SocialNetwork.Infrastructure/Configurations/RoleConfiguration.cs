using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasData(
                new RoleEntity
                {
                    Id = new Guid("a600280d-e519-41d5-998f-82fd428af9f3"),
                    RoleName = "Admin"
                },

                new RoleEntity
                {
                    Id = new Guid("a02a928b-425c-45dc-9441-82cae13dc44a"),
                    RoleName = "User"
                }
            );
        }
    }
}
