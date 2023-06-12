using System;

namespace DOSE.System.Model
{
    public class SessionLogin 
    {
        public string SessionID { get; set; }

        public string UserID { get; set; }
        public string Token { get; set; }
        public string TokenExpire { get; set; }
        public string RegisterToken { get; set; }
    }
}
