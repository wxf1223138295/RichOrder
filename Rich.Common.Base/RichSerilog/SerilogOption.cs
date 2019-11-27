using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Rich.Common.Base.RichSerilog
{
    public class SerilogOption
    {
        /// <summary>
        /// 日志文件名
        /// </summary>
        public string pathName { get; set; }
        /// <summary>
        /// 日志模板
        /// </summary>
        public string strTempName { get; set; }
        /// <summary>
        /// 保存数据库连接字符串
        /// </summary>
        public string logConnectstr{get;set;}
        /// <summary>
        /// 日志表名
        /// </summary>
        public string logTableName{get;set;}
        public LogEventLevel consoleminEvent{get;set;}
        public LogEventLevel debugminEvent { get; set; }

        public LogEventLevel fileminEvent { get; set; }
        public LogEventLevel mssminEvent { get; set; }
        public ColumnOptions columnOptions { get; set; }
        public string msgTemp { get; set; }
        /// <summary>
        /// 是否写数据库
        /// </summary>
        public bool NeedToMSS { get; set; } = false;
        /// <summary>
        /// 是否写Console
        /// </summary>
        public bool NeedToConsole { get; set; } = true;
        /// <summary>
        /// 是否写Debug
        /// </summary>
        public bool NeedToDebug { get; set; } = true;

    }
}
