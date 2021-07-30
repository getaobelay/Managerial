using Application.ViewModels;
using Domain.Entites;
using MediatR;
using WarehouseAngularApp.Managerial.Controllers;

namespace Managerial.Controllers
{
    public class ProductsController : BaseApiController<Product, ProductViewModel>
    {
        public ProductsController(IMediator mediator): base(mediator)
        {

        }
    }
      
}