using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.DbContext.IRepository
{
    public interface INewsRepository
    {
        news Get(int id);

        /// <summary>
        /// 查询最新新闻列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<news> GetNewList(int? userId,int number);
    }
}
