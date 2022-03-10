using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using XAdmin.IRepository;
using XAdmin.Repository;

namespace Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));//解决视图输出内容中文编码问题
            //这里为jwt登录验证的的key,在配置文件中
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"] + DateTime.Now.ToString("yyyyMMdd")));
            services.AddSingleton(securityKey);
            //添加cookie认证服务
            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Path = "/";
            cookieBuilder.Name = "XADMIN_USERDATA";
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie = cookieBuilder;
                    options.LoginPath = "/Login/Index";
                    options.ClaimsIssuer = "Cookie";
                })
                //配置jwt认证
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,
                        ValidAudience = "localhost",
                        ValidIssuer = "localhost",
                        IssuerSigningKey = securityKey
                    };
                });

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "XAdmin Web.Api", Version = "v1", Description = "by zhanglc" });
                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.*",
                    SearchOption.AllDirectories).Where(f => Path.GetExtension(f).ToLower() == ".xml"))
                {
                    option.IncludeXmlComments(name, includeControllerXmlComments: true);
                }
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                option.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CommonRepository>().As<ICommonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SiteSetRepository>().As<ISiteSetRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleInfoRepository>().As<IRoleInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<GroupInfoRepository>().As<IGroupInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SystemManagementRepository>().As<ISystemManagementRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SiteInfoManagementRepository>().As<ISiteInfoManagementRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ResourcesRepository>().As<IResourcesRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowRepository>().As<IWorkFlowRepository>().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web.Api v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.DocExpansion(DocExpansion.None);
                c.OAuthClientId("OpenAuth.WebApi");  //oauth客户端名称
                c.OAuthAppName("开源版webapi认证"); // 描述
            });

        }
    }
}
