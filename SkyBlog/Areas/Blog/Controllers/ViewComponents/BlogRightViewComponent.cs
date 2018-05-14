using Microsoft.AspNetCore.Mvc;
using SkyBlog.DbContexts.IRepository;
using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.Areas.Blog.Controllers.ViewComponents
{
    public class BlogRightViewComponent : ViewComponent
    {
        public INewsRepository newsRepository;
        public IUserRepository userRepository;
        public BlogRightViewComponent(INewsRepository newsRepository, IUserRepository userRepository)
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
                    //查询点击排行文章列表
                    ViewBag.TopNewsList = newsRepository.GetTopNewsList(entity.id, 10);

                    //查询栏目推荐文章列表 
                    ViewBag.RecommendNewsList = newsRepository.GetRecommendNewsList(entity.id, 10);
                }

                return View();
            });
        }
    }
}
