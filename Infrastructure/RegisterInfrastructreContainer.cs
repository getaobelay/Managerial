using Autofac;
using Infrastructure.Context;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public static class RegisterInfrastructreContainer
    {
        public static ContainerBuilder RegisterInfrastructre(this ContainerBuilder builder)
        {
            builder.RegisterType(typeof(DataContext)).As(typeof(IDataContext));

            builder.RegisterType(typeof(HttpContextAccessor)).As(typeof(IHttpContextAccessor));

            builder.RegisterType(typeof(CurrentUser)).As(typeof(ICurrentUser));
            builder.RegisterGeneric(typeof(UnitOfWorkRepository<>))
                   .As(typeof(IUnitOfWorkRepository<>));

            builder.RegisterType<ManagerialDbContext>();

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