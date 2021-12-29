using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Application.Features.Products.Commands.CreateProduct;
using NetCoreWebApp.Application.Features.Products.Commands.DeleteProduct;
using NetCoreWebApp.Application.Features.Products.Commands.UpdateProduct;
using NetCoreWebApp.Application.Features.Products.Queries.GetAllProducts;
using NetCoreWebApp.Application.Features.Products.Queries.GetProductById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApp.Product.Microservice.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Post), response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            var response = await Mediator.Send(command);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok(response);
        }
    }
}
