using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Rich.Order.Domain.ServiceCollectionExtensions
{
    public static class CustomServiceCollectionExtensions
    {
        public static IServiceCollection AddRichSwagger(this IServiceCollection services,
            IConfiguration configuration, [NotNull] Action<RichSwaggerOption> swaggeroption)
        {
            RichSwaggerOption option=new RichSwaggerOption();
            swaggeroption?.Invoke(option);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(option.versionName, new Info { Title = option.swaggerTitle, Version = option.versionName });
                var xmlPath = option.pathName;
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
