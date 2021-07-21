using Autofac;
using AutoMapper.Configuration;
using DAL.Core.Helpers.InventoryViewModels;
using DAL.Core.Helpers.ProductDtos;
using DAL.Core.ViewModels;
using DAL.Models;
using DAL.ViewModels;
using Managerial.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            builder.RegisterHandlers<Batch, BatchDto>();
            builder.RegisterHandlers<ProductCategory, ProductCategoryDto>();
            builder.RegisterHandlers<Stock, StockViewModel>();
            builder.RegisterHandlers<Inventory, InventoryViewModel>();
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
