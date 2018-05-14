using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;

namespace SkyBlog.Controllers
{

    public class HomeController : Controller
    {
       private INewsRepository newsRepository { get;}
        //public HomeController(INewsRepository newsRepository)
        //{
        //   // this.newsRepository = newsRepository;
        //}


        public IActionResult Index()
        {
            ViewBag.NewsList = newsRepository.GetRecommendNewsList(null, 10);
            return View();
        }    

        
    }
}
