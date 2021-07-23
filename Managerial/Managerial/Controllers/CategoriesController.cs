using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Core.loC;
using DAL.Models;
using DAL.ViewModels;
using DAL.ViewModels.Interfaces;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Managerial.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public  class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController()
        {
            mediator = MediatorContainer.BuildMediator();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(200, Type = typeof(List<CategoryViewModel>))]
        public async Task<IActionResult> GetAllAsync([FromRoute] int pageNumber, [FromRoute] int pageSize)
        {
            var query = new ListQueryRequest<Category, CategoryViewModel>();
            var result = await mediator.Send(query);

            return Ok(result.Dtos);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(200, Type = typeof(CategoryViewModel))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int Id)
        {
            var query = new SingleQueryRequest<Category, CategoryViewModel> ()
            {
                Id = Id,
                Filter = p => p.Id == Id
            };

            var result = await mediator.Send(query);

            if (result.ViewModal != null)

                return Ok(result.ViewModal);
            else
                return NotFound(Id);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CategoryViewModel createdObject)
        {
            var query = new CreateCommandRequest<Category, CategoryViewModel> ()
            {
                CreateObject = createdObject
            };

            var result = await mediator.Send(query);

            if (!result.Success)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(PostAsync), new { id = result.ViewModel.Id }, result.ViewModel);
        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAsync([FromRoute] int Id, [FromBody] CategoryViewModel updatedObject)
        {
            var query = new UpdateCommandRequest<Category, CategoryViewModel> ()
            {
                Id = Id,
                UpdatedObject = updatedObject
            };

            var result = await mediator.Send(query);

            if (!result.Success)
            {
                return BadRequest($"{nameof(Product)} cannot be null");
            }

            return Ok(result.ViewModel);
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(200, Type = typeof(CategoryViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int Id)
        {
            var query = new DeleteCommandRequest<Category, CategoryViewModel>()
            {
                Id = Id
            };

            var result = await mediator.Send(query);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Ok(result.ViewModel);
        }
    }
}