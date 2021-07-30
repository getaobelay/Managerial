using Application.ViewModels;
using Domain.Entites;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public class WarehouseItemsController : BaseApiController<WarehouseItem, WarehouseItemViewModel>
    {
        public WarehouseItemsController(IMediator mediator) : base(mediator)
        {

        }
    }
      
}