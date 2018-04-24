using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Common
{
    public class HResult
    {
        public int Code { get; set; }
        public object Data { get; set; }
        public string Msg { get; set; }
    }
}
