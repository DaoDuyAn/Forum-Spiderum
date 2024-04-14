using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.Post
{
    public interface IPostRepository : IAsyncRepository<PostEntity>
    {
        Task<PostEntity> GetPostBySlugAsync(string slug);
    }
}
