using DOSE.BASE.PlatformCore.BL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DOSE.BASE.Platform.Web
{
    public static class BaseStartupConfig
    {
        public static void ConfigureServices(ref IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtAuthentication(configuration);

            services.UserCoreBL(configuration);

            services.AddTransient<IJwtService, JwtService>();
        }
    } 
}
