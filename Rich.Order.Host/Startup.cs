using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rich.Common.Base.Configuration;
using Rich.Order.Domain.ApplicationBuilderExtensions;
using Rich.Order.Domain.ServiceCollectionExtensions;
using System.IO;

namespace Rich.Order.Host
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = _env.GetAppConfiguration();
        }

        private readonly IConfigurationRoot _appConfiguration;
        private readonly IHostingEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddRichSwagger(_appConfiguration, p =>
            {
                p.versionName = "v1";
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = System.AppDomain.CurrentDomain.BaseDirectory;//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Rich.Order.Host.xml");
                p.pathName = xmlPath;
                p.swaggerTitle = "RichPay API V1";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRichSwagger(_appConfiguration, p =>
            {
                p.swaggerUIName = "RichPay API V1";
                p.urlName= "/swagger/v1/swagger.json";
                
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }

    }
}
