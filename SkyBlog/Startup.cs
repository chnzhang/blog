using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using SkyBlog.Config;
using SkyBlog.Filter;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //设置数据库连接字符串
            DbContexts.EFDbFactory.ConnectionString = Configuration.GetSection("connection")["mysql"];
            //设置数据库连接字符串
            //services.AddDbContextPool<EFDbFactory>(options => options.UseMySQL(Configuration.GetSection("connection")["mysql"]));

            //注册注入配置  
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
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

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

        }
    }
}
