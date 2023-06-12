using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public class ConfigService : IConfigService
    {
        public IConfigurationSection _appsetting;
        public IConfigurationSection _connectionString;
        public IConfigurationSection _apiUrls;
        public ConfigService(IConfiguration configurationSection)
        {
            _appsetting = configurationSection.GetSection("Appsetting");
            _connectionString = configurationSection.GetSection("ConnectionStrings");
            _apiUrls = configurationSection.GetSection("APIUrl");
        }
        public string GetConfigKey(string key)
        {
            return _appsetting[key];
        }

        public string GetConnectionString(string name)
        {
            return _connectionString[name];
        }
        public string GetAppSetting(string key, string defaultValue = null)
        {
            var appSetting = _appsetting[key];
            if (string.IsNullOrWhiteSpace(appSetting) && !string.IsNullOrWhiteSpace(defaultValue))
            {
                appSetting = defaultValue;
            }
            return appSetting;
        }

        public string GetAPIUrl(string key)
        {
            return _apiUrls[key];
        }
    }
}
