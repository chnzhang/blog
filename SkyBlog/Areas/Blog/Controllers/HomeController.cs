using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;

namespace SkyBlog.Areas.Blog.Controllers
{    
    public class HomeController : BaseController
    {
        #region Properties
        private INewsRepository newsRepository;
        #endregion
        public HomeController(INewsRepository newsRepository)
        {
            this.newsRepository=newsRepository;
        }

        public IActionResult Index()
        {         
            return View(newsRepository.GetLasNewsList(userId,10));
        }
    
    }
}