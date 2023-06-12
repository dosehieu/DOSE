using System;

namespace DOSE.System.Model
{
    public class LoginWithTenantParam
    {
        public string Token { get; set; }
        public Guid TenantID { get; set; }
        public bool IsSwitch { get; set; }
    }
}
