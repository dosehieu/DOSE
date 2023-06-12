using DOSE.BASE.Auth.API.Model;
using DOSE.BASE.Auth.BL;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Web.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOSE.BASE.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(CoreServiceCollection serviceCollection) : base(serviceCollection)
        {

        }
        [HttpPost("login")]
        public async Task<ServiceRespone> Login([FromBody] Model.LoginParam param)
        {
            try
            {
                _serviceRespone.Data = await (_BL as IAccountBL).Login(param);
                return _serviceRespone.OnSuccess();
            }
            catch (Exception ex)
            {
                return _serviceRespone.OnError(ex.InnerException.ToString());
            }
        }
    }
}
