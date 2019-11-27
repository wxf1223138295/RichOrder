using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Common.Base.RichReturnModel
{
    public class RichBizReturn<T>
    {
        public string message { get; set; }

        public T data { get; set; }
        public int resultCode { get; set; }
        public bool success { get; set; }
        public string exceptionMsg { get; set; }
    }
}
