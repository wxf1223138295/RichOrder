using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Rich.Order.Domain.Permissions;
using Rich.Order.Infrastructure.RichRepository;

namespace Rich.Order.Infrastructure.EntityConfiguration
{
    public static class InfrastrctureContainerExtension
    {
        public static void RegistInfrastrctureToContianer(this ContainerBuilder containerBuilder, IHostingEnvironment env)
        {
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            //var path1 = env.ContentRootPath;
            //path = Path.Combine(path, "Rich.Order.Application.dll");
            //var dataAccess = Assembly.LoadFile(path);

            //containerBuilder.RegisterAssemblyTypes(dataAccess)
            //    .Where(t => t.Name.EndsWith("AppService"))
            //    .AsImplementedInterfaces();

            //containerBuilder.RegisterAssemblyTypes(dataAccess)
            //    .Where(t => t.Name.EndsWith("Repository"))
            //    .AsImplementedInterfaces();
            containerBuilder.RegisterType<RichUserRepository>()
                .As<IRichUserRepository>()
                .InstancePerDependency();
        }
    }
}
