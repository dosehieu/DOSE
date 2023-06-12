using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Service
{
    public interface IAuthService
    {
        Guid GetTenantID();
        bool CheckCallInternalAPI();
    }
}
