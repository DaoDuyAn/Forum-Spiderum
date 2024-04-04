using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Posts")]
    public class PostEntity : BaseEntity
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreationDate { get; set; }
        public string ThumbnailImagePath { get; set; } = "";

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]

        public UserEntity? User { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public CategoryEntity? Category { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SavedCount { get; set; }
        public ICollection<ImageEntity>? Images { get; set; }

    }
}
