using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;
using SkyBlog.Models.DTO;

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
            NewsDetailDTO entity = newsRepository.GetNewsDetail(userId,id);           

            //查询文章标签
            ViewBag.NewsTagList=newsRepository.GetNewsTagList(id);

            ViewBag.PerNextNewsList=newsRepository.GetPerNextNews(userId,id);
            ViewBag.RelevantArticlesNewsList=newsRepository.GetRelevantArticlesList(userId,id,10);

            //更新点击率
            newsRepository.UpdateClickNumber(id);

            return View(userBlogTemplate+"News/Details.cshtml",entity);
        }
    }
}