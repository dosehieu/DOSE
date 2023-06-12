using DOSE.BASE.PlatformCore.BL;
using DOSE.BASE.PlatformCore.Common.Constant;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Service;

namespace DOSE.BASE.Platform.Web
{
    public interface IJwtService
    {
        string GenerateSecurityToken(Dictionary<string, string> payload);
        string GenerateRegisterToken(Dictionary<string, string> payload);
        SecurityToken ValidateSecurityToken(string token);
        double GetExpireTime()
    } 
}
