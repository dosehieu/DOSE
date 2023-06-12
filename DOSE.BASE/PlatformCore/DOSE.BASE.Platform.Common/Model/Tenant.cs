using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Model
{
    public class Tenant
    {
        public Guid TenantID { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string Tax { get; set; }
        public string PhoneNumber { get; set; }
    }
}
