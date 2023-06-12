using DOSE.BASE.Platform.Web;
using DOSE.BASE.PlatformCore.BL.Base;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Constant;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Utility;
using DOSE.System.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOSE.System.BL
{
    public class SystemBaseBL : BaseBL, ISystemBaseBL
    {
        public SystemBaseBL(CoreServiceCollection serviceCollection) : base(serviceCollection)
        {
        } 
    }
}
