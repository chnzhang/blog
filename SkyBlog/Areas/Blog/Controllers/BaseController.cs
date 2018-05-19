using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;
using SkyBlog.DbContexts.Repository;

namespace SkyBlog.Areas.Blog.Controllers
{
    [Area("blog")]
    public class BaseController : Controller
    {
        protected IUserRepository userRepository=new UserRepository();     
        protected IUserBlogTemplateRecordRepository userBlogTemplateRecordRepository=new UserBlogTemplateRecordRepository();
        public string IndexUrl { get; set; }
        public int userId { get; set; }
        public string userBlogTemplate{get;set;}
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (RouteData.Values["index"] != null)
            {
                ViewBag.Index = (string)RouteData.Values["index"];
                IndexUrl = (string)RouteData.Values["index"];
            }
            else
            {
                ViewBag.Index = "zgz";
                IndexUrl = "zgz";
            }
            //查询该主页对应的用户
            user entity = userRepository.GetUserByIndexAsync(IndexUrl);
            if (entity != null)
            {
                userId = entity.id;
                //查询用户博客模板
                blog_template btEntity= userBlogTemplateRecordRepository.GetUserBlogTemplate(userId);
                if(btEntity!=null)
                {
                    userBlogTemplate=btEntity.view_path;
                }
            }
        }


    }
}