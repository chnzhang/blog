//-----------------------------------------------------------------------
// <copyright file=" tag.cs" company="四川易迅通健康医疗有限公司 Enterprises">
// * Copyright (C) 2018 四川易迅通健康医疗有限公司 Enterprises All Rights Reserved
// * version : 4.0.30319.42000
// * author  : auto generated by T4
// * FileName: tag.cs
// * history : Created by T4 04/25/2018 19:31:41 
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SkyBlog.Models.DbModel
{
    /// <summary>
    /// tag Entity Model
    /// </summary>    
    public class tag: BaseEntity
    {
    
	    
     
				/// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
		    
     
				/// <summary>
        /// 用户编号
        /// </summary>
        public int user_id { get; set; }
		    }
}
