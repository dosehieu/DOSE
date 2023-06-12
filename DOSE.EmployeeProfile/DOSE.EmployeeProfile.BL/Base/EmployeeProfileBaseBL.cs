using DOSE.BASE.PlatformCore.BL.Base;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Constant;
using DOSE.BASE.PlatformCore.Common.Enum;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.EmployeeProfile.BL.Interface;
using DOSE.EmployeeProfile.Common.Constant;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.EmployeeProfile.BL.Base
{
    public class EmployeeProfileBaseBL : BaseBL, IEmployeeProfileBaseBL
    {
        public override string ApplicationCode => AppCode.EmployeeProfile;
        public EmployeeProfileBaseBL(CoreServiceCollection coreServiceCollection) : base (coreServiceCollection)
        {
        }
        public override string[] GetPermissionView(BaseModel baseModel)
        {
            return new string[1] { PermissionCode.View };
        }
        public override string[] GetPermissionSave(BaseModel baseModel)
        {
            if(baseModel.State == ModelState.Insert || baseModel.State == ModelState.Dublicate)
            {
                return new string[1] { PermissionCode.Create };
            }
            else if (baseModel.State == ModelState.Update )
            {
                return new string[1] { PermissionCode.Edit };
            }
            else
            {
                return null;
            }
        }
        public override string[] GetPermissionDelete(BaseModel baseModel)
        {
            return new string[1] { PermissionCode.Delete };
        }
    }
}
