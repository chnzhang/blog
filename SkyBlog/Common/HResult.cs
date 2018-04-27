using SkyBlog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkyBlog.Common
{
    public class HResult
    {
        private object code;
        private dynamic data = string.Empty;
        private string msg = string.Empty;


        /// <summary>
        /// 设置状态编码
        /// </summary>
        /// <param name="value"></param>
        public object Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }


        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="value"></param>
        public dynamic Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }

        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg
        {
            get
            {
                if (string.IsNullOrEmpty(msg))
                {
                    return Enums.GetOPCodeDescription(code);
                }
                return msg;
            }
            set
            {
                msg = value;
            }
        }
    }
}
