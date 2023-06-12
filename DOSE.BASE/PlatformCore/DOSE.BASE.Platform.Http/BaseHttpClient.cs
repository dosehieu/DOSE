using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DOSE.BASE.Platform.Http
{
    public class BaseHttpClient
    {
        public IHttpContextAccessor _httpContextAccessor;
        public IConfigService _configService;
        public ILogService _logService;

        public BaseHttpClient(IHttpContextAccessor httpContextAccessor, IConfigService configService, ILogService logService)
        {
            _httpContextAccessor = httpContextAccessor;
            _configService = configService;
            _logService = logService;
        }
        public string GetAPIUrl(string apiUrl)
        {
            return _configService.GetAPIUrl(apiUrl);
        }
    }
}
