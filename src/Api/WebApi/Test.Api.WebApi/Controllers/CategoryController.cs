using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.Api.Application.Features.Commands.Category.Create;
using Test.Api.Application.Features.Queries.GetCategories;

namespace Test.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoryController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoriesQuery query)
        {
            var entries = await mediator.Send(query);

            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateCommand")]
     
        public async Task<IActionResult> CreateCommand([FromBody] CreateCategoryCommand command)
        {
            

            var result = await mediator.Send(command);

            return Ok(result);
        }
    }
}
