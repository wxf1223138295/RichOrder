using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Rich.Common.Base.RichSerilog
{
    public class ShawnSerilog:ILogger
    {
        private ILogger logger;

        private string msgTemp;
        public ShawnSerilog(Action<SerilogOption> optins)
        {
            SerilogOption opt = new SerilogOption();
            optins?.Invoke(opt);
            msgTemp = opt.msgTemp;
            logger = new SerilogFactory().Create(opt);
        }

        public ILogger ForContext(ILogEventEnricher enricher)
        {
            return  logger.ForContext(enricher);
        }

        public ILogger ForContext(IEnumerable<ILogEventEnricher> enrichers)
        {
            return logger.ForContext(enrichers);
        }

        public ILogger ForContext(string propertyName, object value, bool destructureObjects = false)
        {
            return logger.ForContext(propertyName, value, destructureObjects);
        }

        public ILogger ForContext<TSource>()
        {
            return logger.ForContext<TSource>();
        }

        public ILogger ForContext(Type source)
        {
            return logger.ForContext(source);
        }

        public void Write(LogEvent logEvent)
        {
            logger.Write(logEvent);
        }

        public void Write(LogEventLevel level, string messageTemplate)
        {
            logger.Write(level, msgTemp);
        }

        public void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
        {
            logger.Write<T>(level, msgTemp, propertyValue);
        }

        public void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Write<T0, T1>(level, msgTemp, propertyValue0, propertyValue1);
        }

        public void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Write<T0, T1, T2>(level, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Write(LogEventLevel level, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(level, msgTemp, propertyValues);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate)
        {
            logger.Write(level, exception, msgTemp);
        }

        public void Write<T>(LogEventLevel level, Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Write<T>(level, exception, msgTemp, propertyValue);
        }

        public void Write<T0, T1>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            logger.Write<T0, T1>(level, exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Write<T0, T1, T2>(LogEventLevel level, Exception exception, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            logger.Write<T0, T1, T2>(level, exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Write(LogEventLevel level, Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Write(level, exception, msgTemp, propertyValues);
        }

        public bool IsEnabled(LogEventLevel level)
        {
            return logger.IsEnabled(level);
        }

        public void Verbose(string messageTemplate)
        {
            logger.Verbose( msgTemp);
        }

        public void Verbose<T>(string messageTemplate, T propertyValue)
        {
            logger.Verbose<T>(msgTemp, propertyValue);
        }

        public void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Verbose<T0, T1>(msgTemp, propertyValue0, propertyValue1);
        }

        public void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Verbose<T0, T1, T2>(msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Verbose(string messageTemplate, params object[] propertyValues)
        {
            logger.Verbose(msgTemp, propertyValues);
        }

        public void Verbose(Exception exception, string messageTemplate)
        {
            logger.Verbose(exception,msgTemp);
        }

        public void Verbose<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Verbose<T>(exception, msgTemp, propertyValue);
        }

        public void Verbose<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Verbose<T0, T1>(exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Verbose<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Verbose<T0, T1, T2>(exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Verbose(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Verbose(exception, msgTemp, propertyValues);
        }

        public void Debug(string messageTemplate)
        {
            logger.Debug(msgTemp);
        }

        public void Debug<T>(string messageTemplate, T propertyValue)
        {
            logger.Debug<T>(msgTemp, propertyValue);
        }

        public void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Debug<T0, T1>(msgTemp, propertyValue0, propertyValue1);
        }

        public void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Debug<T0, T1, T2>(msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Debug(string messageTemplate, params object[] propertyValues)
        {
            logger.Debug(msgTemp, propertyValues);
        }

        public void Debug(Exception exception, string messageTemplate)
        {
            logger.Debug(exception,msgTemp);
        }

        public void Debug<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Debug<T>(exception,msgTemp, propertyValue);
        }

        public void Debug<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Debug<T0, T1>(exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Debug<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Debug<T0, T1, T2>(exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Debug(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Debug(exception, msgTemp, propertyValues);
        }

        public void Information(string messageTemplate)
        {
            logger.Information( msgTemp);
        }

        public void Information<T>(string messageTemplate, T propertyValue)
        {
            logger.Information<T>(msgTemp, propertyValue);
        }

        public void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Information<T0, T1>(msgTemp, propertyValue0, propertyValue1);
        }

        public void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Information<T0, T1, T2>(msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Information(string messageTemplate, params object[] propertyValues)
        {
            logger.Information(msgTemp, propertyValues);
        }

        public void Information(Exception exception, string messageTemplate)
        {
            logger.Information(exception, msgTemp);
        }

        public void Information<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Information<T>(exception, msgTemp, propertyValue);
        }

        public void Information<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Information<T0, T1>(exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Information<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Information<T0, T1, T2>(exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Information(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Information(exception, msgTemp, propertyValues);
        }

        public void Warning(string messageTemplate)
        {
            logger.Warning(msgTemp);
        }

        public void Warning<T>(string messageTemplate, T propertyValue)
        {
            logger.Warning<T>(msgTemp, propertyValue);
        }

        public void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Warning<T0, T1>(msgTemp, propertyValue0, propertyValue1);
        }

        public void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Warning<T0, T1, T2>(msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Warning(string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(msgTemp, propertyValues);
        }

        public void Warning(Exception exception, string messageTemplate)
        {

            logger.Warning(exception, msgTemp);
        }

        public void Warning<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Warning<T>(exception, msgTemp, propertyValue);
        }

        public void Warning<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Warning<T0, T1>(exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Warning<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Warning<T0, T1, T2>(exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Warning(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(exception, msgTemp, propertyValues);
        }

        public void Error(string messageTemplate)
        {
            logger.Error(msgTemp);
        }

        public void Error<T>(string messageTemplate, T propertyValue)
        {
            logger.Error<T>(msgTemp, propertyValue);
        }

        public void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Error<T0, T1>(msgTemp, propertyValue0, propertyValue1);
        }

        public void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Error<T0, T1, T2>(msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error(string messageTemplate, params object[] propertyValues)
        {
            logger.Error(msgTemp, propertyValues);
        }

        public void Error(Exception exception, string messageTemplate)
        {
            logger.Error(exception, msgTemp);
        }

        public void Error<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Error<T>(exception, msgTemp, propertyValue);
        }

        public void Error<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Error<T0, T1>(exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Error<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Error<T0, T1, T2>(exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Error(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Error(exception, msgTemp, propertyValues);
        }

        public void Fatal(string messageTemplate)
        {
            logger.Fatal(msgTemp);
        }

        public void Fatal<T>(string messageTemplate, T propertyValue)
        {
            logger.Error<T>(msgTemp, propertyValue);
        }

        public void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Fatal<T0, T1>(msgTemp, propertyValue0, propertyValue1);
        }

        public void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
        {
            logger.Fatal<T0, T1, T2>(msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Fatal(string messageTemplate, params object[] propertyValues)
        {
            logger.Fatal(msgTemp, propertyValues);
        }

        public void Fatal(Exception exception, string messageTemplate)
        {
            logger.Fatal(exception, msgTemp);
        }

        public void Fatal<T>(Exception exception, string messageTemplate, T propertyValue)
        {
            logger.Fatal<T>(exception, msgTemp, propertyValue);
        }

        public void Fatal<T0, T1>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
        {
            logger.Fatal<T0, T1>(exception, msgTemp, propertyValue0, propertyValue1);
        }

        public void Fatal<T0, T1, T2>(Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1,
            T2 propertyValue2)
        {
            logger.Fatal<T0, T1, T2>(exception, msgTemp, propertyValue0, propertyValue1, propertyValue2);
        }

        public void Fatal(Exception exception, string messageTemplate, params object[] propertyValues)
        {
            logger.Fatal(exception, msgTemp, propertyValues);
        }

        public bool BindMessageTemplate(string messageTemplate, object[] propertyValues, out MessageTemplate parsedTemplate,
            out IEnumerable<LogEventProperty> boundProperties)
        {
          return   logger.BindMessageTemplate(messageTemplate, propertyValues,out parsedTemplate,out boundProperties);
        }

        public bool BindProperty(string propertyName, object value, bool destructureObjects, out LogEventProperty property)
        {
            return logger.BindProperty(propertyName, value, destructureObjects,out property);
        }
    }
}
