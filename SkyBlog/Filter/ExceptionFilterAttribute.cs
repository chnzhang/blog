/*
* ==============================================================================
*
* File name: ExceptionFilterAttribute
* Description: 接口全局异常处理过滤器
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using Microsoft.AspNetCore.Mvc.Filters;
using SkyBlog.Common;
using SkyBlog.Config;
using System;
using System.Threading.Tasks;
using static SkyBlog.Config.Enums;

namespace SkyBlog.Filter
{
    public class ExceptionFilterAttribute : IExceptionFilter
    {
        // private ILog log = LogManager.GetLogger(Startup.logRepository.Name, typeof(ExceptionFilterAttribute));
        ILogManager log;

        public ExceptionFilterAttribute(ILogManager logger)
        {
            this.log = logger;
        }

        //全局捕获异常处理
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AggregateException || context.Exception is OperationCanceledException || context.Exception is TaskCanceledException)
            {
                //服务器出现错误
                HResult hr = new HResult
                {
                    Code = OPCode.SYSTEM_ERROR
                };
                context.Result = new ApplicationErrorResult(hr);
                context.Exception.Data.Clear();
            }
            else
            {
                //Nlog打印日志
                log.Error(context.Exception.Message + " " + context.Exception.StackTrace);
                //服务器出现错误
                HResult hr = new HResult
                {
                    Code = OPCode.SYSTEM_ERROR
                };
                //返回错误json
                context.Result = new ApplicationErrorResult(hr);
                //context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
               // context.ExceptionHandled = true;
            }

        

        }


        public class ApplicationErrorResult : Microsoft.AspNetCore.Mvc.ObjectResult
        {
            public ApplicationErrorResult(object value) : base(value)
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            }
        }

    }
}
