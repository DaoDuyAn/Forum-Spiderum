using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.Post
{

    public class PostResponseDTO
    {
        public PostDetailInfo PostInfo { get; set; }
        public UserDetailInfo UserInfo { get; set; }
        public PostCategoryDetailInfo PostCategoryInfo { get; set; }
    }

    public class PostDetailInfo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string CreationDate { get; set; }
        public string ThumbnailImagePath { get; set; } = "";
        public string Slug { get; set; } = "";
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }

    public class UserDetailInfo
    {
        public string FullName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string AvatarImagePath { get; set; } = "";
    }

    public class PostCategoryDetailInfo
    {
        public string CategoryName { get; set; }
        public string Slug { set; get; } = "";
    }
}
