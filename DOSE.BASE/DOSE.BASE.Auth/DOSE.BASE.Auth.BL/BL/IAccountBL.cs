using DOSE.BASE.PlatformCore.BL.Base;
using System;
using System.Threading.Tasks;

namespace DOSE.BASE.Auth.BL
{
    public interface IAccountBL : IBaseBL
    {
        Task<object> Login(LoginParam param);
    }
}
