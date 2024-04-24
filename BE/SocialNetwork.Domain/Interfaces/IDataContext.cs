using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IDataContext
    {
        ICategoryRepository CategoryRepo { get; }
        IPostRepository PostRepo { get; }
        IUserRepository UserRepo { get; }
        IRefreshTokenRepository RefreshTokenRepo { get; }
        IRoleRepository RoleRepo { get; }
    }
}
