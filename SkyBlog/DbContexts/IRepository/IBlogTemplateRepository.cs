using System;
using System.Collections.Generic;
using SkyBlog.Models.DbModel;

namespace SkyBlog.DbContexts.IRepository
{
    public interface IBlogTemplateRepository
    {
        IList<blog_template> GetBlogTemplateList();
    }
}