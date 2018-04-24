using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.Models.DbModel
{
    public class BaseEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        [Key]
        public int id { get; set; }
    }
}
