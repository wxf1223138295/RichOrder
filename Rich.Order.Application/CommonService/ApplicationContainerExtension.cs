﻿using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Rich.Order.Application.RichOrderApplication;
using Rich.Order.Application.UserAppService;
using Rich.Order.Domain.Permissions;

namespace Rich.Order.Application.CommonService
{
    public static class ApplicationContainerExtension
    {
        public static void RegistAppServiceToContianer(this ContainerBuilder containerBuilder, IHostingEnvironment env)
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
            containerBuilder.RegisterType<RichUserAppService>()
                .As<IRichUserAppService>()
                .InstancePerDependency();
        }
    }
}
