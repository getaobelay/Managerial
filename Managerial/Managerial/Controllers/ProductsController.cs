using DAL.Core.Cqrs.Common.Commands.Requests;
using DAL.Core.Cqrs.Common.Queries.Requests;
using DAL.Core.loC;
using DAL.Models;
using DAL.ViewModels;
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
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController()
        {
            mediator = MediatorContainer.BuildMediator();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        [ProducesResponseType(200, Type = typeof(List<ProductViewModel>))]
        public async Task<IActionResult> GetAllAsync([FromRoute] int page, [FromRoute] int pageSize)
        {
            var query = new ListQueryRequest<Product, ProductViewModel>();
            var result = await mediator.Send(query);

            return Ok(result.Dtos);
        }

        [HttpGet("{Id:int}")]
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int Id)
        {
            var query = new SingleQueryRequest<Product, ProductViewModel>()
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
        public async Task<IActionResult> PostAsync([FromBody] ProductViewModel createdObject)
        {
            try
            {
                var query = new CreateCommandRequest<Product, ProductViewModel>()
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
            catch (System.Exception)
            {

                throw;
            }
  

        }

        [HttpPut("{Id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAsync([FromRoute] int Id, [FromBody] ProductViewModel updatedObject)
        {
            var query = new UpdateCommandRequest<Product, ProductViewModel>()
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
        [ProducesResponseType(200, Type = typeof(ProductViewModel))]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int Id)
        {
            var query = new DeleteCommandRequest<Product, ProductViewModel>()
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