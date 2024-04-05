using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Services.Category;
using SocialNetwork.Domain.Entities;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/[controller]")]
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

        [HttpGet]
        public async Task<List<CategoryEntity>> GetAllCategories()
        {
            return await _service.ListCategoriesAsync();
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            try
            {
                var category = await _service.GetCategoryBySlugAsync(slug);

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
    }
}
