using SkyBlog.DbContext.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkyBlog.Models.DbModel;
using SqlSugar;

namespace SkyBlog.DbContext.Repository
{
    public class NewsRepository : INewsRepository
    {
        /// <summary>
        /// 根据编号获取一条文章信息
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public news Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询新闻列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">查询条数</param>
        /// <returns></returns>
        public IList<news> GetNewList(int? userId, int number)
        {
            return DbFactory.Db.Queryable<news>()
                  .WhereIF(userId != null, w => w.user_id == userId)
                  .OrderBy(w => w.create_date_time, OrderByType.Desc)
                  .Take(number)
                  .ToList();
        }
    }
}
