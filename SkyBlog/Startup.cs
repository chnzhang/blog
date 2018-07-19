using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDataAnnotations;
using NLog.Web;
using SkyBlog.Config;
using SkyBlog.Filter;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace SkyBlog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }
        public static IContainer AutofacContainer;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //设置数据库连接字符串
            DbContexts.EFDbFactory.ConnectionString = Configuration.GetSection("connection")["mysql"];

            //原core原生依赖注入配置  
            DIRegister sdr = new DIRegister();
            sdr.DIRegister_Context(services);

            //解决中文被编码
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
       
            //配置时间格式
            services.AddMvc(opt =>
            {
                opt.RespectBrowserAcceptHeader = true;
                opt.Filters.Add<ExceptionFilterAttribute>();
                opt.Filters.Add<PermissionsAttribute>();
                opt.Filters.Add<GlobalActionFilterAttribute>();

            })
            .AddJsonOptions(options =>
            {
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region 使用autofac代替原生IOC
            //ContainerBuilder builder = new ContainerBuilder();
            ////将services中的服务填充到Autofac中.
            //builder.Populate(services);
            ////新模块组件注册
            //builder.RegisterModule<DefaultModuleRegister>();
            ////创建容器.
            //AutofacContainer = builder.Build();
            ////使用容器创建 AutofacServiceProvider 
            //return new AutofacServiceProvider(AutofacContainer);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "area",
                   template: "{area:exists}/{index}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                 name: "area-min",
                 template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{area=blog}/{controller=Home}/{action=Index}/{id?}"
                    );
            });

            //Nlog配置
            env.ConfigureNLog("nlog.config");//读取Nlog配置文件 

            // 程序停止调用函数
            //appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }
    }
}
