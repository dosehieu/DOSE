using System;

namespace DOSE.System.Model
{
    public class UserLogin
    {
        public Guid UserID { get; set; }
        public Guid DOSEID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
    }
}
