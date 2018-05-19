using SkyBlog.DbContexts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkyBlog.Models.DbModel;
using SqlSugar;
using Microsoft.EntityFrameworkCore;
using DMF.Tools.DataTypeExtend;
using SkyBlog.Common;
using SkyBlog.Models.DTO;

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
                IQueryable<news> query = db.news;
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
                IQueryable<news> query = db.news;
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
        public IList<NewsListDTO> GetLasNewsList(int? userId, int number)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                var query = from n in db.news
                            join u in db.user on n.user_id equals u.id
                            join t in db.tag on n.category_id equals t.id into temp
                            from tt in temp.DefaultIfEmpty()
                            select new NewsListDTO
                            {
                                id = n.id,
                                title = n.title,
                                description = n.description,
                                image = n.image,
                                userName = u.name,
                                categoryName = tt.name,
                                number = n.number,
                                goodNumber = n.good_number,
                                createDateTime = n.create_date_time
                            };
                List<NewsListDTO> list = query.Take(number).ToList();
                foreach (var item in list)
                {
                    var itemQuery = from nt in db.news_tag
                                    join t in db.tag on nt.tag_id equals t.id
                                    where nt.news_id == item.id
                                    select t;
                    item.tag = itemQuery.ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// 更新文章点击率
        /// </summary>
        /// <param name="id">文章编号</param>
        public void UpdateClickNumber(int id)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                news entity = db.news.Find(id);
                entity.number += 1;
                db.news.Update(entity);
                db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 上一篇，下一篇文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<news> GetPerNextNews(int? userId, int id)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                IQueryable<news> perQuery = db.news.Where(w => w.id < id);
                var nextQuery = db.news.Where(w => w.id > id);
                if (userId != null)
                {
                    perQuery.Where(w => w.user_id == userId);
                    nextQuery.Where(w => w.user_id == userId);
                }
                //上一篇
                news perEntity = perQuery.OrderByDescending(w => w.id).FirstOrDefault();

                //下一篇
                news nextEntity = nextQuery.FirstOrDefault();

                IList<news> list = new List<news>();
                list.Add(perEntity);
                list.Add(nextEntity);

                return list;
            }
        }

        /// <summary>
        /// 相关文章列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<news> GetRelevantArticlesList(int? userId, int id, int number)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                //查询该类别文章
                news newsEntity = db.news.Find(id);
                if (newsEntity != null)
                {
                    //查询该文章类别相关文章列表                   
                    IQueryable<news> quuery = db.news.Where(w => w.category_id == newsEntity.category_id);
                    if (userId != null)
                    {
                        quuery.Where(w => w.user_id == userId);
                    }
                    return quuery.OrderByDescending(w => w.id).Take(number).ToList();
                }
                else
                {
                    return new List<news>();
                }
            }
        }

        /// <summary>
        /// 查询文章标签
        /// </summary>
        /// <param name="id">文章编号</param>
        /// <returns></returns>
        public IList<tag> GetNewsTagList(int id)
        {
            using (EFDbFactory db = new EFDbFactory())
            {
                IQueryable<tag> query = from nt in db.news_tag
                                        join t in db.tag on nt.tag_id equals t.id
                                        where nt.news_id == id
                                        select t;
                return query.ToList();
            }
        }

        public Pager GetNewsListPage(int pageSize, int page)
        {
            throw new NotImplementedException();
        }
    }
}
