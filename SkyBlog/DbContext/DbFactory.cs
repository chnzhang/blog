using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.DbContext
{
    public static class DbFactory
    {
        public static SqlSugarClient Db
        {
            get
            {

     
                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = Startup.Configuration.GetSection("connection")["mysql"], //必填
                    DbType = DbType.MySql, //必填
                    IsAutoCloseConnection = true, //默认false
                     InitKeyType = InitKeyType.Attribute
                }); //默认SystemTable

                return db;
            }
        }
}
}

