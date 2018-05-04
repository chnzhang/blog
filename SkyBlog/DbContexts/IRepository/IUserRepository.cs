using SkyBlog.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.DbContexts.IRepository
{
    public interface IUserRepository
    {
        /// <summary>
        /// 根据主页地址获取用户编号
        /// </summary>
        /// <param name="index">主页地址</param>
        /// <returns></returns>
        user GetUserByIndexAsync(string index);
    }
}
