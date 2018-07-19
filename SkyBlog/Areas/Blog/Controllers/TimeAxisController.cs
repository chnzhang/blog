using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.Common;
using SkyBlog.DbContexts.IRepository;

namespace SkyBlog.Areas.Blog.Controllers
{
    public class TimeAxisController:BaseController
    {
        private INewsRepository newsRepository;
        public TimeAxisController(INewsRepository newsRepository)
        {
            this.newsRepository=newsRepository;
        }
        
        public IActionResult Index(int page=1)
        {
            Pager pager=newsRepository.GetNewsListPage(24,page,userId,"");

            return View(userBlogTemplate+"TimeAxis/Index.cshtml",pager);
        }
    }
}