using DOSE.BASE.PlatformCore.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.System.Model.System
{
    class User : BaseModel
    {
        public string EmployeeCode { get; set; }
        public int EmployeeStatus { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public bool? Gender { get; set; }
        public DateTime? HireDate { get; set; }
        public bool? IsEditPermission { get; set; }
        public bool? IsStaff { get; set; }
        public bool? IsUser { get; set; }
        public int JobPositionID { get; set; }
        public string JobPositionName { get; set; }
        public string LastName { get; set; }
        public Guid MISAID { get; set; }
        public string MISAIDEmail { get; set; }
        public string Mobile { get; set; }
        public string OfficeEmail { get; set; }
        public int OrganizationUnitID { get; set; }
        public string OrganizationUnitName { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string RoleDetails { get; set; }
        public int? Status { get; set; }
        public DateTime? TerminationDate { get; set; }
        public Guid? UserID { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
    }
}
