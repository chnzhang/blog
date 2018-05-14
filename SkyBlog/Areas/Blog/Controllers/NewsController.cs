using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;

namespace SkyBlog.Areas.Blog.Controllers
{
    [Route("blog/{IndexUrl}/news")]
    public class NewsController : BaseController
    {
        private INewsRepository newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            news entity = newsRepository.Get(id, userId);
            if (entity != null)
            {
                user user = userRepository.Get(entity.user_id);
                if (user != null)
                    ViewBag.Auther = user.name;
                else
                    ViewBag.Auther ="未知";
            }
            return View(entity);
        }
    }
}