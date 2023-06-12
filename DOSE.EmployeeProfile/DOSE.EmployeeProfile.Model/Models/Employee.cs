using DOSE.BASE.Platform.Common;
using DOSE.BASE.PlatformCore.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DOSE.EmployeeProfile.Model.Models
{
    [ConfigTable("employee")]
    public class Employee: BaseModel
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int? JobPositionID { get; set; }
        public string JobPositionName { get; set; }
        public int? OrganizationUnitID { get; set; }
        public string OrganizationUnitName { get; set; }
    }
}
