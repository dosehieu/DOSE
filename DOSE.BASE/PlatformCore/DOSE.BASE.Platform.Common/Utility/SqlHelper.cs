using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Utility
{
    public class SqlHelper
    {

        public static object ConvertToDBParameters(string filter, Dictionary<string, object> param)
        {
            var paramSql = new Dictionary<string, object>();

            var filterArr = Converter.Deserialize<List<List<object>>>(filter);
            for (int i = 1; i < filterArr.Count; i++)
            {
                var item = filterArr[i];
                //Check sql injnection
                paramSql.Add($"@{item[0]}", item[2]);
            }
            paramSql = paramSql.Concat(param).ToLookup(x => x.Key, x => x.Value).ToDictionary(x => x.Key, g => g.First());
            var json = JsonConvert.SerializeObject(paramSql, Newtonsoft.Json.Formatting.Indented);
            var myobject = JsonConvert.DeserializeObject(json);
            return myobject;
        }

    }
}
