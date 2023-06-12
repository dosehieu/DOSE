using DOSE.BASE.PlatformCore.Database.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Database
{
    public static class StartupExtension
    {
        public static void UseDatabaseService(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseService, DatabaseService>();
        }
    }
}
