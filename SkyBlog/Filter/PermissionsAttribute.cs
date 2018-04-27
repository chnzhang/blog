/*
* ==============================================================================
*
* File name: PermissionsAttribute
* Description: 全局接口功能权限校验
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
using System.Linq;
using static SkyBlog.Config.Enums;

namespace SkyBlog.Filter
{
    public class PermissionsAttribute : ActionFilterAttribute
    {
        private string[] authcode;
        private Level level;
              

        public PermissionsAttribute(string value = "", Level _level = Level.ROLE)
        {
            if (!string.IsNullOrEmpty(value))
                this.authcode = value.Split(";");
            this.level = _level;
        }


        //接口功能权限校验
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //跳过权限验证
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                //验证权限                
            }

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
