using DOSE.BASE.PlatformCore.BL;
using DOSE.BASE.PlatformCore.Common.Constant;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DOSE.BASE.Platform.Web
{
    public static class JwtAuthorizationExtension
    {
        public static string[] Items =
         {
            JwtAuthorizeKey.TenantID,
            JwtAuthorizeKey.TenantName,
            JwtAuthorizeKey.TenantCode,
            JwtAuthorizeKey.ShortName
        };

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var secret = config.GetSection("appsetting").GetSection("JwtSecretKey").Value;
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "DOSEJSC",
                };
            });

            return services;
        }

    } 
}
