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
        public UserEntity User { get; set; }
        public UserEntity Following { get; set; }
    }
}
