/*
* ==============================================================================
*
* File name: AuthorizationAttribute
* Description: 接口全局合法验证过滤器
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SkyBlog.Config;
using System.Linq;
using static SkyBlog.Config.Enums;

namespace SkyBlog.Filter
{
    public class AuthorizationAttribute :IAuthorizationFilter
    {
  
        public void OnAuthorization(AuthorizationFilterContext context)
        {


            //if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            //{
            //    //跳过令牌及登录认证
            //}
            //else
            //{
            //    HResult hr = new HResult();
            //    //登录及令牌认证
            //    string token = context.HttpContext.Request.Headers["token"];
            //    if (string.IsNullOrEmpty(token))
            //    {
            //        hr.Code = OPCode.UNSIGNATURE;
            //        context.Result = new ApplicationErrorResult(hr);
            //    }
            //}

        }
        

        public class ApplicationErrorResult : Microsoft.AspNetCore.Mvc.ObjectResult
        {
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = (int)System.Net.HttpStatusCode.OK;
            }
        }
    }
}
