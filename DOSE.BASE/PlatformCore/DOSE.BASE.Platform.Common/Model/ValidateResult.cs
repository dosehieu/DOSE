using DOSE.BASE.PlatformCore.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Model
{
    public class ValidateResult
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string ErrorMessage { get; set; }
        public object AddInfo { get; set; }
        public ValidateType ValidateType { get; set; }
    }
}
