using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Rich.Common.Base.Configuration;
using Rich.Common.Base.CoreStart;
using Rich.Common.Base.RichDapper;
using Rich.Common.Base.RichSerilog;
using Rich.Order.Application.CommonService;
using Rich.Order.Domain.User;
using Rich.Order.Infrastructure.EntityFrameworkCore;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Rich.Order.Web.Host
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Order Web Host Api",
                    Version = "v1",
                    Description = "Order for Rich",
                    TermsOfService = "Terms Of Order Apis"
                });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Rich.Order.Web.Host.xml");
                options.IncludeXmlComments(xmlPath);
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins(
                            // CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .ToArray()
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddConfigurationIdentity();



            return services.AddShawnService(option =>
            {
                var iocManager = option._IocManager.BuilderContainer;
                iocManager.RegistAppServiceToContianer();

                var path = AppDomain.CurrentDomain.BaseDirectory;
                //注册 

                services.AddAuthorization(p =>
                {
                    
                });

                var columnOptions = new ColumnOptions // 自定义字段
                {
                    AdditionalColumns = new Collection<SqlColumn>
                    {
                        new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "User"},
                        new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "TraceId"},
                        new SqlColumn {DataType = SqlDbType.VarChar, ColumnName = "Msg"},
                        new SqlColumn {DataType = SqlDbType.DateTime, ColumnName = "CreateTime"}
                    }
                };
                //注入日志
                option.UseSerilog(p =>
                {
                    //传空默认运行目录
                    p.pathName = string.Empty;
                    p.strTempName = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] {SourceContext} level: {Level:u4}, {Message:l}{NewLine}";
                    p.debugminEvent = LogEventLevel.Debug;
                    p.consoleminEvent = LogEventLevel.Verbose;
                    p.mssminEvent = LogEventLevel.Verbose;
                    p.columnOptions = columnOptions;
                    p.msgTemp = "User: {User} TraceId:{TraceId} Msg:{Msg} CreateTime:{CreateTime}";
                    p.logTableName = "RichLog";
                    //默认false
                    p.NeedToConsole = true;
                    p.NeedToDebug = true;
                    p.NeedToMSS = true;
                    p.logConnectstr = _appConfiguration["ConnectionStrings:Default"];
                });

                option.UseDapper(p => { p.DefaultConnectStrName = _appConfiguration["ConnectionStrings:Default"]; });
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
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Web API V1");
                });
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddConfigurationIdentity(this IServiceCollection service)
        {
            service.AddIdentity<RichOrderUser, IdentityRole>()
                .AddEntityFrameworkStores<RichOrderDbContext>()
                .AddDefaultTokenProviders();

            service.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            service.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            return service;
        }
    }
}
