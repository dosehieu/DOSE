using DOSE.BASE.PlatformCore.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOSE.BASE.Auth.API.Model
{
    public class Account : BaseModel
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
