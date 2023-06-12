using DOSE.BASE.PlatformCore.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.System.Model.System
{
    public class LogSystem : BaseModel
    {
        public string Action { get; set; }
        public int? ActionType { get; set; }
        public int IPAddress { get; set; }
        public string Message { get; set; }
        public string ModelID { get; set; }
        public string ModelName { get; set; }
        public string RawData { get; set; }
        public string Reference { get; set; }
        public string TableName { get; set; }
        public Guid? UserID { get; set; }
        public string UserName { get; set; }
    }
}
