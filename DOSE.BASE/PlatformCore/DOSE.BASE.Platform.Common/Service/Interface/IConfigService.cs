using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public interface IConfigService
    {
        string GetConnectionString(string key);
        string GetAppSetting(string key, string defaultValue = null);
        string GetAPIUrl(string key);
    }
}
