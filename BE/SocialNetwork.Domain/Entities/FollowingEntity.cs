using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Followings")]
    public class FollowingEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
        public Guid FollowingId { get; set; }
        [ForeignKey("FollowingId")]
        public UserEntity Following { get; set; }
    }
}
