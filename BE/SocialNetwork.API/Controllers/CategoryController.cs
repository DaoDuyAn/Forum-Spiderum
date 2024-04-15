using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialNetwork.API.Services.Category;
using SocialNetwork.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.DTOs;
using SocialNetwork.API.DTOs.Category;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger
            , ICategoryService service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAllCategories")]
        public async Task<List<CategoryEntity>> GetAllCategories()
        {
            return await _service.ListCategoriesAsync();
        }

        [HttpGet("GetCategoryBySlug/slug/{slug}")]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            try
            {
                var request = new GetCategoryBySlugRequest { Slug = slug };
                var category = await _service.GetCategoryBySlugAsync(request);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the category by slug.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetCategoryById/id/{Id}")]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            try
            {
                var request = new GetCategoryByIdRequest { Id = Id };
                var category = await _service.GetCategoryByIdAsync(request);

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the category by Id.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromForm] AddCategoryRequest request)
        {
            try
            {
                var category = await _service.AddCategoryAsync(request);
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the category.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryRequest request)
        {
            try
            {
                var cate = await _service.UpdateCategoryAsync(request);

                if (cate == null)
                {
                    return NotFound(StatusCodes.Status404NotFound);
                }

                return Ok(cate);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete("DeleteCategoryById/id/{Id}")]
        public async Task<bool> DeleteCategoryById(string Id)
        {
            return await _service.DeleteCategoryAsync(Guid.Parse(Id));
        }
    }
}
