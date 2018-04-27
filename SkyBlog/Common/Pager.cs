using Newtonsoft.Json;
namespace SkyBlog.Common
{
    /// <summary>
    /// 分页类
    /// </summary> 
    [JsonObject(MemberSerialization.OptOut)]
    public class Pager
    {
        /// <summary>
        /// 当前页码
        /// </summary>        
        [JsonIgnore]
        public int page { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        [JsonIgnore]
        public int pageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public dynamic rows { get; set; }


    }
}
