using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Infrastructure.Repositories.Category;
using SocialNetwork.API.Utilities;
using System;
using SocialNetwork.API.DTOs;

namespace SocialNetwork.API.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepo;
        private static IWebHostEnvironment? _hostEnvironment;

        public CategoryService(ICategoryRepository categoryRepo, IWebHostEnvironment hostEnvironment)
        {
            this.categoryRepo = categoryRepo;
            _hostEnvironment = hostEnvironment ?? throw new ArgumentNullException();
        }

        public async Task<CategoryEntity> GetCategoryBySlugAsync(string slug)
        {
            var cate = await categoryRepo.GetAsync(c => c.Slug == slug);
            cate.CoverImagePath = ImageToBase64(cate.CoverImagePath);

            return cate;
        }

        public async Task<CategoryEntity> GetCategoryByIdAsync(Guid Id)
        {
            var cate = await categoryRepo.GetAsync(c => c.Id == Id);
            cate.CoverImagePath = ImageToBase64(cate.CoverImagePath);

            return cate;
        }

        public async Task<List<CategoryEntity>> ListCategoriesAsync()
        {
            return await categoryRepo.ListAsync();
        }

        public async Task<CategoryEntity> AddCategoryAsync(AddCategoryRequest model)
        {
            // Lưu file ảnh và lấy đường dẫn
            string imagePath = await UploadImage(model.Image);

            // Tạo slug từ CategoryName
            string slug = AppUtilities.GenerateSlug(model.CategoryName);

            // Tạo entity mới
            var newCategory = new CategoryEntity
            {
                CategoryName = model.CategoryName,
                ContentAllowed = model.ContentAllowed,
                CoverImagePath = imagePath,
                Slug = slug
            };

            return await categoryRepo.AddAsync(newCategory);
        }

        public async Task<CategoryEntity> UpdateCategoryAsync(UpdateCategoryRequest model)
        {
            var cate = await categoryRepo.GetAsync(c => c.Id == Guid.Parse(model.Id));

            if (cate != null)
            {
                // Lưu file ảnh và lấy đường dẫn
                string imagePath = await UploadImage(model.Image);

                // Tạo slug từ CategoryName
                string slug = AppUtilities.GenerateSlug(model.CategoryName);

                cate.CategoryName = model.CategoryName;
                cate.ContentAllowed = model.ContentAllowed;
                cate.Slug = slug;
                cate.CoverImagePath = imagePath;

                return await categoryRepo.UpdateAsync(cate);
            }

            return null;
        }

        public async Task<bool> DeleteCategoryAsync(Guid Id)
        {
            var cate = await categoryRepo.GetAsync(c => c.Id == Id);
            if (cate != null)
            {
                // Xóa ảnh của danh mục (nếu có)
                string imagePath = cate.CoverImagePath;
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }

                return await categoryRepo.DeleteAsync(cate);
            }

            return false;
        }


        #region Handle Image
        public static string GetBase64String(string path)
        {
            var encode = path.Split(",")[1];

            return encode;
        }

        //public static string CreateFolder(string folderName)
        //{
        //    string path = _hostEnvironment.WebRootPath + "\\Images\\" + "\\" + folderName + "\\";

        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    return path;
        //}

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

        //public List<IFormFile> Base64ToImage(List<string> listBase64String, string nameFolder)
        //{
        //    DirectoryInfo directoryInfo;
        //    List<IFormFile> images = new List<IFormFile>();

        //    // When Root path is null
        //    if (string.IsNullOrWhiteSpace(_hostEnvironment.WebRootPath))
        //    {
        //        _hostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    }

        //    string path = _hostEnvironment.WebRootPath + "\\Images\\";

        //    if (!Directory.Exists(path))
        //    {
        //        directoryInfo = Directory.CreateDirectory(path);
        //    }

        //    int i = 0;
        //    //
        //    foreach (var s in listBase64String)
        //    {
        //        var contentTypeTemp = s.Split(",")[0];
        //        var encode = s.Split(",")[1];

        //        var contentTypeTemp_1 = contentTypeTemp.Split(":")[1];
        //        var contentType = contentTypeTemp_1.Split(";")[0];

        //        //File.WriteAllBytes(path + i.ToString(), Convert.FromBase64String(encode));

        //        var bytes = Convert.FromBase64String(encode);
        //        MemoryStream stream = new MemoryStream(bytes);

        //        var file = new FormFile(stream, 0, stream.Length, i.ToString(), i.ToString())
        //        {
        //            Headers = new HeaderDictionary(),
        //            ContentType = contentType
        //        };

        //        images.Add(file);


        //    }

        //    return images;

        //}

        public async Task<string> UploadImage(IFormFile file)
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
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images", "categories");


                    if (!Directory.Exists(_hostEnvironment.WebRootPath + "\\images\\"))
                    {
                        directoryInfo = Directory.CreateDirectory(_hostEnvironment.WebRootPath + "\\images\\");
                    }

                    // Tạo tên file duy nhất
                    string uniqueFileName = $"{DateTime.Now.Ticks}_{file.FileName}";

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

    }
}
