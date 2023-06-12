using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.System.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DOSE.System.BL
{
    public interface IAccountBL
    {
        Task<ServiceRespone> Login(LoginParam loginParam, HttpRequest httpRequest, HttpResponse httpResponse);
        Task<ServiceRespone> LoginWithTenant(LoginWithTenantParam loginParam, HttpRequest httpRequest, HttpResponse httpResponse);
    }
}
