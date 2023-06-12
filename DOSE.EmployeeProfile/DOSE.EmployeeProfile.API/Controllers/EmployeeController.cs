using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Web.Controller;
using DOSE.EmployeeProfile.BL.Base;
using DOSE.EmployeeProfile.BL.Interface;
using DOSE.EmployeeProfile.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOSE.EmployeeProfile.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : EmployeeProfileBaseController
    {
        public EmployeeController(CoreServiceCollection coreServiceCollection, IEmployeeBL employeeBL) : base(coreServiceCollection)
        {
            _currentModelType = typeof(Employee);
            _BL = employeeBL;
        }
    }
}
