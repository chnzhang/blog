/*
* ==============================================================================
*
* File name: MyRequiredAttribute
* Description: 自定义为空验证
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using System.ComponentModel.DataAnnotations;

namespace SkyBlog.DataValidate
{


    public class MyRequiredAttribute : RequiredAttribute
    {    
        
        /// <summary>
        /// 错误消息key
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 验证规则
        /// </summary>
        public object [] Groups { get; set; }  

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (validationContext.Items.Count > 0)
            {
                var Model =validationContext.Items["Model"];

                if (IsValidate(Groups, Model))
                {
                    if (value == null || value.ToString().Trim()=="")
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
