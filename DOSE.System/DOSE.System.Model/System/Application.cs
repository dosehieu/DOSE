using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.System.Model.System
{
    public class Application
    {
        public int ApplicationID { get; set; }
        public int ApplicationCategoryID { get; set; }
        public string ApplicationCode { get; set; }
        public string ApplicationInfo { get; set; }
        public string ApplicationName { get; set; }
        public string Description { get; set; }
        public bool? IsDefaultForAllEmployees { get; set; }
        public bool? IsDefaultForAllUsers { get; set; }
        public int SecurityType { get; set; }
        public string URL { get; set; }
    }
}
