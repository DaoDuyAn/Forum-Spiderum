using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.User
{
    public class GetUserByIdResponseDTO
    {
        public string FullName { get; set; } = "";
        public string Description { get; set; } = "";
        public string BirthDate { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public int Gender { get; set; } 
        public string AvatarImagePath { get; set; } = "";
        public string CoverImagePath { get; set; } = "";
    }
}
