using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AutofacSerilogIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rich.Common.Base.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Rich.Common.Base.RichSerilog
{
    public static class ShawnBootOptionsSerilogExtensions
    {
        public static ShawnBootOptions UseSerilog(this ShawnBootOptions options,Action<SerilogOption> SerilogAction)
        {

            var logger = new ShawnSerilog(SerilogAction);

            options._IocManager.BuilderContainer.RegisterLogger(logger, true,true);

            return options;
        }
    }
}
