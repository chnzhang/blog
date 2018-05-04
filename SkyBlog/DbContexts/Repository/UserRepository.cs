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
        public  user GetUserByIndexAsync(string index)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                 return    db.user.Where(w => w.index == index).FirstOrDefault();
            }
        }
    }
}
