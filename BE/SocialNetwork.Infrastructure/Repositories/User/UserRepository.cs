using Dapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.Dapper;
using SocialNetwork.Infrastructure.EF;
using SocialNetwork.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Repositories.User
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        SocialNetworkDbContext _dbContext;
        private readonly DapperContext dapperContext;

        public UserRepository(SocialNetworkDbContext dbContext, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            this.dapperContext = dapperContext;
        }

        public async Task<int> AddLikePostAsync(Guid UserId, Guid PostId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@PostId", PostId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_AddLikePost",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> UnlikePostAsync(Guid UserId, Guid PostId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@PostId", PostId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_UnlikePost",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> AddSavedPostAsync(Guid UserId, Guid PostId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@PostId", PostId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_AddSavedPost",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> UnsavedPostAsync(Guid UserId, Guid PostId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@PostId", PostId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_UnsavedPost",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> AddFollowCategoryAsync(Guid UserId, Guid CategoryId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@CategoryId", CategoryId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_AddFollowCategory",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> UnfollowCategoryAsync(Guid UserId, Guid CategoryId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@CategoryId", CategoryId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_UnfollowCategory",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> AddFollowUserAsync(Guid UserId, Guid FollowerId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@FollowerId", FollowerId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_AddFollowUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<int> UnfollowUserAsync(Guid UserId, Guid FollowerId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", UserId);
            parameters.Add("@FollowerId", FollowerId);
            parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (var connection = dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "proc_UnfollowUser",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }

            int resultValue = parameters.Get<int>("@Result");
            return resultValue;
        }

        public async Task<UserEntity> GetUserAsync(Expression<Func<UserEntity, bool>> expression)
        {
            var user = await GetAsync(expression);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.AvatarImagePath))
                {
                    user.AvatarImagePath = HandleImage.ImageToBase64(user.AvatarImagePath);
                }

                if (!string.IsNullOrEmpty(user.CoverImagePath))
                {
                    user.CoverImagePath = HandleImage.ImageToBase64(user.CoverImagePath);
                }

                return user;
            }

            return null;
        }

        
    }
}
