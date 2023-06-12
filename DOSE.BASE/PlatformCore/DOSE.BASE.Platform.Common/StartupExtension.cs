using DOSE.BASE.PlatformCore.Common.Constant;
using DOSE.BASE.PlatformCore.Common.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common
{
    public static class StartupExtension
    {
        public static void UseCoreService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigService, ConfigService>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            var redisConfig = configuration.GetSection("ConnectionStirngs: RedisCache").Value;
            if(!string.IsNullOrWhiteSpace(redisConfig))
            {
                services.AddStackExchangeRedisCache(option =>
                {
                    option.Configuration = redisConfig;
                    option.InstanceName = CacheKeys.RedisCacheInstantName;
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }
        }
    }
}
