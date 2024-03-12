using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Entities
{
    [Table("UserCategoryFollowings")]
    public class UserCategoryFollowingEntity : BaseEntity
    {
        public UserEntity User { get; set; }
        public CategoryEntity Category { get; set; }
    }
}
