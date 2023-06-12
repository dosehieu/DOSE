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
    public class AccountBL : BaseBL, IAccountBL
    {
        private readonly IJwtService _jwtService;
        public AccountBL(CoreServiceCollection serviceCollection, IJwtService jwtService) : base(serviceCollection)
        {
            _jwtService = jwtService;
        } 
        public async Task<ServiceRespone> Login(LoginParam loginParam, HttpRequest httpRequest, HttpResponse httpResponse)
        {
            var respone = new ServiceRespone();
            var param = new Dictionary<string, object>()
            {
                {"@p_UserName", loginParam.UserName },
                {"@p_PasswordHash", loginParam.PasswordHash }
            };

            var userLogins = await _databaseService.ExecuteStoredProceduceAsync<UserLogin>(_tenantID, AppCode.MANAGERMENT, "Proc_GetUserLoginByLoginParam", param);

            if(userLogins != null && userLogins.Count > 0)
            {
                var userLogin = userLogins.FirstOrDefault();
                param = new Dictionary<string, object>()
                {
                    {"@p_UserID", userLogin.UserID },
                };
                var tenants = await _databaseService.ExecuteStoredProceduceAsync<Tenant>(_tenantID, AppCode.MANAGERMENT, "Proc_GetListTenantByUserID", param);

                var payload = new Dictionary<string, string>()
                {
                    { "Tenants", Converter.Serialize(tenants) },
                    { "DOSEID", userLogin.UserID.ToString() },
                };
                string token = _jwtService.GenerateSecurityToken(payload);
                string registerToken = _jwtService.GenerateRegisterToken(payload);
                return respone.OnSuccess(new
                {
                    AccessToken = new
                    {
                        Token = token,
                        TokenExpired = DateTime.UtcNow.AddMinutes(_jwtService.GetExpireTime())
                    },
                    RegisterToken = registerToken,
                    ListTenant = tenants,
                    IsMultiTenants = tenants.Count > 0,
                    DOSEID = userLogin.UserID
                });
            }
            return null;
        }

        public async Task<ServiceRespone> LoginWithTenant(LoginWithTenantParam param, HttpRequest request, HttpResponse response)
        {
            var result = new ServiceRespone();
            SecurityToken securityToken;
            try
            {
                securityToken = _jwtService.ValidateSecurityToken(param.Token);
            }
            catch (Exception ex)
            {
                return result.OnError("Invalid Token",ex.Message);
            }
            var token = (JwtSecurityToken)securityToken;
            var payload = token.Payload;
            if (token.Payload.ContainsKey("Tenants"))
            {
                var tenants = Converter.Deserialize<List<Tenant>>(token.Payload.GetValueOrDefault("Tenants").ToString());
                var tenant = tenants.Find(n => n.TenantID == param.TenantID);
                if(tenant!= null)
                {
                    var doseID = token.Payload.GetValueOrDefault("DOSEID").ToString();
                    await LoginWithDOSEID(doseID, tenant.TenantID, request, response, result);
                }
                else
                {
                    result.OnError("TenantID not valid");
                }
            }
            else
            {
                result.OnError("Tenants is empty");
            }
            return result.OnSuccess();

        }

        public async Task LoginWithDOSEID(string doseID, Guid tenantID, HttpRequest request, HttpResponse response, ServiceRespone result)
        {
            var param = new Dictionary<string, object>()
                {
                    {"@p_DoseID", doseID },
                };
            var userLogins = await _databaseService.ExecuteStoredProceduceAsync<UserLogin>(_tenantID, AppCode.MANAGERMENT, "Proc_GetUserLoginByDOSEID", param);
            string sessionID = "";
            if (userLogins != null && userLogins.Count > 0)
            {
                var userLogin = userLogins.FirstOrDefault();
                sessionID = await GenerateSessionID(userLogin, tenantID, request, response);
            }
            result.Data = new { SessionID = sessionID };
        }

        public async Task<string> GenerateSessionID(UserLogin userLogin,Guid tenantID, HttpRequest request, HttpResponse response)
        {
            var sessionID = Guid.NewGuid().ToString().Replace("-", string.Empty);
            Converter.AddCookie(response, Keys.SessionID, sessionID);

            var param = new Dictionary<string, object>()
                {
                    {"@p_SessionID", sessionID },
                    {"@p_TenantID", tenantID },
                    {"@p_DoseID", userLogin.DOSEID },
                    {"@p_UserID", userLogin.UserID },
                };
            await _databaseService.ExecuteStoredProceduceAsync<UserLogin>(_tenantID, AppCode.MANAGERMENT, "Proc_SaveSession", param);

            Converter.AddCookie(response, Keys.TenantID, Encoding.UTF8.GetBytes(tenantID.ToString()).ToString());
            return sessionID;
        }
    }
}
