using Application.ViewModels;
using Domain.Entites;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public  class AllocationsController : BaseApiController<Allocation, AllocationViewModel>
    {
        private readonly IMediator mediator;

        public AllocationsController(IMediator mediator): base(mediator)
        {
            this.mediator = mediator;
        }

    }
}