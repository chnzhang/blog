﻿/*
* ==============================================================================
*
* File name: GlobalActionFilterAttribute
* Description: 全局接口方法参数规则验证过滤器
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SkyBlog.Common;
using SkyBlog.Config;
using SkyBlog.DataValidate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static SkyBlog.Config.Enums;

namespace SkyBlog.Filter
{
    public class GlobalActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 方法参数校验拦截器
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

            //判断Action是否有参数
            if (actionContext.ActionDescriptor.Parameters.Any())
            {

                //获取验证模型
                var myType = ((ControllerActionDescriptor)actionContext.ActionDescriptor).MethodInfo.CustomAttributes
                                        .Where(w => w.AttributeType.BaseType == typeof(BaseModelType)).FirstOrDefault();

                //判断是否有验证模型
                if (myType != null)
                {

                    //组装当前使用的验证模式
                    IDictionary<object, object> dic = new Dictionary<object, object>
                    {
                        { "Model", myType.AttributeType }
                    };

                    //组装验证类，并触发模型验证
                    if (!actionContext.ActionArguments.Any())
                    {
                        //非法请求
                        HResult hr = new HResult
                        {
                            Code = OPCode.PARAMETER_ERROR
                        };
                        actionContext.Result = new ApplicationErrorResult(hr);
                        return;
                    }
                    object objClass = actionContext.ActionArguments.FirstOrDefault().Value;
                    //触发验证
                    ValidationContext context = new ValidationContext(objClass, dic);
                    List<ValidationResult> results = new List<ValidationResult>();

                    bool isValid = Validator.TryValidateObject(objClass, context, results, true);

                    //根据返回结果 判断是否验证成功 未成功则返回错误信息 已成功则进入方法执行
                    if (isValid == false)
                    {
                        HResult hr = new HResult
                        {
                            Code = OPCode.PARAMETER_ERROR,
                            Msg = results.FirstOrDefault().ErrorMessage
                        };
                        actionContext.Result = new ApplicationErrorResult(hr);

                    }
                    else
                    {
                      // Type type = objClass.GetType();
                        foreach (var item in objClass.GetType().GetProperties())
                        {
                          
                            if (item.PropertyType.FullName.Contains("List"))
                            {
                           
                                IEnumerable<object> list = item.GetValue(objClass) as IEnumerable<object>;
                                if ((list == null || list.Count() == 0)&& item.CustomAttributes.Where(w => w.AttributeType.BaseType == typeof(BaseModelType)).FirstOrDefault() != null)
                                {
                                    //创建该类型
                                    string name = item.PropertyType.FullName;
                                    int start = name.IndexOf("[") + 2;
                                    int end = name.IndexOf(",");

                                    name = name.Substring(start, end - start);

                                    Type type = Type.GetType(name);
                                    object obj = type.Assembly.CreateInstance(type.ToString());
                                    var model = type.Assembly.CreateInstance(name);
                                    var attr = model.GetType().Attributes;

                                    //普通对象验证                             
                                    var validateResult = ValidateData(objClass, model, dic);
                                    if (validateResult.Item1 == false)
                                    {
                                        isValid = validateResult.Item1;
                                        results = validateResult.Item2;
                                    }

                                }
                                else if(list!=null&&list.Count()>0)
                                {
                                    foreach (var ch in list)
                                    {
                                        //普通对象验证                             
                                        var validateResult = ValidateData(objClass, ch, dic);
                                        if (validateResult.Item1 == false)
                                        {
                                            isValid = validateResult.Item1;
                                            results = validateResult.Item2;
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (item.PropertyType.BaseType == typeof(BaseValidate))
                            {
                                //验证子类普通对象                              
                                var validateResult = ValidateData(objClass, item, dic);
                                if (validateResult.Item1 == false)
                                {
                                    isValid = validateResult.Item1;
                                    results = validateResult.Item2;
                                }
                            }
                        }

                        if (isValid == false)
                        {
                            HResult hr = new HResult
                            {
                                Code = OPCode.PARAMETER_ERROR,
                                Msg = results.FirstOrDefault().ErrorMessage
                            };
                            actionContext.Result = new ApplicationErrorResult(hr);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, List<ValidationResult>> ValidateData(object objClass, System.Reflection.PropertyInfo info, IDictionary<object, object> dic)
        {
            bool isValid = false;
            List<ValidationResult> listvali = new List<ValidationResult>();

            if (info.CanRead && info.PropertyType.BaseType == typeof(BaseValidate))
            {
                var value = info.GetValue(objClass);
                if (value == null)
                {
                    Type type = info.PropertyType;
                    value = type.Assembly.CreateInstance(type.ToString());
                }
                ValidationContext context = new ValidationContext(objClass, dic);
                context = new ValidationContext(value, dic);

                isValid = Validator.TryValidateObject(value, context, listvali, true);
            }
            Tuple<bool, List<ValidationResult>> output = new Tuple<bool, List<ValidationResult>>(isValid, listvali);
            return output;
        }

        /// <summary>
        /// 验证模型
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, List<ValidationResult>> ValidateData(object objClass, object info, IDictionary<object, object> dic)
        {
            bool isValid = false;
            List<ValidationResult> listvali = new List<ValidationResult>();


            ValidationContext context = new ValidationContext(objClass, dic);
            context = new ValidationContext(info, dic);

            isValid = Validator.TryValidateObject(info, context, listvali, true);

            Tuple<bool, List<ValidationResult>> output = new Tuple<bool, List<ValidationResult>>(isValid, listvali);
            return output;
        }


        public class ApplicationErrorResult : Microsoft.AspNetCore.Mvc.ObjectResult
        {
            public ApplicationErrorResult(object value) : base(value)
            {
            }
        }
    }
}
