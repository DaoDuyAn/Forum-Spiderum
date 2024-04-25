using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Interfaces
{
    public interface IAccountRepository : IAsyncRepository<AccountEntity>
    {
        Task<ApiResponse> LoginAsync(string username, string pass);
        Task<ApiResponse> RenewTokenAsync(Guid userId, string username, string accessToken, string refreshToken);
        Task<int> AddAccountAsync(string username, string pass, string fullname, string phone, string roleName);

        Task<int> ChangePasswordAsync(string oldPass, string newPass, string confirmPass, Guid userId);
    }
}
