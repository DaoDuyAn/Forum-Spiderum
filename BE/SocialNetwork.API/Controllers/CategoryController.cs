using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SocialNetwork.API.Services.Category;
using SocialNetwork.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.DTOs;
using SocialNetwork.API.DTOs.Category;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.Queries.Category;
using SocialNetwork.Application.DTOs.Post;
using System.Net;
using SocialNetwork.Application.Queries.Post;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger
            , ICategoryService service, IMediator mediator)
        {
            _mediator = mediator;
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetAllCategories")]
        [ProducesDefaultResponseType(typeof(List<CategoryResponseDTO>))]
        public async Task<IActionResult> GetAllCategories()
        {
           return  Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpGet("GetCategoryBySlug/slug/{slug}")]
        [ProducesResponseType(typeof(CategoryResponseDTO), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetCategoryBySlug(string slug)
        {
            var cate = await _mediator.Send(new GetCategoryBySlugQuery { Slug = slug });
            return Ok(cate);
        }

        [HttpGet("GetCategoryById/id/{Id}")]
        [ProducesResponseType(typeof(CategoryResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            var cate = await _mediator.Send(new GetCategoryByIdQuery { Id = Id });
            return Ok(cate);
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
