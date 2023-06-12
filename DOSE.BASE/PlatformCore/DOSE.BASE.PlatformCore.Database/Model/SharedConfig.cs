using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Database.Model
{
    public class SharedConfig
    {
        public int SharedID { get; set; }
        public string Database { get; set; }
        public string AppCode { get; set; }
        public string Server { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
    }
}
