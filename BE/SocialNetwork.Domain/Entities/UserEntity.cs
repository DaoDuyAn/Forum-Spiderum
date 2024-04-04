using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    [Table("Users")]
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public int Gender { get; set; }
        public string AvatarImagePath { get; set; } = "";
        public string CoverImagePath { get; set; } = "";
        public AccountEntity? Account { get; set; }
        public int TotalPost { get; set; }
        public int ToltalFollower { get; set; }
        public int TotalFollowing { get; set; }
        public ICollection<PostEntity>? Posts { get; set; }
    }
}
