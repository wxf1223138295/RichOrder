using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Rich.Order.Domain.ApplicationBuilderExtensions
{
    public static class CustomApplicationExtension
    {
        public static IApplicationBuilder UseRichSwagger(this IApplicationBuilder app, IConfiguration config, [NotNull]Action<RichAppSwaggerOption> richoption)
        {
            RichAppSwaggerOption option = new RichAppSwaggerOption();
            richoption?.Invoke(option);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(option.urlName, option.swaggerUIName);
                c.RoutePrefix = "";
            });
            return app;
        }
    }
}
