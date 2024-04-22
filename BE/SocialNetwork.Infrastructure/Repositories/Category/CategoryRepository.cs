using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Domain.Entities;
using SocialNetwork.Domain.Interfaces;
using SocialNetwork.Infrastructure.EF;
using SocialNetwork.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SocialNetwork.Infrastructure.Repositories.Category
{
    public class CategoryRepository : RepositoryBase<CategoryEntity>, ICategoryRepository
    {
        SocialNetworkDbContext _dbContext;
        private static IWebHostEnvironment? _hostEnvironment;

        public CategoryRepository(SocialNetworkDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<CategoryEntity> GetCategoryAsync(Expression<Func<CategoryEntity, bool>> expression)
        {
            var cate = await GetAsync(expression);
            if (cate.CoverImagePath != "")
            {
                cate.CoverImagePath = HandleImage.ImageToBase64(cate.CoverImagePath);
            }

            return cate;
        }

        public async Task<string> AddCategoryAsync(CategoryEntity entity)
        {
            // Tạo slug từ CategoryName
            string slug = AppUtilities.GenerateSlug(entity.CategoryName);

            IFormFile formFile = HandleImage.Base64ToImage(entity.CoverImagePath, slug);
            string imgPath = await UploadImage(formFile);

            entity.Slug = slug;
            entity.CoverImagePath = imgPath;

            var categoryCreate = await AddAsync(entity);

            return categoryCreate.Id.ToString();
        }

        public async Task<Guid> UpdateCategoryAsync(CategoryEntity newCategory)
        {
            var oldCategory = await GetAsync(c => c.Id == newCategory.Id);
            if(oldCategory.CategoryName != oldCategory.CategoryName)
            {
                string slug = AppUtilities.GenerateSlug(newCategory.CategoryName);
                oldCategory.Slug = slug;
                oldCategory.CategoryName = newCategory.CategoryName;
            }

            if (newCategory.CoverImagePath != "")
            {
                // Xóa ảnh của danh mục (nếu có)
                string imagePath = oldCategory.CoverImagePath;
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }

                IFormFile formFile = HandleImage.Base64ToImage(newCategory.CoverImagePath, oldCategory.Slug);
                string imgPath = await UploadImage(formFile);
                oldCategory.CoverImagePath = imagePath;
            }

            if (newCategory.ContentAllowed != "")
            {
                oldCategory.ContentAllowed = newCategory.ContentAllowed;
            }

            var res = await UpdateAsync(oldCategory);
            return res.Id;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var cate = await GetAsync(c => c.Id == id);
            if (cate != null)
            {
                // Xóa ảnh của danh mục (nếu có)
                string imagePath = cate.CoverImagePath;
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }

                return await DeleteAsync(cate);
            }

            return false;
        }
    }
}
