using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Followers")]
    public class FollowerEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
        public Guid FollowerId { get; set; }
        [ForeignKey("FollowerId")]
        public UserEntity Follower { get; set; }
    }
}
