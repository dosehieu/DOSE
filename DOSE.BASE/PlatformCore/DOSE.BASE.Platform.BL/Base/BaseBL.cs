using Dapper;
using DOSE.BASE.Platform.Http;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common;
using DOSE.BASE.PlatformCore.Common.Attribute;
using DOSE.BASE.PlatformCore.Common.Constant;
using DOSE.BASE.PlatformCore.Common.Enum;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Common.Utility;
using DOSE.BASE.PlatformCore.Database.Service;
using DOSE.BASE.PlatformCore.Database.SQLHelper;
using DOSE.BASE.PlatformCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DOSE.BASE.PlatformCore.BL.Base
{
    public class BaseBL : IBaseBL
    {
        protected readonly IConfigService _configService;
        protected readonly ICacheService _cacheService;
        protected readonly IAuthService _authService;
        protected readonly IHttpService _httpService;
        protected readonly IDatabaseService _databaseService;
        public string _modelNamespace;
        public string _resourceNamespace;
        public string _enumNamespace;
        public Guid _tenantID;
        public string SubSystemCode;
        public virtual string ApplicationCode => string.Empty;
        public Guid TenantID
        {
            get { return _tenantID; }
            set { _tenantID = value; }
        }

        List<PermissionByApp> _permissions = new List<PermissionByApp>();

        public BaseBL(CoreServiceCollection serviceCollection)
        {
            _configService = serviceCollection.ConfigService;
            _authService = serviceCollection.AuthService;
            _cacheService = serviceCollection.CacheService;
            _databaseService = serviceCollection.DatabaseService;

            _modelNamespace = _configService.GetAppSetting(AppSettingKeys.ModelNamespace);
            _resourceNamespace = _configService.GetAppSetting(AppSettingKeys.ResourceNamespace);
            _enumNamespace = _configService.GetAppSetting(AppSettingKeys.EnumNamespace);
            //_tenantID = _authService.GetTenantID();
            TenantID = Guid.Parse("245a524e-499e-49f9-a681-ffd158a7ae98");
        }
        
        #region Permission
        public Dictionary<string, object> GetAllPermissionByApp()
        {
            return new Dictionary<string, object>();
        }
        public List<PermissionByApp> GetAllPermission()
        {
            var result = new List<PermissionByApp>();
            var allPermission = GetAllPermissionByApp();
            if (allPermission.ContainsKey($"{ApplicationCode}_Permission"))
            {
                result = (List<PermissionByApp>)allPermission.GetValueOrDefault($"{ApplicationCode}_Permission");
            }
            return result;
        }
        public string GetLevelCodeByOrganozationUnitID(Guid? organozationUnitID)
        {
            return "";
        }
        public bool CheckPermission(string subSystemCode, string[] permissionCodes, Guid? organozationUnitID = null, bool isANDPermission = true, bool checkPermissionWhenCallInternalAPI = false)
        {
            if (_authService.CheckCallInternalAPI() && !checkPermissionWhenCallInternalAPI)
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(subSystemCode) || permissionCodes == null || permissionCodes.Length == 0)
            {
                return false;
            }

            if (_permissions == null)
            {
                _permissions = GetAllPermission();
            }
            if (_permissions == null || _permissions.Count == 0)
            {
                return false;
            }
            List<PermissionByApp> permissions = new List<PermissionByApp>();
            if (organozationUnitID.HasValue && organozationUnitID != Guid.Empty)
            {
                var levelCode = GetLevelCodeByOrganozationUnitID(organozationUnitID);
                if (!string.IsNullOrWhiteSpace(levelCode))
                {
                    permissions = _permissions.Where(n => levelCode.StartsWith(n.LevelCode ?? "/999/")).ToList();
                }
                else
                {
                    permissions = _permissions;
                }
            }

            if (permissions == null || permissions.Count == 0)
            {
                return false;
            }
            bool check = false;
            foreach (var permissionCode in permissionCodes)
            {
                check = false;
                foreach (var permission in permissions)
                {
                    // Check SubsSystemCode 
                    var subsSystemCodePer = permission.PermissionDetail.FirstOrDefault(n => n.Key.Contains(subSystemCode, StringComparison.OrdinalIgnoreCase));
                    if (subsSystemCodePer.Key == null)
                    {
                        continue;
                    }

                    var permissionDetail = subsSystemCodePer.Value.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    check = permissionDetail.Any(n => n.Equals(permissionCode, StringComparison.OrdinalIgnoreCase));
                    if (check)
                    {
                        break;
                    }

                    if (check)
                    {
                        // Nếu chỉ cần 1 quyền
                        if (!isANDPermission)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        // Nếu chỉ cần tất cả quyền
                        if (isANDPermission)
                        {
                            return false;
                        }
                    }
                }
            }
            return check;
        }


        public virtual string[] GetPermissionView(BaseModel baseModel)
        {
            return new string[0];
        }
        public virtual string[] GetPermissionSave(BaseModel baseModel)
        {
            return new string[0];
        }
        public virtual string[] GetPermissionDelete(BaseModel baseModel)
        {
            return new string[0];
        }

        public virtual List<ValidateResult> CheckPermission(BaseModel baseModel, string[] action, Guid? organozationUnitID = null)
        {
            List<ValidateResult> validateResults = new List<ValidateResult>();
            if (!CheckPermission(SubSystemCode, action, organozationUnitID, true))
            {
                validateResults.Add(new ValidateResult()
                {
                    Code = ErrorCode.NO_PERMISSION,
                    ErrorMessage = CoreResource.NoPermission
                });
            }
            return validateResults;
        }

        #endregion

        #region query

        public async Task<object> AddOrUpdate(BaseModel model)
        {
            string proc = GetProcAddOrUpdate(model);
            var param = Converter.ToDatabaseParam(model);
            var res = await ExecuteStoredProceduceAsync(proc, param);
            return res;
        }
        public string GetProcAddOrUpdate(BaseModel model)
        {
            StringBuilder procName = new StringBuilder("Proc_");
            procName.Append(model.GetType().Name);
            if(model.State == ModelState.Insert)
            {
                procName.Append("_Insert");
            }
            else
            {
                procName.Append("_Update");
            }
            return procName.ToString();
        }

        public async Task<List<T>> QueryUsingCommandTextAsync<T>(string commandText, Dictionary<string, object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            return await _databaseService.QueryUsingCommandTextAsync<T>(_tenantID, this.ApplicationCode, commandText, param, transaction, connection);
        }
        
        public async Task<List<object>> ExecuteStoredProceduceAsync(string commandText, Dictionary<string, object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            return await _databaseService.ExecuteStoredProceduceAsync(_tenantID, this.ApplicationCode, commandText, param, transaction, connection);
        }
        public async Task<PagingRespone> GetPagingAsync(Type typeModel, PagingRequest pagingRequest, Dictionary<string, object> param = null)
        {
            var whereParameter = BuildWhereParameter(typeModel, pagingRequest);
            return await _databaseService.GetPagingUsingCommandTextAsync(typeModel, _tenantID, this.ApplicationCode, pagingRequest.PageIndex, pagingRequest.PageSize, pagingRequest.Filter, pagingRequest.CustomFilter, pagingRequest.Sort,  whereParameter, column: pagingRequest.Column);
        }

        public async Task<List<object>> GetAllAsync(Type typeModel, string filter = "", string sort = "", string customFilter = "", WhereParameter whereParameter = null, string column = "", string viewOrTableName = "")
        {
            return await _databaseService.GetAllUsingCommandTextAsync(typeModel, _tenantID, this.ApplicationCode, filter, sort, customFilter, whereParameter, column, viewOrTableName);
        }
        protected async Task<ServiceRespone> CallInternalAPI(string apiUrlKey, string apiPath, HttpMethod method, object param = null, string authorizationToken = "", Dictionary<string, string> header = null)
        {
            return await _httpService.CallInternalAPI(apiUrlKey, apiPath, method, param, authorizationToken, header);
        }

        public WhereParameter BuildWhereParameter(Type typeModel, PagingRequest pagingRequest)
        {
            var whereParameter = new WhereParameter();
            return whereParameter;
        }

        public async Task<object> GetByIDAsync(Type model, string idStr)
        {
            if (int.TryParse(idStr, out int id))
            {
                var param = new Dictionary<string, object>()
                {
                    { "@TenantID" , TenantID }
                };
                var sql = $"SELECT * FROM {model.GetTableName()} WHERE `{model.GetPrimaryKeyName()}` = {id}";
                return await QueryUsingCommandTextAsync<object>(sql, param);
            }
            return null;
        }

        #endregion
    }
}
