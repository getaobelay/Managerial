using Application.ViewModels;
using Domain.Entites;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public class WarehousesController : BaseApiController<Warehouse, WarehouseViewModel>
    {
        public WarehousesController(IMediator mediator): base(mediator)
        {

        }
    }
      
}