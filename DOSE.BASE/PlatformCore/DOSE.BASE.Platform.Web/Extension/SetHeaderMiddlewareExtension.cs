using DOSE.BASE.PlatformCore.BL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DOSE.BASE.Platform.Web
{
    public static class SetHeaderMiddlewareExtension
    {
        public static IApplicationBuilder UseSetHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetHeaderMiddleware>();
        }
    }
    public class SetHeaderMiddleware {

        private readonly RequestDelegate _next;

        public SetHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var iscontinue = SetHeader(context);
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        public bool SetHeader(HttpContext context)
        {
            return true;
        }
    }

}
