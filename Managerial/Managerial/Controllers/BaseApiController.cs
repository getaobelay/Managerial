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
    public abstract class BaseApiController<TEntity, TViewModel> : ControllerBase
        where TEntity: AuditableEntity, new()
        where TViewModel: BaseViewModel, new()
    {
        private readonly IMediator mediator;

        public BaseApiController()
        {
            mediator = MediatorContainer.BuildMediator();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetAllAsync([FromRoute] int page, [FromRoute] int pageSize)
        {
            var query = new ListQueryRequest<TEntity, TViewModel>();
            var result = await mediator.Send(query);

            return Ok(result.Dtos);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int Id)
        {
            var query = new SingleQueryRequest<TEntity, TViewModel>()
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
        public async Task<IActionResult> PostAsync([FromBody] TViewModel createdObject)
        {
            try
            {
                var query = new CreateCommandRequest<TEntity, TViewModel>()
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
        public async Task<IActionResult> PutAsync([FromRoute] int Id, [FromBody] TViewModel updatedObject)
        {
            var query = new UpdateCommandRequest<TEntity, TViewModel>()
            {
                Id = Id,
                UpdatedObject = updatedObject
            };

            var result = await mediator.Send(query);

            if (!result.Success)
            {
                return BadRequest($"{nameof(TEntity)} cannot be null");
            }

            return Ok(result.ViewModel);
        }

        [HttpDelete("{Id:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int Id)
        {
            var query = new DeleteCommandRequest<TEntity, TViewModel>()
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