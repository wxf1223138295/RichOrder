using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;

namespace Rich.Common.Base.RichSerilog
{
    public class SerilogLoger
    {
        public static ILogger CreateSerilog(string templateStr, string pathname,string logconnectstr,string tablename,LogEventLevel consoleminEvent, LogEventLevel debugminEvent, LogEventLevel fileminEvent, LogEventLevel mssminEvent, ColumnOptions columnoptions,string msgtemp,bool needToConsole,bool needToDebug,bool needToMSS)
        {

            var logConfiguration = new LoggerConfiguration()
                .Enrich.WithProperty("SourceContext", null)
                //必须写文件
                .WriteTo.File(pathname,
                    shared: true,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 6,
                    outputTemplate: templateStr,
                    restrictedToMinimumLevel: fileminEvent);

            if (needToConsole)
            {
                logConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: consoleminEvent);
            }

            if (needToDebug)
            {
                logConfiguration.WriteTo.Debug(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    restrictedToMinimumLevel: debugminEvent);
            }

            if (needToMSS)
            {
                logConfiguration.WriteTo.MSSqlServer(logconnectstr, tablename, columnOptions: columnoptions,
                    restrictedToMinimumLevel: mssminEvent, autoCreateSqlTable: true);
            }
            
             Log.Logger= logConfiguration.CreateLogger();

            return Log.Logger;
        }
     
    }
}
