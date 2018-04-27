/*
* ==============================================================================
*
* File name: MyStringLengthAttribute
* Description: 自定义字符串长度验证
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace SkyBlog.DataValidate
{

    public class MyStringLengthAttribute : StringLengthAttribute
    {
        /// <summary>
        /// 错误消息key
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 验证规则
        /// </summary>
        public object[] Groups { get; set; }


        public MyStringLengthAttribute(int maximumLength) : base(maximumLength)
        {

        }
        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
          
            if (validationContext.Items.Count > 0)
            {
                var Model = validationContext.Items["Model"];

                if (IsValidate(Groups, Model))
                {
                    if (value != null&& !string.IsNullOrEmpty(value.ToString()) && (Convert.ToString(value).Trim().Length < MinimumLength || Convert.ToString(value).Trim().Length > MaximumLength))
                    {
                        string tips = Common.XMLConfiguartionService.GetXmlConfig(Message);
                        return new ValidationResult(tips);
                    }
                }
            }
            return ValidationResult.Success;
        }

        public bool IsValidate(object[] groups, object model)
        {

            foreach (var item in groups)
            {
                if (item == model)
                {
                    return true;
                }
            }
            return false;
        }

    }

}
