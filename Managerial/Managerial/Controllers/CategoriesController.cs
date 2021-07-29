using Application.ViewModels;
using Domain.Entites;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
    public  class CategoriesController : BaseApiController<Category, CategoryViewModel>
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator): base(mediator)
        {
            this.mediator = mediator;
        }

    }
}