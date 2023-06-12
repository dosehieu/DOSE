using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Web.Controller;
using DOSE.EmployeeProfile.BL.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOSE.EmployeeProfile.API.Controllers
{
    public class EmployeeProfileBaseController : BaseController
    {
        public EmployeeProfileBaseController(CoreServiceCollection serviceCollection) : base(serviceCollection)
        {

        }
    }
}
