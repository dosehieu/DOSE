
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.EmployeeProfile.BL.Base;
using DOSE.EmployeeProfile.BL.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.EmployeeProfile.BL.Dashboard
{
    public class WeatherForecastBL : EmployeeProfileBaseBL, IWeatherForecastBL
    {
        public WeatherForecastBL(CoreServiceCollection serviceCollection) : base(serviceCollection)
        {

        }
    }
}
