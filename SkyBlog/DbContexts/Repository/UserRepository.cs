using SkyBlog.DbContexts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkyBlog.Models.DbModel;

namespace SkyBlog.DbContexts.Repository
{
    public class UserRepository : IUserRepository
    {
         /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        public user Get(int id)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                 return db.user.Find(id);
            }
        }

        public  user GetUserByIndexAsync(string index)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                 return db.user.Where(w => w.index == index).FirstOrDefault();
            }
        }

        
    }
}
