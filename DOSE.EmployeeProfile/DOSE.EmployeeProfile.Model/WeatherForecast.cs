﻿using DOSE.BASE.PlatformCore.Common.Model;
using System;

namespace DOSE.EmployeeProfile.Model
{
    public class WeatherForecast : BaseModel
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
