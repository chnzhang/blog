using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.DbContexts.IRepository
{
    public interface INewsRepository
    {
        /// <summary>
        /// 获取单条文章编号
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <param name="userId">用户编号</param>
        news Get(int id, int? userId);

        /// <summary>
        /// 查询点击排行文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">获取条数</param>
        IList<news> GetTopNewsList(int? userId, int number);

        /// <summary>
        /// 查询推荐栏目文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">获取条数</param>
        /// <returns></returns>
        IList<news> GetRecommendNewsList(int? userId, int number);

        /// <summary>
        /// 查询最新发布的文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">获取条数</param>
        IList<news> GetLasNewsList(int? userId,int number);
    }
}
