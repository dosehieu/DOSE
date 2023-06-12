using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.EmployeeProfile.BL.Dashboard;
using DOSE.EmployeeProfile.BL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOSE.EmployeeProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : EmployeeProfileBaseController
    {
        public WeatherForecastController(CoreServiceCollection coreServiceCollection, IWeatherForecastBL weatherForecastBL) : base(coreServiceCollection)
        {
            _currentModelType = typeof(WeatherForecast);
            _BL = weatherForecastBL;
        }
    }
}
