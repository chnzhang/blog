using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkyBlog.DbContexts;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.DbContexts.Repository;
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
            services.AddScoped<INewsRepository, NewsRepository>();
            //解决中文被编码
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            services.AddMvc();
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
        }
    }
}
