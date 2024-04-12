using SocialNetwork.API.DTOs;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.API.Services.Post
{
    public interface IPostService
    {
        Task<PostEntity> AddPostAsync(AddPostRequest model);
        Task<PostEntity> UpdatePostAsync(UpdatePostRequest model);


    }
}
