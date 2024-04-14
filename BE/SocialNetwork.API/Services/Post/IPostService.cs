using SocialNetwork.API.DTOs;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.API.Services.Post
{
    public interface IPostService
    {
        Task<PostEntity> AddPostAsync(AddPostRequest request);
        Task<PostEntity> UpdatePostAsync(UpdatePostRequest request);
        Task<GetPostBySlugResponse> GetPostBySlugAsync(GetPostBySlugRequest request);
        Task<bool> DeletePostAsync(Guid id);

    }
}
