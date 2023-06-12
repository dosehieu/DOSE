using Dapper;
using DOSE.BASE.PlatformCore.Common.Attribute;
using DOSE.BASE.PlatformCore.Common.Constant;
using DOSE.BASE.PlatformCore.Common.Model;
using DOSE.BASE.PlatformCore.Common.Service;
using DOSE.BASE.PlatformCore.Common.Utility;
using DOSE.BASE.PlatformCore.Database.Model;
using DOSE.BASE.PlatformCore.Database.SQLHelper;
using DOSE.BASE.PlatformCore.Database.Utility;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOSE.BASE.PlatformCore.Database.Service
{
    public class DatabaseService: IDatabaseService
    {
        public IConfigService _configService;

        public DatabaseService(IConfigService configService)
        {
            _configService = configService;
        }
        public async Task<List<T>> QueryUsingCommandTextAsync<T>(Guid tenantID, string appCode, string sql, Dictionary<string,object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            try
            {
                List<T> result = new List<T>();
                CommandDefinition cd = BuildCommandDefinition(sql, CommandType.Text, param, transaction);
                var con = transaction != null ? transaction.Connection : connection;
                if (con != null)
                {
                    var query = await con.QueryAsync<T>(cd);
                    result = query.AsList<T>();
                }
                if (con == null)
                {
                    using var conn = await GetConnectionAsync(tenantID, appCode);
                    var query = await conn.QueryAsync<T>(cd);
                    result = query.AsList<T>();
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<List<T>> ExecuteStoredProceduceAsync<T>(Guid tenantID, string appCode, string procName, Dictionary<string,object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            try
            {
                List<T> result = new List<T>();
                CommandDefinition cd = BuildCommandDefinition(procName, CommandType.StoredProcedure, param, transaction);
                var con = transaction != null ? transaction.Connection : connection;
                if (con != null)
                {
                    var query = await con.QueryAsync<T>(cd);
                    result = query.AsList<T>();
                }
                if (con == null)
                {
                    using var conn = await GetConnectionAsync(tenantID, appCode);
                    var query = await conn.QueryAsync<T>(cd);
                    result = query.AsList<T>();
                }
                return result;
            }
            catch(Exception ex)
            {
                var a = Guid.NewGuid();
                throw ex;
            }
        }
        
        public async Task<List<object>> ExecuteStoredProceduceAsync(Guid tenantID, string appCode, string procName, Dictionary<string,object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            try
            {
                List<object> result = new List<object>();
                CommandDefinition cd = BuildCommandDefinition(procName, CommandType.StoredProcedure, param, transaction);
                var con = transaction != null ? transaction.Connection : connection;
                if (con != null)
                {
                    var query = await con.QueryAsync<object>(cd);
                    result = query.AsList<object>();
                }
                if (con == null)
                {
                    using var conn = await GetConnectionAsync(tenantID, appCode);
                    var query = await conn.QueryAsync<object>(cd);
                    result = query.AsList<object>();
                }
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<SharedConfig>> GetTenantSharedConfigAsync(Guid tenantID, string masterDbConnectionString, string cacheKey)
        {
            var config = new List<SharedConfig>();
            using (var con = new SqlConnection(masterDbConnectionString))
            {
                var param = new { p_TenantID = tenantID };
                var query = await con.QueryAsync<SharedConfig>("proc_get", param, commandType: CommandType.StoredProcedure);
                config = query.ToList();
            }
            return config;
        }
        public string GetConnectionString(string appCode, List<SharedConfig> sharedConfig)
        {
            var config = sharedConfig.FirstOrDefault(n => n.AppCode.Equals(appCode, StringComparison.OrdinalIgnoreCase));
            if(config!= null)
            {
                var builder = new SqlConnectionStringBuilder()
                {
                    DataSource = config.Server,
                    InitialCatalog = config.Database,
                    UserID = config.UserID,
                    Password = config.Password,
                };
                return builder.ToString();
            }
            return null;
        }
        public async Task<string> GetConnectionStringAsync(Guid tenantID, string appCode)
        {
            //var masterDbConnectionString = _configService.GetConnectionString(ConnectionStringsKey.MasterDB);
            //if(tenantID == Guid.Empty || string.IsNullOrEmpty(appCode))
            //{
            //    return masterDbConnectionString;
            //}
            //var cacheKey = string.Format(CacheKeys.CacheSharedConnection, tenantID);

            //var sharedConfig = await GetTenantSharedConfigAsync(tenantID, masterDbConnectionString, cacheKey);
            //var connection = GetConnectionString(appCode, sharedConfig);

            string connectionStringsKey = ConnectionStringsKey.ManagementConn;
            switch (appCode)
            {
                case AppCode.EMPLOYEE_PROFILE:
                    {
                        connectionStringsKey = ConnectionStringsKey.EmployeeProfileConn;
                        break;
                    }
                case AppCode.SYSTEM:
                    {
                        connectionStringsKey = ConnectionStringsKey.SystemConn;
                        break;
                    }
            }
            var connection = _configService.GetConnectionString(connectionStringsKey);
            return connection;
    
        }
        public async Task<IDbConnection> GetConnectionAsync(Guid tenantID, string appCode)
        {
            var connectionString = await GetConnectionStringAsync(tenantID, appCode);
            IDbConnection sqlConn = new MySqlConnection(connectionString);
            return sqlConn;
        }
        public CommandDefinition BuildCommandDefinition(string sql, CommandType commandType, object param, IDbTransaction transaction)
        {
            var commandDefinition = new CommandDefinition(sql,param,transaction, commandType: commandType);
            return commandDefinition;
        }

        public async Task<PagingRespone> GetPagingDataUsingCommandTextAsync(Type modelType, Guid tenantID, string appCode, int pageIndex, int pageSize, WhereParameter filter, List<GridSortItem> sort, WhereParameter whereParameter = null, string columns = "")
        {
            var dicParam = new Dictionary<string, object>();
            List<Type> types = new List<Type>();
            string sql = BuildCommandTextGetDataPaging(modelType, types, tenantID, appCode, dicParam, pageIndex, pageSize, filter, sort, whereParameter, columns);

            var resultQuery  = await QueryMultipleUsingCommandText(tenantID, appCode, sql, types, dicParam);
            var res = new PagingRespone();
            if (resultQuery?.Count > 0)
            {
                 res = new PagingRespone(resultQuery[0].ToList(), int.Parse(resultQuery[1][0].ToString()));
            }
            return res;

        }

        public async Task<List<object>> GetAllDataUsingCommandTextAsync(Type modelType, Guid tenantID, string appCode, WhereParameter filter, List<GridSortItem> sort, WhereParameter whereParameter = null, string columns = "")
        {
            var dicParam = new Dictionary<string, object>();
            List<Type> types = new List<Type>();
            string sql = BuildCommandTextGetDataPaging(modelType, types, tenantID, appCode, dicParam, 0, 0, filter, sort, whereParameter, columns);

            var resultQuery = await QueryMultipleUsingCommandText(tenantID, appCode, sql, types, dicParam);
            if (resultQuery?.Count > 0)
            {
                return resultQuery[0].ToList();
            }
            return null;

        }
        public async Task<List<List<object>>> QueryMultipleUsingCommandText(Guid tenantID, string appCode,  string sql, List<Type> types, Dictionary<string,object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            try
            {
                var result = new List<List<object>>();
                CommandDefinition cd = BuildCommandDefinition(sql, CommandType.Text, param, transaction);
                var con = transaction != null ? transaction.Connection : connection;
                if (con != null)
                {
                    using var multi = await con.QueryMultipleAsync(cd);

                    int i = 0;
                    do
                    {
                        result.Add((await multi.ReadAsync(types[i])).ToList());
                        i++;
                    } while (!multi.IsConsumed);
                }
                if (con == null)
                {
                    using var conn = await GetConnectionAsync(tenantID, appCode);
                    using var multi = await conn.QueryMultipleAsync(cd);
                    int i = 0;
                    do
                    {
                        result.Add((await multi.ReadAsync(types[i])).ToList());
                        i++;
                    } while (!multi.IsConsumed);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<dynamic>> QueryUsingCommandText(Guid tenantID, string appCode, string sql, List<Type> types, Dictionary<string, object> param, IDbTransaction transaction = null, IDbConnection connection = null)
        {
            try
            {
                var result = new List<dynamic>();
                CommandDefinition cd = BuildCommandDefinition(sql, CommandType.Text, param, transaction);
                var con = transaction != null ? transaction.Connection : connection;
                if (con != null)
                {
                    result = (await con.QueryAsync(cd)).ToList();
                }
                if (con == null)
                {
                    using var conn = await GetConnectionAsync(tenantID, appCode);
                    result = (await con.QueryAsync(cd)).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagingRespone> GetPagingUsingCommandTextAsync(Type type, Guid tenantID, string appCode, int pageIndex, int pageSize, string filter, string customFilter, string sort, WhereParameter whereParameter = null, string columns = "")
        {
            //Xử lý filter và sort
            var filterWhere = GridFilterParser.Parse(DecodeBase64String(filter), DecodeBase64String(customFilter));
            List<GridSortItem> sortItem = Converter.Deserialize<List<GridSortItem>>(DecodeBase64String(sort));
            return await GetPagingDataUsingCommandTextAsync(type, tenantID, appCode, pageIndex, pageSize, filterWhere, sortItem, whereParameter, columns);
        }
        public string DecodeBase64String(string base64Text)
        {
            if (string.IsNullOrEmpty(base64Text))
            {
                return "";
            }
            return UTF8Encoding.UTF8.GetString(Convert.FromBase64String(base64Text));
        }
        public string BuildCommandTextGetDataPaging(Type type, List<Type> listType, Guid tenantID, string appCode, Dictionary<string, object>  dicParam, int pageIndex, int pageSize, WhereParameter filter, List<GridSortItem> sort, WhereParameter whereParameter = null, string columns = "", string viewOrTableName = "")
        {
            var sql = new StringBuilder("SELECT ");
            var sqlCountTotal = new StringBuilder("SELECT COUNT(1) ");

            if (!string.IsNullOrWhiteSpace(columns))
            {
                sql.Append(columns + " ");
                listType.Add(typeof(object));
            }
            else
            {
                sql.Append(" * ");
                listType.Add(type);
            }
            listType.Add(typeof(int));

            if (string.IsNullOrWhiteSpace(viewOrTableName))
            {
                viewOrTableName = type.GetTableName();
            }
            //Get table name
            sql.Append("FROM " + viewOrTableName + " WHERE ");
            sqlCountTotal.Append("FROM " + viewOrTableName + " WHERE ");

            if (filter != null)
            {
                filter.AddWhere(whereParameter);
            }
            else
            {
                filter = whereParameter;
            }

            if (filter != null)
            {
                sql.Append($" {filter.WhereClause} ");
                sqlCountTotal.Append($" {filter.WhereClause} ");
                foreach (var item in filter.WhereParam)
                {
                    if (dicParam.ContainsKey(item.Key))
                    {
                        dicParam[item.Key] = item.Value;
                    }
                    else
                    {
                        dicParam.Add(item.Key, item.Value);
                    }
                }
            }


            if (sort != null && sort.Count > 0)
            {
                sql.Append(" ORDER BY ");
                foreach(var item in sort)
                {
                    sql.Append($" {item.selector} {(item.desc ? " DESC " : "ASC")}");
                }
            }

            if (pageSize > 0 && pageIndex > 0)
            {
                sql.Append($" LIMIT {pageSize} OFFSET {(pageIndex - 1) * pageSize} ");
            }

            return sql.ToString() + ";" + sqlCountTotal.ToString();
        }

        public async Task<List<object>> GetAllUsingCommandTextAsync(Type typeModel, Guid tenantID, string appCode, string filter = "", string sort = "", string customFilter = "", WhereParameter whereParameter = null, string column = "", string viewOrTableName = "")
        {
            //Xử lý filter và sort
            var filterWhere = GridFilterParser.Parse(DecodeBase64String(filter), DecodeBase64String(customFilter));
            List<GridSortItem> sortItem = Converter.Deserialize<List<GridSortItem>>(DecodeBase64String(sort));
            return await GetAllDataUsingCommandTextAsync(typeModel, tenantID, appCode, filterWhere, sortItem, whereParameter, column);
        }
    }
}
                                                  