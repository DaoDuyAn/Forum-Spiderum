using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.User
{
    public class UserResponseDTO
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
    }
}
