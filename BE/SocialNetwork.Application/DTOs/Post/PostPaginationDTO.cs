using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.Post
{
    public class PostPaginationDTO
    {
        public List<PostResponseDTO> postResponse { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
    }
}
