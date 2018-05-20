using SkyBlog.Common;
using SkyBlog.Models.DbModel;
using SkyBlog.Models.DTO;
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

        NewsDetailDTO GetNewsDetail(int userId,int id);

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
        IList<NewsListDTO> GetLasNewsList(int? userId, int number);


        IList<news> GetSpecialRecommendNewsList(int userId,int number);


        /// <summary>
        /// 更新文章点击率
        /// </summary>
        /// <param name="id">文章编号</param>
        void UpdateClickNumber(int id);

        /// <summary>
        /// 上一篇，下一篇文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<news> GetPerNextNews(int? userId, int id);

        /// <summary>
        /// 相关文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="id">文章编号</param>
        /// <param name="number">获取条数</param>
        IList<news> GetRelevantArticlesList(int? userId, int id, int number);

        /// <summary>
        /// 查询文章标签
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        IList<tag> GetNewsTagList(int id);

        /// <summary>
        /// 根据条件查询文章列表-分页查询
        /// </summary>    
        /// <param name="pageSize">每页条数</param>
        /// <param name="page">当前页码</param>   
        /// <returns>分页对象</returns>
        Pager GetNewsListPage(int pageSize, int page,int userId);
    }
}
