using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Database.SQLHelper;

namespace DOSE.BASE.PlatformCore.BL.Base
{
    public interface IBaseBL
    {
        public Task<object> AddOrUpdate(BaseModel model);
        public Task<List<object>> GetAllAsync(Type typeName, string filter = "", string sort = "", string customFilter = "", WhereParameter whereParameter = null, string colum = "", string viewOrTableName = "");
        public Task<PagingRespone> GetPagingAsync(Type model, PagingRequest pagingRequest, Dictionary<string, object> param = null);
        public Task<object> GetByIDAsync(Type model, string id);
        public string[] GetPermissionView(BaseModel baseModel);
        public string[] GetPermissionSave(BaseModel baseModel);
        public string[] GetPermissionDelete(BaseModel baseModel);
        public List<ValidateResult> CheckPermission(BaseModel baseModel, string[] action, Guid? organozationUnitID = null);
    }
}
