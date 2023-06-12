using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Model.Base
{
    public class PagingRequest
    {
        
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Sort { get; set; }
        public string Filter { get; set; }
        public string CustomFilter { get; set; }
        public bool? UseProc { get; set; }
        public string Column { get; set; }
        public Dictionary<string, object> Param { get; set; }
        public Dictionary<string, object> CustomParam { get; set; }
    }
}
