using SkyBlog.DbContexts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkyBlog.Models.DbModel;
using SqlSugar;
using Microsoft.EntityFrameworkCore;
using DMF.Tools.DataTypeExtend;

namespace SkyBlog.DbContexts.Repository
{
    public class NewsRepository : INewsRepository
    {
        /// <summary>
        /// 根据编号获取一条文章信息
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public news Get(int id, int? userId)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                if (userId != null)
                {
                    return db.news.Where(w => w.id == id && w.user_id == userId).First();
                }
                return db.news.Find(id);
            }
        }


        /// <summary>
        ///  查询推荐栏目文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">查询条数</param>
        /// <returns></returns>
        public IList<news> GetRecommendNewsList(int? userId, int number)
        {

            //List<SugarParameter> par = new List<SugarParameter>();
            //IDictionary<string, string> onWhere = new Dictionary<string, string>();
            //onWhere.Add("user", " AND u.name=@name");
            //par.Add(new SugarParameter("name", "123"));


            //var quey = DbFactory.Db.Queryable<news>("w")
            //    .AddJoinInfo("user", "u", "u.id=w.id" + (userId != null ? onWhere["user"] : string.Empty), JoinType.Left)
            //    .AddParameters(par)
            //    .WhereIF(userId != null, w => w.user_id == userId)
            //    .OrderBy(w => w.create_date_time, OrderByType.Desc)
            //    .Take(number);
            //var sql = quey.ToSql();
            //var list = quey.ToList();

            //System.Linq.Expressions.Expression<Func<news, user, bool>> exps = null;
            //if (userId == null)
            //{
            //    exps = (n, u) => n.user_id == u.id;
            //}
            //else
            //{
            //    exps = (n, u) => n.user_id == u.id && u.name == "zgz";
            //}


            //var exp = Expressionable.Create<news, user>()
            //    .And((n, u) => n.user_id == u.id)
            //    .AndIF(userId != null, (n, u) => u.name == "aaa")
            //    .ToExpression();

          
            //var queys = DbFactory.Db.Queryable<news, user>((n, u) => new object[] {
            //  JoinType.Left,
            //}).Where((n, u) => n.user_id == 1).ToSql();


            using (EFDbFactory db = new EFDbFactory())
            {
                var query = db.news;
                if (userId != null)
                {
                    query.Where(w => w.user_id == userId);
                }
                return query.OrderByDescending(w => w.create_date_time).Take(number).ToList();
            }
        }

        /// <summary>
        /// 查询点击排行文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">获取条数</param>
        public IList<news> GetTopNewsList(int? userId, int number)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                var query = db.news;
                if (userId != null)
                {
                    query.Where(w => w.user_id == userId);
                }
                return query.OrderByDescending(w => w.number).Take(number).ToList();
            }
        }

        /// <summary>
        /// 查询最新发布的文章列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="number">获取条数</param>
        public IList<news> GetLasNewsList(int? userId, int number)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                var query = db.news;//.Include(person => person.user);
                if (userId != null)
                {
                    query.Where(w => w.user_id == userId);
                }
                return query.OrderByDescending(w => w.id).Take(number).ToList();
            }
        }
    }
}
