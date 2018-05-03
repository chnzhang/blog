using Autofac;
using SkyBlog.Common;
using System;
using System.Linq;
using System.Runtime.Loader;

namespace SkyBlog.Config
{
    public class DefaultModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {           
            //注册当前程序集中以“Repository”结尾的类,暴漏类实现的所有接口，生命周期为PerLifetimeScope      
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //注册所有"SkyBlog"程序集中的类
            //builder.RegisterAssemblyTypes(GetAssembly("SkyBlog")).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();

            //手动注册
            builder.RegisterType<NLogManager>().As<ILogManager>().SingleInstance();
            builder.RegisterType<XMLConfiguartionService>().SingleInstance();
        }

        public static System.Reflection.Assembly GetAssembly(string assemblyName)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
            return assembly;
        }
    }
}
