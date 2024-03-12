using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Entities
{
    [Table("Accounts")]
    public class AccountEntity : BaseEntity
    {
        [StringLength(30)]
        public string UserName { get; set; } = "";

        [StringLength(50)]
        public string Password { get; set; } = "";

        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public RoleEntity Role { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey("UserId")]

        public UserEntity? User { get; set; }
    }
}
