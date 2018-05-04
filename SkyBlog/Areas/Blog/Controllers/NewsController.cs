using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;

namespace SkyBlog.Areas.Blog.Controllers
{
  [Route("blog/{IndexUrl}/news")]
    public class NewsController : BaseController
    {
        private INewsRepository newsRepository;
        private IUserRepository userRepository;

        public NewsController(INewsRepository newsRepository, IUserRepository userRepository)
        {
            this.newsRepository = newsRepository;
            this.userRepository = userRepository;
        }

        [HttpGet()]
        public IActionResult Index()
        {         
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            return View(newsRepository.Get(id, userId));
        }
    }
}