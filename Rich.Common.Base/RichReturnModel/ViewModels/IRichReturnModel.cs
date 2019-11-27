using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Common.Base.RichReturnModel
{
    public interface IRichReturnModel
    {
        /// <summary>
        /// 标准序列化JSON方法
        /// </summary>
        /// <returns></returns>
        string ToJson();
    }
}
