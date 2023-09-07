using Serilog.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ImTool.LogWindow<Anvil.LogCategories>;

namespace Anvil
{
    public static class Logging
    {
        public static void Log(LogCategories cat, LogEventLevel level, string message, params object[] args)
        {
            Serilog.Log.ForContext("Category", cat).Write(level, message, args);
        }

        public static void LogInfo(LogCategories cat, string message, params object[] args)
        {
            Log(cat, LogEventLevel.Information, message, args);
        }

        public static void LogWarning(LogCategories cat, string message, params object[] args)
        {
            Log(cat, LogEventLevel.Warning, message, args);
        }

        public static void LogError(LogCategories cat, string message, params object[] args)
        {
            Log(cat, LogEventLevel.Error, message, args);
        }

        public static void LogDebug(LogCategories cat, string message, params object[] args)
        {
            Log(cat, LogEventLevel.Debug, message, args);
        }
    }

    public class AnvilSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public AnvilSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            var anvilLogLevel = logEvent.Level switch
            {
                LogEventLevel.Information => LogLevel.Info,
                LogEventLevel.Warning     => LogLevel.Warn,
                LogEventLevel.Error       => LogLevel.Error,
                LogEventLevel.Debug       => LogLevel.Trace,
                _                         => LogLevel.Warn,
            };

            var cat = Enum.Parse<LogCategories>(logEvent.Properties["Category"].ToString());
            AnvilTool.Ref.LogWindow.AddLog(anvilLogLevel, cat, message);
        }
    }

    public static class AnvilSinkExtensions
    {
        public static LoggerConfiguration AnvilSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new AnvilSink(formatProvider));
        }
    }
}
