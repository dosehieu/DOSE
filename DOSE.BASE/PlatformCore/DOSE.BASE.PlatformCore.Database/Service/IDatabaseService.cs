using Dapper;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Database.SQLHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DOSE.BASE.PlatformCore.Database.Service
{
    public interface IDatabaseService
    {
        Task<List<T>> QueryUsingCommandTextAsync<T>(Guid tenantID, string appCode, string sql, Dictionary<string, object> param, IDbTransaction transaction = null, IDbConnection connection = null);
        Task<List<T>> ExecuteStoredProceduceAsync<T>(Guid tenantID, string appCode, string sql, Dictionary<string, object> param, IDbTransaction transaction = null, IDbConnection connection = null);
        Task<List<object>> ExecuteStoredProceduceAsync(Guid tenantID, string appCode, string sql, Dictionary<string, object> param, IDbTransaction transaction = null, IDbConnection connection = null);
        Task<PagingRespone> GetPagingUsingCommandTextAsync(Type type, Guid tenantID, string appCode, int pageIndex, int pageSize, string filter, string customFilter, string sort,  WhereParameter whereParameter = null, string column = "");
        Task<List<object>> GetAllUsingCommandTextAsync(Type typeModel, Guid tenantID, string appCode, string filter = "", string sort = "", string customFilter = "", WhereParameter whereParameter = null, string column = "", string viewOrTableName = "");

    }
}
