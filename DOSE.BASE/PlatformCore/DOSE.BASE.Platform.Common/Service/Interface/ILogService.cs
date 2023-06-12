using DOSE.BASE.PlatformCore.Common.Constant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public interface ILogService
    {
        void LogInfo(string message, Dictionary<string, object> prop = null);
        void LogError(Exception ex, string message, Dictionary<string, object> prop = null);
    }
}
