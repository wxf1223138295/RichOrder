using System;
using System.Collections.Generic;
using System.Text;

namespace Rich.Common.Base.RichReturnModel
{
    public static class Const
    {
        #region 日期格式

        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public const string DateTimeShortFormat = "yyyy-MM-dd HH:mm:ss";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateShortFormat = "yyyy-M-d";
        public const string DateShortestFormat = "yyyyMMdd";
        public const string LogTraceIdKey = "LogTraceId";

        #endregion

        public const int GeneralSuccessCode = 20000;
        public const int GeneralExceptionErrorCode = 50000;
    }
}
