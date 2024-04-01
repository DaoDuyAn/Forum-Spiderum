using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Roles")]
    public class RoleEntity : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = "";

        public ICollection<AccountEntity>? Accounts { get; set; }
    }
}
