using Application.ViewModels;
using Domain.Entites;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public  class StocksController : BaseApiController<Stock, StockViewModel>
    {
        private readonly IMediator mediator;

        public StocksController(IMediator mediator): base(mediator)
        {
            this.mediator = mediator;
        }

    }
}