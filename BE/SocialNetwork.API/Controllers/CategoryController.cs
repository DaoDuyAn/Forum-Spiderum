using Microsoft.AspNetCore.Mvc;
using MediatR;
using SocialNetwork.Application.DTOs.Category;
using SocialNetwork.Application.Queries.Category;
using System.Net;
using SocialNetwork.Application.Commands.Category.Delete;
using SocialNetwork.Application.Commands.Category.Create;
using SocialNetwork.Application.Commands.Category.Update;

namespace SocialNetwork.API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger
           , IMediator mediator)
        {
            _mediator = mediator;
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

        [HttpPost("CreateCategory")]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("UpdateCategory")]
        [ProducesDefaultResponseType(typeof(Guid))]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("DeleteCategoryById/id/{Id}")]
        public async Task<bool> DeleteCategoryById(string Id)
        {
            return await _mediator.Send(new DeleteCategoryCommand { Id = Id });
        }
    }
}
