using System;
using SkyBlog.Models.DbModel;

namespace SkyBlog.DbContexts.IRepository
{
    public interface IUserBlogTemplateRecordRepository
    {
        blog_template GetUserBlogTemplate(int userId);
    }
}