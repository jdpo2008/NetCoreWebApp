using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Application.Features.SubCategories.Commands.CreateSubCategory;
using NetCoreWebApp.Application.Features.SubCategories.Commands.DeleteSubCategory;
using NetCoreWebApp.Application.Features.SubCategories.Commands.UpdateSubCategory;
using NetCoreWebApp.Application.Features.SubCategories.Queries.GetAllSubCategories;
using NetCoreWebApp.Application.Features.SubCategories.Queries.GetSubCategoryById;
using System;
using System.Threading.Tasks;

namespace NetCoreWebApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class SubCategoryController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllSubCategoriesRequest filter)
        {
            return Ok(await Mediator.Send(new GetAllSubCategoriesQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetSubCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateSubCategoryCommand command)
        {
            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(Post), response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateSubCategoryCommand command)
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
            var response = await Mediator.Send(new DeleteSubCategoryCommand { Id = id });

            return Ok(response);
        }
    }
}
