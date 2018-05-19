using System;
using System.Collections.Generic;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SkyBlog.DbContexts.Repository
{
    public class BlogTemplateRepository : IBlogTemplateRepository
    {
        public IList<blog_template> GetBlogTemplateList()
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                return db.blog_template.ToList();
            }
        }
    }
}