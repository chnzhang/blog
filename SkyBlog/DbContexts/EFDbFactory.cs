﻿using Microsoft.EntityFrameworkCore;
using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.DbContexts
{
    public class EFDbFactory:DbContext
    {
        public static string ConnectionString { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
        }


        /// <summary>
        /// 文章信息操作表
        /// </summary>
        public DbSet<news> news { get; set; }        
        public DbSet<user> user { get; set; }
        public DbSet<news_tag> news_tag{get;set;}
        public DbSet<tag> tag{get;set;}
        public DbSet<blog_template> blog_template{get;set;}
        public DbSet<user_blog_template_record> user_blog_template_record{get;set;}
    }
}
