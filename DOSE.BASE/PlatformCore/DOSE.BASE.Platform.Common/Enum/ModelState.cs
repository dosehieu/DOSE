using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Enum
{
    public enum ModelState : int
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Dublicate = 4,
        Restore = 5,
        Sync = 6,
    }
}
