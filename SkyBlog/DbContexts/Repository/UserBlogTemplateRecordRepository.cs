using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;

namespace SkyBlog.DbContexts.Repository
{
    public class UserBlogTemplateRecordRepository : IUserBlogTemplateRecordRepository
    {
        public blog_template GetUserBlogTemplate(int userId)
        {
           using(EFDbFactory db=new EFDbFactory())
           {
               var query=from ubtr in db.user_blog_template_record
                        join bt in db.blog_template on ubtr.blog_template_id equals bt.id
                        where ubtr.user_id==userId
                        select bt;
               return query.FirstOrDefault();                        
           }
        }
    }
}