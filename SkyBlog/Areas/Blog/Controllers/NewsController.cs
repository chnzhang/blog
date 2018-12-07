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

        /// <summary>
        /// 查询文章列表
        /// </summary>
        /// <param name="type">文章类别</param>
        /// <returns></returns>
        [HttpGet("list/{type}/{page}")]
        public IActionResult Index(string type = "", int page = 1)
        {
            if (!string.IsNullOrEmpty(type) && type == "all")
            {
                type = "";
            }
            return View(userBlogTemplate + "News/Index.cshtml", newsRepository.GetNewsListPage(20, page, userId, type));
        }

        /// <summary>
        /// 查询文章详情
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            NewsDetailDTO entity = newsRepository.GetNewsDetail(userId, id);

            //查询文章标签
            ViewBag.NewsTagList = newsRepository.GetNewsTagList(id);

            ViewBag.PerNextNewsList = newsRepository.GetPerNextNews(userId, id);
            ViewBag.RelevantArticlesNewsList = newsRepository.GetRelevantArticlesList(userId, id, 10);

            //更新点击率
            newsRepository.UpdateClickNumber(id);

            return View(userBlogTemplate + "News/Details.cshtml", entity);
        }
    }
}