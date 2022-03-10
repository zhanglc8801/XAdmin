using Autofac;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using XAdmin.IService;
using XAdmin.Service;

namespace Web.Admin
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
            services.AddControllersWithViews();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));//解决视图输出内容中文编码问题
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //添加cookie认证服务
            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Path = "/";
            cookieBuilder.Name = "XADMIN_USERDATA";
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Cookie = cookieBuilder;
                    options.LoginPath = "/User/Login";
                    options.ClaimsIssuer = "Cookie";
                });

            //install-package Microsoft.AspNetCore.Mvc.NewtonsoftJson
            services.AddControllersWithViews().AddNewtonsoftJson(options => {
                if (options.SerializerSettings.ContractResolver is DefaultContractResolver resolver)
                {
                    resolver.NamingStrategy = null;
                }
            });
            services.AddMvc().AddRazorRuntimeCompilation();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<CommonService>().As<ICommonService>().InstancePerLifetimeScope();
            builder.RegisterType<SiteSetService>().As<ISiteSetService>().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoService>().As<IUserInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleInfoService>().As<IRoleInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<GroupInfoService>().As<IGroupInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<SystemManagementService>().As<ISystemManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<SiteInfoManagementService>().As<ISiteInfoManagementService>().InstancePerLifetimeScope();
            builder.RegisterType<ResourcesService>().As<IResourcesService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowService>().As<IWorkFlowService>().InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            //使用认证服务
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            //启用静态文件服务 用于访问wwwroot外的静态文件
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true, //不限制文件类型
                //ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                //{
                //    { ".mp4","video/mp4"},
                //    { ".rar","application/octet-stream"}
                //}),
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/Files"
            });

        }
    }
}
