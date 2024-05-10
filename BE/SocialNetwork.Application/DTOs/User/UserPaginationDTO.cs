
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.User
{
    public class UserPaginationDTO
    {
        public List<UserSearchResponseDTO> userResponse { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
    }
}
