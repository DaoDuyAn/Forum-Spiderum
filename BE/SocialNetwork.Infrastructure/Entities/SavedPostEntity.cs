using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Entities
{
    [Table("SavedPosts")]
    public class SavedPostEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }

        public Guid PostId { get; set; }
        [ForeignKey("PostId")]
        public PostEntity Post { get; set; }
    }
}
