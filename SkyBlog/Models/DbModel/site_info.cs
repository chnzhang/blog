//-----------------------------------------------------------------------
// <copyright file=" site_info.cs" company="四川易迅通健康医疗有限公司 Enterprises">
// * Copyright (C) 2018 四川易迅通健康医疗有限公司 Enterprises All Rights Reserved
// * version : 4.0.30319.42000
// * author  : auto generated by T4
// * FileName: site_info.cs
// * history : Created by T4 04/25/2018 19:31:41 
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace SkyBlog.Models.DbModel
{
    /// <summary>
    /// site_info Entity Model
    /// </summary>    
    public class site_info: BaseEntity
    {
    
	    
     
				/// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
		    
     
				/// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
		    
     
				/// <summary>
        /// 
        /// </summary>
        public string keyword { get; set; }
		    
     
				/// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
		    
     
				/// <summary>
        /// 是否开放注册功能
        /// </summary>
        public int is_openregist { get; set; }
		    
     
				/// <summary>
        /// 备案信息
        /// </summary>
        public string icp { get; set; }
		    
     
				/// <summary>
        /// 版权
        /// </summary>
        public string copyright { get; set; }
		    
     
				/// <summary>
        /// 网站logo
        /// </summary>
        public string logo { get; set; }
		    }
}
