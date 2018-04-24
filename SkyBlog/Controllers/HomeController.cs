using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContext.IRepository;

namespace SkyBlog.Controllers
{

    public class HomeController : Controller
    {
        INewsRepository newsRepository;
        public HomeController(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }


        public IActionResult Index()
        {
            ViewBag.NewsList = newsRepository.GetNewList(null, 10);
            return View();
        }    
    }
}
