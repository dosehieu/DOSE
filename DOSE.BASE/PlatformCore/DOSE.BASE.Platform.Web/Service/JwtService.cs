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
    public class JwtService: IJwtService
    {
        private readonly string _secret;
        private readonly string _expireTime;
        private readonly string _issuer;
        private readonly IConfigService _configService;

        public JwtService(IConfigService configService)
        {
            _configService = configService;
            _secret = _configService.GetAppSetting("JwtSecretKey");
            _expireTime = _configService.GetAppSetting("JwtExpireTime");
            _issuer = _configService.GetAppSetting("JwtIssuer");
        }

        public string GenerateSecurityToken(Dictionary<string, string> payload)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var claims = new List<Claim>();
            foreach(var item in payload)
            {
                claims.Add(new Claim(item.Key.ToString(), item.Value.ToString()));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expireTime)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public SecurityToken ValidateSecurityToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters() {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true
                }, out SecurityToken validatedToken);
                return validatedToken;
            }
	        catch
	        {
		        return null;
	        }
        }
        public double GetExpireTime()
        {
            return double.Parse(_expireTime);
        }
    } 
}
