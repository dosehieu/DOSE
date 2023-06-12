using DOSE.EmployeeProfile.BL.Base;
using DOSE.EmployeeProfile.BL.Dashboard;
using DOSE.EmployeeProfile.BL.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.EmployeeProfile.BL
{
    public static class StartupExtension
    {
        public static void UseEmployeeProfileBL(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddTransient<IEmployeeBL, EmployeeBL>();
            service.AddTransient<IWeatherForecastBL, WeatherForecastBL>();
            service.AddTransient<IEmployeeProfileBaseBL, EmployeeProfileBaseBL>();
        }
    }
}
