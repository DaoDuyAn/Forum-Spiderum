using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using SocialNetwork.Application.Options;
using Microsoft.Extensions.Options;
using SocialNetwork.Infrastructure.Models;
using SocialNetwork.Infrastructure.Repositories.Data;
using SocialNetwork.Domain.Models;
using System.Numerics;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using SocialNetwork.Infrastructure.Dapper;

namespace SocialNetwork.Infrastructure.Repositories.Account
{
    public class AccountRepository : RepositoryBase<AccountEntity>, IAccountRepository
    {
        SocialNetworkDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IDataContext _dataContext;
        private readonly DapperContext dapperContext;

        public AccountRepository(SocialNetworkDbContext dbContext, IDataContext _dataContext, IOptionsMonitor<JwtSettings> optionsMonitor, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _jwtSettings = optionsMonitor.CurrentValue;
            this._dataContext = _dataContext;
            this.dapperContext = dapperContext;
        }

        public async Task<ApiResponse> LoginAsync(string username, string pass)
        {
            var account = await GetAsync(a => a.UserName == username && a.Password == pass);
            if (account == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username/password"
                };
            }

            // Cấp token
            var token = await GenerateToken(account);

            return new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = token
            };
        }

        private async Task<TokenModel> GenerateToken(AccountEntity account)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, account.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", account.UserId.ToString()),
                    new Claim("AccountId", account.Id.ToString()),


                    // roles
                    // ...
                }),

                Expires = DateTime.UtcNow.AddMinutes(30),    // Thời gian hết hạn - 30 phút,
                Issuer = "ForumSpiderum",
                Audience = "ForumSpiderumUser",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            // Lưu database
            var refreshTokenEntity = new RefreshTokenEntity
            {
                Id = Guid.NewGuid(),
                JwtId = token.Id,
                AccountId = account.Id,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };

            var rfToken = await _dataContext.RefreshTokenRepo.AddAsync(refreshTokenEntity);
            var user = await _dataContext.UserRepo.GetAsync(u => u.Id == account.UserId);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
                UserName = user.UserName,
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval = dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }

        public async Task<ApiResponse> RenewTokenAsync(Guid userId, string username, string accessToken, string refreshToken)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var tokenValidateParam = new TokenValidationParameters
            {
                // Tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                // Ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

                ValidateLifetime = false
            };

            try
            {
                // Check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(accessToken, tokenValidateParam, out var validatedToken);

                // Check 2: Check algorithm
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
                    if (!result) // false
                    {
                        return new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid token"
                        };
                    }
                }

                // Check 3: Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Access token has not yet expired"
                    };
                }

                // Check 4: Check refreshtoken exist in DB
                var storedToken = await _dataContext.RefreshTokenRepo.GetAsync(x => x.Token == refreshToken);
                if (storedToken == null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token does not exist"
                    };
                }

                // Check 5: check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token has been used"
                    };
                }
                if (storedToken.IsRevoked)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Refresh token has been revoked"
                    };
                }

                // Check 6: AccessToken id == JwtId in RefreshToken
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Token doesn't match"
                    };
                }

                // Update token is used
                storedToken.IsRevoked = true;
                storedToken.IsUsed = true;

                var tokenUpdate = await _dataContext.RefreshTokenRepo.UpdateAsync(storedToken);

                // Create new token
                var user = await GetAsync(nd => nd.Id == storedToken.AccountId);
                var token = await GenerateToken(user);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Renew token success",
                    Data = token
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Something went wrong"
                };
            }
        }

        public async Task<int> AddAccountAsync(string username, string pass, string fullname, string phone, string roleName)
        {
            var role = await _dataContext.RoleRepo.GetAsync(r => r.RoleName == roleName);

            var parameters = new DynamicParameters();

            parameters.Add("@UserName", username);
            parameters.Add("@FullName", fullname);
            parameters.Add("@Phone", phone);
            parameters.Add("@RoleId", role.Id, DbType.Guid);
            parameters.Add("@Password", pass);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_AddUserAndAccount",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> ChangePasswordAsync(string oldPass, string newPass, string confirmPass, Guid userId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@OldPassword", oldPass);
            parameters.Add("@NewPassword", newPass);
            parameters.Add("@ConfirmPassword", confirmPass);
            parameters.Add("@UserId", userId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_Account_ChangePassword",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }
    }
}
