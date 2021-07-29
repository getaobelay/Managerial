using Autofac;
using DAL.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DAL.Core.loC
{
    public static class MediatorContainer
    {
        public static IMediator BuildMediator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterHandlers<Allocation, AllocationViewModel>();
            builder.RegisterHandlers<Warehouse, WarehouseViewModel>();
            builder.RegisterHandlers<Location, LocationViewModel>();
            builder.RegisterHandlers<WarehouseItem, WarehouseItemViewModel>();
            builder.RegisterHandlers<Product, ProductViewModel>();
            builder.RegisterHandlers<Batch, BatchViewModel>();
            builder.RegisterHandlers<Category, CategoryViewModel>();
            builder.RegisterHandlers<Customer, CustomerViewModel>();

            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor));
            builder.RegisterGeneric(typeof(HttpUnitOfWork<>))
                   .As(typeof(IUnitOfWork<>))
                   .InstancePerDependency();

            builder.RegisterType<ApplicationDbContext>();
            builder.RegisterType<LoggerFactory>()
               .As<ILoggerFactory>()
               .SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();

            builder.Register<ServiceFactory>(ctx =>

            {
                var c = ctx.Resolve<IComponentContext>();

                return t => c.Resolve(t);
            });

            var container = builder.Build();

            return container.ResolveOptional<IMediator>();
        }
    }
}