using Application.ViewModels;
using Domain.Entites;
using MediatR;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public class OrdersController : BaseApiController<Order, OrderViewModel>
    {
        public OrdersController(IMediator mediator): base(mediator)
        {

        }
    }
      
}