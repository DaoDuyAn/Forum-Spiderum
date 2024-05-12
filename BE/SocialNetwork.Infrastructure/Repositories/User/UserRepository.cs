using Dapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private static IWebHostEnvironment? _hostEnvironment;
        private readonly DapperContext dapperContext;

        public UserRepository(SocialNetworkDbContext dbContext, IWebHostEnvironment hostEnvironment, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
            this.dapperContext = dapperContext;
        }

        #region Handle image

        public static string CreateFolder(string folderName)
        {
            string path = _hostEnvironment.WebRootPath + "\\images\\" + "\\users\\" + folderName + "\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }



        public async Task<string> UploadImage(IFormFile file, string userName)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException("file", "No file or empty file provided.");
            }

            if (file.Length > 0)
            {
                try
                {
                    DirectoryInfo directoryInfo;
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "users", userName);


                    if (!Directory.Exists(_hostEnvironment.WebRootPath + "\\images\\"))
                    {
                        directoryInfo = Directory.CreateDirectory(_hostEnvironment.WebRootPath + "\\images\\");
                    }

                    // Tạo tên file duy nhất
                    string uniqueFileName = $"{file.FileName}";

                    // Tạo đường dẫn lưu trữ file
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Lưu file vào thư mục
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    return filePath.Replace("\\", "/");

                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

            }

            return "Not success";
        }
        #endregion

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

        public async Task<(List<UserEntity>, int, int)> SearchUserByValueAsync(string searchValue, int page)
        {
            using (var connection = dapperContext.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@page", page);
                parameters.Add("@pageSize", 5);
                parameters.Add("@searchValue", searchValue);
                parameters.Add("@rowCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@pageCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var users = await connection.QueryAsync<UserEntity>(
                     sql: "proc_Search_User_By_Value",
                     param: parameters,
                     commandType: CommandType.StoredProcedure
                 );

                var rowCount = parameters.Get<int>("@rowCount");
                var pageCount = parameters.Get<int>("@pageCount");

                var userList = users.AsList();


                foreach (var user in userList)
                {
                    if (!string.IsNullOrEmpty(user.AvatarImagePath))
                    {
                        user.AvatarImagePath = HandleImage.ImageToBase64(user.AvatarImagePath);
                    }
                }

                return (userList, rowCount, pageCount);
            }
        }

        public async Task<string> UpdateProfileAsync(UserEntity newProfile, Guid id)
        {
            var user = await GetAsync(u => u.Id == id);
            if (newProfile.Email != "")
            {
                if (AppUtilities.IsValidEmail(newProfile.Email))
                {
                    var _user = await GetAsync(u => u.Email == newProfile.Email);
                    if (_user != null)
                    {
                        if (_user.Id != id)
                        {
                            return "Trùng email";
                        }
                    }
                }
                else
                {
                    return "Email sai định dạng";
                }
            }

            user.FullName = newProfile.FullName;
            user.Description = newProfile.Description;
            user.Email = newProfile.Email;
            user.Phone = newProfile.Phone;
            user.Address = newProfile.Address;
            user.Gender = newProfile.Gender;
            user.BirthDate = newProfile.BirthDate;

            if (newProfile.AvatarImagePath != "" || newProfile.CoverImagePath != "")
            {
                // Tạo thư mục để lưu ảnh cho user nếu chưa có
                string imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "users", user.UserName);
                if (!Directory.Exists(imagePath))
                {
                    string folderPath = CreateFolder(user.UserName);
                }
                else
                {
                    // Thư mục đang chứa các ảnh cũ, thực hiện xóa để bổ sung ảnh mới vào
                    // Lấy danh sách ảnh trong thư mục
                    string[] files = Directory.GetFiles(imagePath);

                    // Xóa tất cả các ảnh trong danh sách
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }


                if (newProfile.AvatarImagePath != "")
                {
                    IFormFile formFile = HandleImage.Base64ToImage(newProfile.AvatarImagePath, user.UserName + "_avatar");
                    string avatarPath = await UploadImage(formFile, user.UserName);
                    user.AvatarImagePath = avatarPath;
                }

                if (newProfile.CoverImagePath != "")
                {
                    IFormFile formFile = HandleImage.Base64ToImage(newProfile.AvatarImagePath, user.UserName + "_cover");
                    string coverPath = await UploadImage(formFile, user.UserName);
                    user.CoverImagePath = coverPath;
                }
            }

            var res = await UpdateAsync(user);
            return res.Id.ToString();
        }
    }
}
