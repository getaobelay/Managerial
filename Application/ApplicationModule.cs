using Application.Extensions.Containers;
using Autofac;
using Infrastructure;

namespace Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApplication();
            builder.RegisterInfrastructre();
        }
    }
}