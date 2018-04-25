//-----------------------------------------------------------------------
// <copyright file=" news_tag.cs" company="四川易迅通健康医疗有限公司 Enterprises">
// * Copyright (C) 2018 四川易迅通健康医疗有限公司 Enterprises All Rights Reserved
// * version : 4.0.30319.42000
// * author  : auto generated by T4
// * FileName: news_tag.cs
// * history : Created by T4 04/25/2018 19:31:41 
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SkyBlog.Models.DbModel
{
    /// <summary>
    /// news_tag Entity Model
    /// </summary>    
    public class news_tag: BaseEntity
    {
    
	    
     
				/// <summary>
        /// 文章编号
        /// </summary>
        public int news_id { get; set; }
		    
     
				/// <summary>
        /// 标签编号
        /// </summary>
        public int tag_id { get; set; }
		    }
}
