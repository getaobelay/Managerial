using Application.ViewModels;
using Domain.Entites;
using MediatR;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public class LocationsController : BaseApiController<Location, LocationViewModel>
    {
        public LocationsController(IMediator mediator): base(mediator)
        {

        }
    }
      
}