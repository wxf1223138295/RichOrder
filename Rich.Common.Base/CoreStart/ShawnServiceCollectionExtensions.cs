using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Rich.Common.Base.Options;

namespace Rich.Common.Base.CoreStart
{
    public static class ShawnServiceCollectionExtensions
    {
        public static IServiceProvider AddShawnService(this IServiceCollection services, Action<ShawnBootOptions> optionsAction)
        {
            var shawnBootstrapper = ShawnBootstrapper.Create(optionsAction, services);

            services.AddSingleton(shawnBootstrapper);
    
            var obj=  new ShawnAutofacServiceProviderFactory(shawnBootstrapper
                .IocManager.BuilderContainer);
            
            services.AddSingleton(obj);
            

            var t=obj.CreateBuilder(services);
            var provide=obj.CreateServiceProvider(t);

            shawnBootstrapper.SetServiceProvide(provide);

            return provide;
        }

        //public static IServiceProvider BuildShawnServiceProvider(this IServiceCollection services)
        //{
        //    var result = services.BuildServiceProvider();
        //    return result;
        //}

    }
}
