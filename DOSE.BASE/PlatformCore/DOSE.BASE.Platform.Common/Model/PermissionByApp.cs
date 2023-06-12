using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Model
{
    public class PermissionByApp
    {
        public Guid? OrganizationUnitID { get; set; }
        public string LevelCode { get; set; }
        public Dictionary<string, string> PermissionDetail { get; set; }

        public PermissionByApp()
        {
            PermissionDetail = new Dictionary<string, string>();
        }
    }
}
