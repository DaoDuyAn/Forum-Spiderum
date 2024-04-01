using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Comments")]
    public class CommentEntity : BaseEntity
    {
        public UserEntity User { get; set; }
        public PostEntity Post { get; set; }
        public string Text { get; set; }
        public Guid RepliedCommentId { get; set; }
    }
}
