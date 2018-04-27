/*
* ==============================================================================
*
* File name: DIRegister
* Description: 依赖注入配置类
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/
using Microsoft.Extensions.DependencyInjection;
using SkyBlog.Common;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.DbContexts.Repository;

namespace SkyBlog.Config
{
    public class DIRegister
    {

        //配置依赖注入
        public void DIRegister_Context(IServiceCollection services)
        {            

            //注入Nlog日志
            services.AddSingleton<ILogManager, NLogManager>();

            
            //注入xml配置获取类
            services.AddSingleton<XMLConfiguartionService>();


            /*********注入数据库操作类******/
            //模型创建DbFirst类注册      

            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

        }
    }
}
