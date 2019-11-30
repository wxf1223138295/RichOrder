using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rich.Common.Base.Configuration;
using Rich.Common.Base.CoreStart;
using Rich.Common.Base.RichAutoMapper;
using Rich.Common.Base.RichDapper;
using Rich.Common.Base.RichSerilog;
using Rich.Order.Application.CommonService;
using Rich.Order.Application.MapProfile;
using Rich.Order.Domain.AuthHandler;
using Rich.Order.Domain.User;
using Rich.Order.Infrastructure.EntityFrameworkCore;
using Rich.Order.Web.Host.Filter.ExceptionFilter;
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
            services.AddMvc(options =>
            {
                options.Filters.Add<RichExctption>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

            services.AddConfigurationIdentity(_appConfiguration);
            services.AddRichAboutHttp();
            services.AddRichAutoMapper((service, map) =>
            {
                map.AddProfile(typeof(AccountProfile));
            }, new List<Type>() { typeof(AccountProfile) });

            return services.AddShawnService(option =>
            {
                var iocManager = option._IocManager.BuilderContainer;
                iocManager.RegistAppServiceToContianer();

                var path = AppDomain.CurrentDomain.BaseDirectory;
                //注册 

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

            app.UseAuthentication(); // 启用身份验证


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
        public static IServiceCollection AddRichAboutHttp(this IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return service;
        }
        public static IServiceCollection AddConfigurationIdentity(this IServiceCollection service, IConfigurationRoot _appConfiguration)
        {


            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            service.Configure<JwtSettings>(_appConfiguration.GetSection("Jwt"));

            var jwtSettings = new JwtSettings();
            _appConfiguration.Bind("Jwt", jwtSettings);

            service.AddAuthorization(options =>
                {
                    options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
                })
                .AddAuthentication(s =>//添加JWT Scheme
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => 
                {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //验证Token颁发者
                    ValidateAudience = true, //谁可以使用Token
                    ValidateLifetime = true, //检查令牌是否过期，发行方(Issuer)的签名密钥是否有效
                    ValidateIssuerSigningKey = true, //验证令牌的签名
                    ValidIssuer = jwtSettings.Issuer, //Token颁发者
                    //ValidAudience = jwtSettings.Audience, //授权标识，这两项和jwt token的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)) //签名密钥
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            //注入授权Handler
            service.AddSingleton<IAuthorizationHandler, PolicyHandler>();

            service.AddDbContext<RichOrderDbContext>(options =>
                options.UseSqlServer(_appConfiguration["ConnectionStrings:Default"]));


            service.AddIdentity<RichOrderUser, RichOrderRole>()
                .AddRoles<RichOrderRole>()
                .AddEntityFrameworkStores<RichOrderDbContext>()
                .AddDefaultTokenProviders();

            service.Configure<IdentityOptions>(options =>
            {
                // Password settings
                //options.Password.RequireDigit = true;
                //options.Password.RequiredLength = 8;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireLowercase = false;
                //options.Password.RequiredUniqueChars = 6;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = false;

                // User settings
                options.User.RequireUniqueEmail = false;
            });
            service.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/api/Account/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            return service;
        }
    }
}
