using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.Areas.Blog.Controllers.ViewComponents
{
    public class BlogRightMoreOneViewComponent : ViewComponent
    {
        public INewsRepository newsRepository;
        public IUserRepository userRepository;
        public BlogRightMoreOneViewComponent(INewsRepository newsRepository, IUserRepository userRepository)
        {
            this.newsRepository = newsRepository;
            this.userRepository = userRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(string index)
        {
            return await Task.Run(() =>
            {
                //根据Index查询用户编号
                var entity = userRepository.GetUserByIndexAsync(index);
                if (entity != null)
                {
                    //查询特别推荐文章列表
                    ViewBag.SpecialRecommendNewsList = newsRepository.GetSpecialRecommendNewsList(entity.id, 3);


                    //查询点击排行文章列表
                    IList<news> topNewsList = newsRepository.GetTopNewsList(entity.id, 5);
                    news topFirst = topNewsList.FirstOrDefault();
                    ViewBag.TopNewFirst = topFirst;
                    if (topFirst != null)
                        ViewBag.TopNewsList = topNewsList.Where(w => w.id != topFirst.id).ToList();

                    //查询栏目推荐文章列表 
                    IList<news> recommendNewsList = newsRepository.GetRecommendNewsList(entity.id, 5);
                    news recommendFirst = recommendNewsList.FirstOrDefault();
                    ViewBag.RecommendNewsFirst = recommendFirst;
                    if (recommendFirst != null)
                        ViewBag.RecommendNewsList = recommendNewsList.Where(w => w.id != recommendFirst.id).ToList();
                }

                return View();
            });
        }
    }
}
