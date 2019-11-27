using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rich.Order.Application.CommonService
{
    public static class ApplicationContainerExtension
    {
        public static void RegistAppServiceToContianer(this ContainerBuilder containerBuilder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            containerBuilder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("AppService"))
                .AsImplementedInterfaces();
        }
    }
}
