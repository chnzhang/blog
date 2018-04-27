/*
* ==============================================================================
*
* File name: MyDescriptionAttribute
* Description: 枚举对应中文值获取
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/
using SkyBlog.Common;
using System.ComponentModel;


namespace SkyBlog.Config
{
    public class MyDescriptionAttribute: DescriptionAttribute
    {
        
        public MyDescriptionAttribute(string key)
        {
            base.DescriptionValue = XMLConfiguartionService.GetXmlConfig(key);
        }

    }
}
