using DOSE.BASE.PlatformCore.BL.Base;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Web.Controller
{
    public class BaseCoreController : ControllerBase
    {
        public IBaseBL _BL;
        public Type _currentModelType;
        public CoreServiceCollection _serviceCollection;
        public ServiceRespone _serviceRespone = new ServiceRespone();
        public BaseCoreController(CoreServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }
        protected IBaseBL BL
        {
            get
            {
                if (_BL == null)
                {
                    throw new Exception("Chưa gán giá trị cho BL");
                }
                else return _BL;
            }
            set
            {
                _BL = BL;
            }
        }
        protected Type CurrentModelType
        {
            get
            {
                if (_currentModelType == null)
                {
                    throw new Exception("Chưa gán giá trị cho CurentModelType");
                }
                else return _currentModelType;
            }
            set
            {
                _currentModelType = CurrentModelType;
            }
        }
        
        
    }
}
