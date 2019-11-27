using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.XPath;
using Serilog;
using Serilog.Core;

namespace Rich.Common.Base.RichSerilog
{
    public class SerilogFactory
    {
        public ILogger Create(SerilogOption optins)
        {
          

            if (string.IsNullOrEmpty(optins.pathName))
            {
               var directory = AppDomain.CurrentDomain.BaseDirectory;

               optins.pathName = Path.Combine($"{directory}", "Logs", $"log.txt");
            }

            return SerilogLoger.CreateSerilog(optins.strTempName, optins.pathName,optins.logConnectstr,optins.logTableName,optins.consoleminEvent, optins.debugminEvent, optins.fileminEvent,optins.mssminEvent,optins.columnOptions,optins.msgTemp,optins.NeedToConsole,optins.NeedToDebug,optins.NeedToMSS);
        }
    }
}
