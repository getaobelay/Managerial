using Application.Extensions.Containers.Handlers;
using Application.ViewModels;
using Autofac;
using Domain.Entites;
using MediatR;
using System.Reflection;

namespace Application.Extensions.Containers
{
    public static class RegisterApplicationContainer
    {
        public static ContainerBuilder RegisterApplication(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator)
                   .GetTypeInfo().Assembly)
                   .AsImplementedInterfaces();

            builder.RegisterHandlers<Warehouse, WarehouseViewModel>();
            builder.RegisterHandlers<WarehouseItem, WarehouseItemViewModel>();
            builder.RegisterHandlers<Product, ProductViewModel>();
            builder.RegisterHandlers<Order, OrderViewModel>();
            builder.RegisterHandlers<Batch, BatchViewModel>();
            builder.RegisterHandlers<Category, CategoryViewModel>();
            builder.RegisterHandlers<Customer, CustomerViewModel>();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();

                return t => c.Resolve(t);
            });

            return builder;
        }
    }
}