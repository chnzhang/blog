using Microsoft.EntityFrameworkCore;
using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.DbContexts
{
    public class EFDbFactory:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Startup.Configuration.GetSection("connection")["mysql"]);
        }
        /// <summary>
        /// 文章信息操作表
        /// </summary>
        public DbSet<news> news { get; set; }
    }
}
