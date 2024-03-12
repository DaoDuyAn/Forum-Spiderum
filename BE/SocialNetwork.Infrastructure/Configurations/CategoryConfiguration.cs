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
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.HasData(
                new CategoryEntity
                {
                    Id = new Guid("6f906c89-37a4-4dfc-bd4f-2c2b6b3b17af"),
                    CategoryName = "Quan điểm - Tranh luận",
                    ContentAllowed = "Các nội dung thể hiện góc nhìn, quan điểm đa chiều về các vấn đề kinh tế, văn hoá – xã hội trong và ngoài nước."
                },  
                new CategoryEntity
                {
                    Id = new Guid("d3f820ec-7e1d-4c57-bf2c-5212d8c5db65"),
                    CategoryName = "Khoa học - Công nghệ",
                    ContentAllowed = "Những tri thức, hiểu biết có liên quan tới các phát minh, xu hướng, lý thuyết trong tất cả các lĩnh vực khoa học cơ bản, tâm lý học, triết học, công nghệ..."
                }, new CategoryEntity
                {
                    Id = new Guid("a7b7fd22-97f7-4ae0-8f11-7fe83c22d812"),
                    CategoryName = "Thể thao",
                    ContentAllowed = "Tất cả những nội dung và trao đổi liên quan tới thể thao trong nước và quốc tế."
                }, new CategoryEntity
                {
                    Id = new Guid("9d1b2b4a-932d-45e0-bf39-8acbf3f727a6"),
                    CategoryName = "Sách",
                    ContentAllowed = "Tổng hợp tất cả những nội dung liên quan tới sách: review, thảo luận về nội dung sách và văn hoá đọc."
                }, new CategoryEntity
                {
                    Id = new Guid("0e48f9c1-912f-4268-b82f-05ec8b1c77e9"),
                    CategoryName = "Game",
                    ContentAllowed = "Review, walkthrough và phân tích game dành cho các game thủ thực thụ."
                }
            );
        }
    }
}
