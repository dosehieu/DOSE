using DOSE.System.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DOSE.System.BL
{
    public static class StartupExtension
    {
        public static void UseSystemBL(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IAccountBL, AccountBL>();
        }
    }
}
