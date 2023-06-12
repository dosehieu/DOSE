using DOSE.BASE.PlatformCore.Common.Constant;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public class LogService : ILogService
    {
        private static Logger _currentLogger;
        private static Logger CurrentLogger {
            get { return _currentLogger; }
            set { _currentLogger = value; }
        }
        public void LogInfo(string message, Dictionary<string,object> prop = null)
        {
            var logEventInfo = CreateLogEventInfo(LogLevel.Info , message, prop);
            CurrentLogger.Log(logEventInfo);
        }
        public void LogError(Exception ex, string message, Dictionary<string,object> prop = null)
        {
            var logEventInfo = CreateLogEventInfo(LogLevel.Error , message, prop, ex);
            CurrentLogger.Log(logEventInfo);
        }
        public LogEventInfo CreateLogEventInfo(LogLevel logLevel, string message, Dictionary<string,object> prop, Exception ex = null)
        {
            var logEventInfo = new LogEventInfo() { Level= logLevel, Message = message, Exception = ex };
            return logEventInfo;
        }


    }
}
