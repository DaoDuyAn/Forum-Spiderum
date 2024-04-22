using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs.Post
{
    public class GetPostBySlugResponseDTO
    {
        public PostInfo PostInfo { get; set; }
        public UserInfo UserInfo { get; set; }
        public PostCategoryInfo PostCategoryInfo { get; set; }
    }

    public class PostInfo
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Content { get; set; } = "";
        public string CreationDate { get; set; }
        public string ThumbnailImagePath { get; set; } = "";
        public string Slug { get; set; } = "";
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public int SavedCount { get; set; }
    }

    public class UserInfo
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Description { get; set; } = "";
        public string AvatarImagePath { get; set; } = "";
    }

    public class PostCategoryInfo
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Slug { set; get; } = "";
    }
}
