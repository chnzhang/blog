/*
* ==============================================================================
*
* File name: BaseModelType
* Description: 模型验证规则集合
*
* Version: 1.0
* Created: 2018年3月7日
*
* Author: GuangZhi.Zhang
*
* ==============================================================================
*/

using System;

namespace SkyBlog.DataValidate
{
    /// <summary>
    /// 验证规则
    /// </summary>
    public class BaseModelType:Attribute
    {
        public class Insert: BaseModelType { };
        public class Update : BaseModelType { };
        public class SelectOne : BaseModelType { };
        public class Delete : BaseModelType { };
        public class Validate : BaseModelType { };
    }

    /// <summary>
    /// 基础验证父类
    /// </summary>
    public class BaseValidate
    {

    }
}
