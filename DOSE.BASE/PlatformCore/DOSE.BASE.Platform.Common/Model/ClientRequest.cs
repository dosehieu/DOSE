using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Model
{
    public class ClientRequest
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Sort { get; set; }
        public string Filter { get; set; }
        public Dictionary<string, object> CustomParam { get; set; }
    }
}
