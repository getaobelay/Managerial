using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Managerial.Controllers.Intefaces;
using Domain.Common;
using Application.ViewModels;
using Application.Common.Queries.Requests;
using Application.Common.Commands.Requests;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;

namespace WarehouseAngularApp.Managerial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]

    public abstract class BaseApiController<TEntity, TDto> : ControllerBase, IAPIController<TDto>
        where TEntity : AuditableEntity, new()
        where TDto : BaseViewModel, new()
    {
        private readonly IMediator _mediator;

        public IMediator Mediator => _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetAllAsync([FromRoute] int page, [FromRoute] int pageSize)
        {
            var query = new ListQueryRequest<TEntity, TDto>();
            var result = await Mediator.Send(query);
            return Ok((IEnumerable<TDto>)result.Object);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int Id)
        {
            var query = new SingleQueryRequest<TEntity, TDto>()
            {
                Id = Id,
                Filter = p => p.Id == Id
            };

            var result = await Mediator.Send(query);

            if (result.Object != null)
            {
                return Ok((TDto)result.Object);

            }
            return NotFound(Id);

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TDto createdObject)
        {

            var query = new CreateCommandRequest<TEntity, TDto>()
            {
                CreateObject = createdObject
            };

            var result = await Mediator.Send(query);

            if (result.Error)
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
        public async Task<IActionResult> PutAsync([FromRoute] int Id, [FromBody] TDto updatedObject)
        {
            var query = new UpdateCommandRequest<TEntity, TDto>()
            {
                Id = Id,
                UpdatedObject = updatedObject
            };

            var result = await Mediator.Send(query);

            if (result.Error)
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
            var query = new DeleteCommandRequest<TEntity, TDto>()
            {
                Id = Id
            };

            var result = await Mediator.Send(query);

            if (result.Error)
            {
                return BadRequest();
            }

            return Ok(result.ViewModel);
        }

    }
}
