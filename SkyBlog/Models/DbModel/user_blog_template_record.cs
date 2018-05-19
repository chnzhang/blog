using System;
namespace SkyBlog.Models.DbModel
{
    public class user_blog_template_record:BaseEntity
    {
        public int user_id{get;set;}
        public int blog_template_id{get;set;}
        public DateTime create_date_time{get;set;}
    }
}