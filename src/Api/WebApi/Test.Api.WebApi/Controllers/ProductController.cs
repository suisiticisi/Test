using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.Api.Application.Features.Queries.GetProducts;

namespace Test.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator) 
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
        {
            var entries = await mediator.Send(query);

            return Ok(entries);
        }

    }
}
