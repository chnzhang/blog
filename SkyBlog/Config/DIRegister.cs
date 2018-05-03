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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            // services.AddScoped<INewsRepository, NewsRepository>();

            //  services.AddScoped<IUserRepository, UserRepository>();

            //批量注入
            foreach (var item in GetClassName("SkyBlog","Repository"))
            {
                foreach (var typeArray in item.Value)
                {
                    services.AddScoped(typeArray, item.Key);
                }
            }

        }

        /// <summary>
        /// 根据程序集名称获取以X结尾的对象
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static Dictionary<Type, Type[]> GetClassName(string assemblyName,string EndsName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = null;
                if (string.IsNullOrEmpty(EndsName))
                {
                    ts = assembly.GetTypes().ToList();
                }
                else
                {
                   ts= assembly.GetTypes().Where(w => w.Name.EndsWith(EndsName)).ToList();
                }
              

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    if (item.IsGenericType) continue;
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }

    }
}
