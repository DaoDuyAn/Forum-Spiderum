using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Infrastructure.Utilities;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using SocialNetwork.Infrastructure.Repositories.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MediatR;
using SocialNetwork.Infrastructure.Dapper;
using Dapper;

namespace SocialNetwork.Infrastructure.Repositories.Post
{
    public class PostRepository : RepositoryBase<PostEntity>, IPostRepository
    {
        SocialNetworkDbContext _dbContext;
        private static IWebHostEnvironment? _hostEnvironment;
        private readonly DapperContext dapperContext;

        public PostRepository(SocialNetworkDbContext dbContext, IWebHostEnvironment hostEnvironment, DapperContext dapperContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
            this.dapperContext = dapperContext;
        }

        #region Handle image

        public static string CreateFolder(string folderName)
        {
            string path = _hostEnvironment.WebRootPath + "\\images\\" + "\\posts\\" + folderName + "\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }



        public async Task<string> UploadImage(IFormFile file, string slug)
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
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "posts", slug);


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

        public async Task<bool> DeletePostAsync(Guid id)
        {
            var post = await GetAsync(c => c.Id == id);
            if (post != null)
            {
                // Xóa thư mục ảnh của bài viết (nếu có)
                string imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "posts", post.Slug);
                if (Directory.Exists(imagePath))
                {
                    Directory.Delete(imagePath, true);
                }

                return await DeleteAsync(post);
            }

            return false;
        }

        public async Task<string> UpdatePostAsync(PostEntity request)
        {
            string thumbnailImagePath = "";
            string jsonString = request.Content;
            // Define regular expression pattern to match URLs
            string pattern = @"(""url"":\s*""([^""]+)"")";

            MatchCollection matches = Regex.Matches(jsonString, pattern);

            if (matches.Count > 0)
            {
                // Tạo thư mục để lưu ảnh cho bài viết nếu chưa có
                string imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", "posts", request.Slug);
                if (!Directory.Exists(imagePath))
                {
                    string folderPath = CreateFolder(request.Slug);
                }
                else
                {
                    // Lấy danh sách ảnh trong thư mục
                    string[] files = Directory.GetFiles(imagePath);

                    // Xóa tất cả các ảnh trong danh sách
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
            }

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string originalUrl = match.Groups[2].Value;
                IFormFile formFile = HandleImage.Base64ToImage(originalUrl, request.Slug + "_" + i);
                string imgPath = await UploadImage(formFile, request.Slug);

                jsonString = jsonString.Replace(originalUrl, imgPath);

                if (i == 0)
                {
                    thumbnailImagePath = imgPath;
                }
            }

            request.Content = jsonString;
            request.ThumbnailImagePath = thumbnailImagePath;

            var res = await UpdateAsync(request);
            return res.Slug;
        }

        public async Task<PostEntity> GetPostAsync(Expression<Func<PostEntity, bool>> expression)
        {
            var post = await GetAsync(expression);
            if (post.ThumbnailImagePath != "")
            {
                post.ThumbnailImagePath = HandleImage.ImageToBase64(post.ThumbnailImagePath);
            }

            var jsonString = post.Content;
            string pattern = @"(""url"":\s*""([^""]+)"")";

            MatchCollection matches = Regex.Matches(jsonString, pattern);

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string originalurl = match.Groups[2].Value;

                string imgpath = HandleImage.ImageToBase64(originalurl);

                jsonString = jsonString.Replace(originalurl, imgpath);

            }

            post.Content = jsonString;

            return post;
        }

        public async Task<PostEntity> AddPostAsync(PostEntity entity)
        {
            string thumbnailImagePath = "";

            Guid guid = Guid.NewGuid();
            string[] guidParts = guid.ToString().Split('-');

            string postSlug = AppUtilities.GenerateSlug(entity.Title) + "-" + guidParts[0];

            string jsonString = entity.Content;
            // Define regular expression pattern to match URLs
            string pattern = @"(""url"":\s*""([^""]+)"")";

            MatchCollection matches = Regex.Matches(jsonString, pattern);

            if (matches.Count > 0)
            {
                // Tạo thư mục để lưu ảnh cho bài viết
                string folderPath = CreateFolder(postSlug);
            }

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string originalUrl = match.Groups[2].Value;
                IFormFile formFile = HandleImage.Base64ToImage(originalUrl, postSlug + "_" + i);
                string imgPath = await UploadImage(formFile, postSlug);

                jsonString = jsonString.Replace(originalUrl, imgPath);

                if (i == 0)
                {
                    thumbnailImagePath = imgPath;
                }
            }

            entity.Content = jsonString;
            entity.ThumbnailImagePath = thumbnailImagePath;
            entity.Slug = postSlug;

            var res = await AddAsync(entity);
            return res;
        }

        public async Task<List<PostEntity>> GetAllPostsAsync()
        {
            using (var connection = dapperContext.CreateConnection())
            {
                var sql = @"
                select  p.Id,
                        p.Title,
                        p.Description,
                        p.CreationDate,
                        p.ThumbnailImagePath,
                        p.Slug,
                        p.LikesCount,
                        p.CommentsCount,
                        u.FullName AS FullName,
                        u.UserName AS UserName,
                        u.AvatarImagePath AS AvatarImagePath,
                        c.CategoryName AS CategoryName,
                        c.Slug
                from    Posts p join Users u ON p.UserId = u.Id
                                join Categories c ON p.CategoryId = c.Id";

                var posts = await connection.QueryAsync<PostEntity, UserEntity, CategoryEntity, PostEntity>(sql, (post, user, category) =>
                    {
                         post.User = user;
                         post.Category = category;
                         return post;
                    },
                    splitOn: "FullName, CategoryName"
                );

                var postList = posts.AsList();


                foreach (var post in postList)
                {
                    if (!string.IsNullOrEmpty(post.ThumbnailImagePath))
                    {
                        post.ThumbnailImagePath = HandleImage.ImageToBase64(post.ThumbnailImagePath);
                    }

                    if (!string.IsNullOrEmpty(post.User.AvatarImagePath))
                    {
                        post.User.AvatarImagePath = HandleImage.ImageToBase64(post.User.AvatarImagePath);
                    }
                }

                return postList;
            }
        }
    }
}
