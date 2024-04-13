using Microsoft.Extensions.Hosting;
using SocialNetwork.API.DTOs;
using SocialNetwork.API.Utilities;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Repositories.Category;
using SocialNetwork.Infrastructure.Repositories.Post;
using System.Text.RegularExpressions;

namespace SocialNetwork.API.Services.Post
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepo;
        private static IWebHostEnvironment? _hostEnvironment;

        public PostService(IPostRepository postRepo, IWebHostEnvironment hostEnvironment)
        {
            this.postRepo = postRepo;
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException();
        }

        public async Task<PostEntity> AddPostAsync(AddPostRequest model)
        {
            string thumbnailImagePath = "";

            Guid guid = Guid.NewGuid();
            string[] guidParts = guid.ToString().Split('-');

            string postSlug = AppUtilities.GenerateSlug(model.Title) + "-" + guidParts[0];

            string folderPath = CreateFolder(postSlug);

            string jsonString = model.Content;
            // Define regular expression pattern to match URLs
            string pattern = @"(""url"":\s*""([^""]+)"")";

            MatchCollection matches = Regex.Matches(jsonString, pattern);

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string originalUrl = match.Groups[2].Value;
                IFormFile formFile = Base64ToImage(originalUrl, postSlug + "_" + i);
                string imgPath = await UploadImage(formFile, postSlug);

                jsonString = jsonString.Replace(originalUrl, imgPath);

                if (i == 0)
                {
                    thumbnailImagePath = imgPath;
                }
            }

            // Tạo entity mới
            var newPost = new PostEntity
            {
                Title = model.Title,
                Content = jsonString,
                Slug = postSlug,
                Description = model.Description,
                UserId = Guid.Parse(model.UserId),
                CategoryId = Guid.Parse(model.CategoryId),
                CreationDate = DateTime.Now,
                ThumbnailImagePath = thumbnailImagePath
            };

            return await postRepo.AddAsync(newPost);
        }

        #region Handle Image
        public static string GetBase64String(string path)
        {
            var encode = path.Split(",")[1];

            return encode;
        }

        public static string CreateFolder(string folderName)
        {
            string path = _hostEnvironment.WebRootPath + "\\images\\" + "\\posts\\" + folderName + "\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static string GetExtensionFromBase64(string path)
        {
            var contentType = path.Split(",")[0];
            var encode = path.Split(",")[1];
            string extension = "";

            switch (contentType)
            {
                case "data:image/jpeg;base64":
                    extension = "jpeg";
                    break;
                case "data:image/png;base64":
                    extension = "png";
                    break;
                default:
                    extension = "jpg";
                    break;
            }

            return extension;
        }

        public string SetExtensionToBase64(string extension)
        {
            string result = "";

            switch (extension)
            {
                case "jpeg":
                    result = "data:image/jpeg;base64";
                    break;
                case "png":
                    result = "data:image/png;base64";
                    break;
                default:
                    result = "data:image/jpg;base64";
                    break;
            }

            return result;
        }

        public string GetExtesionFromFilePath(string filePath)
        {
            var name = filePath.Split('.');

            string fileExt = name[1];

            return fileExt;
        }

        public string ImageToBase64(string path)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(path);

            string base64ImageString = SetExtensionToBase64(GetExtesionFromFilePath(path)) + "," + Convert.ToBase64String(imageArray);

            return base64ImageString;
        }

        public IFormFile Base64ToImage(string base64String, string fileName)
        {
            var contentTypeTemp = base64String.Split(",")[0];
            var encode = base64String.Split(",")[1];

            var contentTypeTemp_1 = contentTypeTemp.Split(":")[1];
            var contentType = contentTypeTemp_1.Split(";")[0];

            var bytes = Convert.FromBase64String(encode);
            MemoryStream stream = new MemoryStream(bytes);

            string extension = GetExtensionFromBase64(base64String);
            string fullFileName = fileName + "." + extension;

            var file = new FormFile(stream, 0, stream.Length, "0", fullFileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };

            return file;
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

        public async Task<PostEntity> UpdatePostAsync(UpdatePostRequest model)
        {
            throw new NotImplementedException();
        }

        public async Task<PostEntity> GetPostBySlugAsync(string slug)
        {
            var post = await postRepo.GetAsync(c => c.Slug == slug);

            post.ThumbnailImagePath = ImageToBase64(post.ThumbnailImagePath);

            var jsonString = post.Content;
            string pattern = @"(""url"":\s*""([^""]+)"")";

            MatchCollection matches = Regex.Matches(jsonString, pattern);

            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];
                string originalurl = match.Groups[2].Value;

                string imgpath = ImageToBase64(originalurl);

                jsonString = jsonString.Replace(originalurl, imgpath);

            }

            post.Content = jsonString;

            return post;
        }
    }
}
