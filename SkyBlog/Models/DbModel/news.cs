using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.Models.DbModel
{
    public class news : BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int user_id { get; set; }
        public int category_id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public int number { get; set; }
        public DateTime update_date_time { get; set; }
        public DateTime create_date_time { get; set; }
    }
}
