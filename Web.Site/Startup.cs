using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using XAdmin.IService;
using XAdmin.Service;

namespace Web.Site
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
            #region Autofac依赖注入服务
            AutofacUtil.Container = app.ApplicationServices.GetAutofacRoot();
            #endregion

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
