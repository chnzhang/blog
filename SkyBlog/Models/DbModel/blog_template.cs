using System;
namespace SkyBlog.Models.DbModel
{
    public class blog_template:BaseEntity
    {        
        public string ename{get;set;}
        public string cname{get;set;}
        public string desc{get;set;}
        public string image{get;set;}
        public string view_path{get;set;}
        public DateTime create_date_time{get;set;}
    }
}