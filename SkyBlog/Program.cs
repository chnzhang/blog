using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SkyBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddEnvironmentVariables()
             .AddCommandLine(args)
             .AddJsonFile("hosting.json", optional: true)
             .Build();

         //   System.Threading.ThreadPool.SetMinThreads(500, 250);
            BuildWebHost(config, args).Run();
        }

        public static IWebHost BuildWebHost(IConfigurationRoot config, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseEnvironment(config["ASPNETCORE_ENVIRONMENT"])
                    .UseConfiguration(config)
                    .UseKestrel()
                    .UseStartup<Startup>()
                    .Build();
    }
}
