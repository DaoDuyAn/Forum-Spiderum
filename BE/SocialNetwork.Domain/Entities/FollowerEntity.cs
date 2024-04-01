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
        public UserEntity User { get; set; }
        public UserEntity Follower { get; set; }
    }
}
