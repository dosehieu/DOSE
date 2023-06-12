using DOSE.BASE.PlatformCore.Common.Constant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public class AuthService : IAuthService
    {
        public IHttpContextAccessor _httpContext;
        public IConfigService _configService;

        public AuthService(IConfigService configService, IHttpContextAccessor httpContext)
        {
            _configService = configService;
            _httpContext = httpContext;
        }
        public string GetSesstionID()
        {
            return GetHeaderByName(Keys.SessionID);
        }
        public Guid GetTenantID()
        {
            var userId = GetItemByName(Keys.TenantID);
            if (string.IsNullOrEmpty(userId))
            {
                userId = DefaultConstant.INVALID_GUILD;
            }
            return Guid.Parse(userId);
        }
        private string GetItemByName(string name)
        {
            return _httpContext?.HttpContext?.Items[name] + "";
        }
       
        private string GetHeaderByName(string name)
        {
            return _httpContext?.HttpContext?.Request?.Headers[name] + "";
        }

        private string GetCulture()
        {
            return _httpContext?.HttpContext?.Request?.Headers[Keys.Culture] + "";
        }

        /// <summary>
        /// Kiểm tra có phải gọi api nội bộ không
        /// </summary>
        /// <returns></returns>
        public bool CheckCallInternalAPI()
        {
            var hasInternalAPIToken = _httpContext?.HttpContext?.Request?.Headers?.ContainsKey(Keys.InternalAPIToken);
            if (hasInternalAPIToken.HasValue && hasInternalAPIToken.Value)
            {
                var internalAPIToken = _httpContext?.HttpContext?.Request?.Headers[Keys.InternalAPIToken];
                var serverAPIToken = _configService.GetAppSetting(AppSettingKeys.InternalAPIToken);
                if(!string.IsNullOrWhiteSpace(serverAPIToken) && serverAPIToken.Equals(internalAPIToken, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
