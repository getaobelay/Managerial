using Autofac;
using AutoMapper.Configuration;
using Infrastructure.Context;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class RegisterInfrastructreContainer
    {
        public static ContainerBuilder RegisterInfrastructre(this ContainerBuilder builder)
        {
            builder.RegisterType(typeof(DataContext)).As(typeof(IDataContext));

            builder.RegisterType<CurrentUser>()
                    .As<ICurrentUser>()
                    .SingleInstance();

            builder.RegisterGeneric(typeof(UnitOfWorkRepository<>))
                   .As(typeof(IUnitOfWorkRepository<>));

            builder.RegisterType<ManagerialDbContext>();

            builder.RegisterType<HttpContextAccessor>()
                   .As<IHttpContextAccessor>()
                   .SingleInstance();

            builder.RegisterType<LoggerFactory>()
               .As<ILoggerFactory>()
               .SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>))
                .SingleInstance();

            return builder;
        }
    }
}