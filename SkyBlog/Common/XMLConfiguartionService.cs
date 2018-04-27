using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace SkyBlog.Common
{
    public class XMLConfiguartionService
    {

        public XMLConfiguartionService()
        {

        }


        /// <summary>
        /// xml配置文件读取
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public string GetXml(string key, string configFileName = "Xml/message.xml", string basePath = "")
        {

            basePath = string.IsNullOrWhiteSpace(basePath) ? Directory.GetCurrentDirectory() : basePath;
            var builder = new ConfigurationBuilder().

             AddXmlFile(b =>
             {
                 b.Path = configFileName;
                 b.FileProvider = new PhysicalFileProvider(basePath);
             });
            var config = builder.Build();
            return config[key];
        }



        /// <summary>
        /// xml配置文件读取
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetXmlConfig(string key,
           string configFileName = "Xml/message.xml",
           string basePath = "")
        {


            //查询缓存当中是否已有配置文件
            //if (cache.Exists("OPCode"))
            //{
            //    var config = (IConfigurationRoot)cache.Get("OPCode");
            //    return config[key];
            //}
            //else
            //{
            basePath = string.IsNullOrWhiteSpace(basePath) ? Directory.GetCurrentDirectory() : basePath;
            var builder = new ConfigurationBuilder().
             //SetBasePath(basePath).
             AddXmlFile(b =>
             {
                 b.Path = configFileName;
                 b.FileProvider = new PhysicalFileProvider(basePath);
             });
            var config = builder.Build();
            return config[key];
        }
        // }
    }
}
