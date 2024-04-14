using SocialNetwork.Infrastructure.Repositories.Category;
using SocialNetwork.Infrastructure.Repositories.Post;
using SocialNetwork.Infrastructure.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.Data
{
    public interface IDataContext
    {
        ICategoryRepository CategoryRepo { get; }
        IPostRepository PostRepo { get; }
        IUserRepository UserRepo { get; }
    }
}
