// Logger.cs
using LoggingLibrary.Abstraction;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace LoggingLibrary
{
    public class Logger: ILoggingLibrary
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        static Logger()
        {
            // Cấu hình NLog từ tệp nlog.config
            LogManager.LoadConfiguration("nlog.config");
        }

        public static void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public static void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        public static void LogException(Exception ex)
        {
            _logger.Error(ex);
        }
    }
}
