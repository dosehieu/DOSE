using DOSE.BASE.PlatformCore.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.System.Model.System
{
    public class OrganizationUnit: BaseModel
    {
        public int OrganizationUnitID { get; set; }
        public string OrganizationUnitCode { get; set; }
        public string OrganizationUnitName { get; set; }
        public int OrganizationUnitTypeID { get; set; }
        public string OrganizationUnitTypeName { get; set; }
        public string Address { get; set; }
        public bool? Inactive { get; set; }
        public bool? IsBusiness { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsParent { get; set; }
        public bool? IsProduce { get; set; }
        public bool? IsSupport { get; set; }
        public string DOSECode { get; set; }
        public int? ParentID { get; set; }
        public string ParentName { get; set; }
        public int QuantityEmployee { get; set; }
        public int SortOrder { get; set; }
    }
}
