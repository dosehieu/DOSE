using DOSE.BASE.PlatformCore.BL.Base;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.EmployeeProfile.BL.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.EmployeeProfile.BL.Base
{
    public class EmployeeBL: EmployeeProfileBaseBL, IEmployeeBL
    {
        public EmployeeBL(CoreServiceCollection coreServiceCollection) : base(coreServiceCollection)
        {

        }
    }
}
