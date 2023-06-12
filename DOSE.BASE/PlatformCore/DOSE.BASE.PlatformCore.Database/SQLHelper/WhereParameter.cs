using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Database.SQLHelper
{
    public class WhereParameter
    {
        public string WhereClause { get; set; }
        public Dictionary<string,object> WhereParam { get; set; }
        public WhereParameter(string whereClause, Dictionary<string, object> whereParam)
        {
            this.WhereClause = whereClause;
            this.WhereParam = whereParam;
        }
        public WhereParameter()
        {
        }
        public void AddWhere(string whereClause, Dictionary<string, object> whereParams)
        {
            if (string.IsNullOrWhiteSpace(whereClause))
            {
                return;
            }
            this.WhereClause = $"{this.WhereClause} AND ({whereClause})";
            if (whereParams != null && whereParams.Count > 0)
            {
                foreach (var whereParam in whereParams)
                {
                    if (!this.WhereParam.ContainsKey(whereParam.Key))
                    {
                        this.WhereParam.Add(whereParam.Key, whereParam.Value);
                    }
                    else
                    {
                        this.WhereParam[whereParam.Key] = whereParam.Value;
                    }
                }
            }
        }
        public void AddWhere(WhereParameter whereParameter)
        {
            if(whereParameter!= null)
            {
                AddWhere(whereParameter.WhereClause, whereParameter.WhereParam);
            }
        }
    }
}
