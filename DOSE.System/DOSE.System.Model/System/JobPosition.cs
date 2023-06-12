using DOSE.BASE.PlatformCore.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.System.Model.System
{
    public class JobPosition : BaseModel
    {
        public int JobPositionID { get; set; }
        public string JobPositionCode { get; set; }
        public string JobPositionName { get; set; }
        public string ListOrgDetails { get; set; }
        public string ListOrgJSON { get; set; }
        public string OrganizationUnitName { get; set; }
        public string OrganizationUnits { get; set; }
        public string OrganizationUnitIDs { get; set; }
        public int? SortOrder { get; set; }
        public bool? Inactive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
