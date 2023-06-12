using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Web.Controller;
using DOSE.System.BL;
using DOSE.System.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOSE.System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController 
    {
        private readonly IAccountBL _accountBL;
        public AccountController(IAccountBL accountBL)
        {
            _accountBL = accountBL;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ServiceRespone> Login([FromBody] LoginParam loginParam)
        {
            var serviceRespone = new ServiceRespone();
            try
            {
                serviceRespone = await _accountBL.Login(loginParam);
                return serviceRespone.OnSuccess();
            }
            catch (Exception ex)
            {
                return serviceRespone.OnError(ex.InnerException.ToString());
            }
        }

        [HttpPost]
        [Route("login-with-tenant")]
        public async Task<ServiceRespone> LoginWithTenant([FromBody] LoginWithTenantParam loginParam)
        {
            var serviceRespone = new ServiceRespone();
            try
            {
                serviceRespone = await _accountBL.LoginWithTenant(loginParam, HttpContext.Request, );
                return serviceRespone.OnSuccess();
            }
            catch (Exception ex)
            {
                return serviceRespone.OnError(ex.InnerException.ToString());
            }
        }
    }
}
