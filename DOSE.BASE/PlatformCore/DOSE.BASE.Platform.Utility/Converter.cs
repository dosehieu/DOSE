using DOSE.BASE.PlatformCore.Model.Base;
using DOSE.BASE.PlatformCore.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace DOSE.BASE.Platform.Utility
{
    public static class Converter
    {
        public static object DeserializeObject(string obj, Type currentModelType)
        {
            return JsonConvert.DeserializeObject(obj, currentModelType);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T Deserialize<T>(string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
        public static Dictionary<string, object> ToDatabaseParam(BaseModel obj, string predict = "p_")
        {
            var res = new Dictionary<string, object>();
            AddOrUpdateParam(ref res, obj, predict);
            return res;
        }
        public static void AddOrUpdateParam(ref Dictionary<string, object> res, BaseModel obj, string predict)
        {
            foreach (PropertyInfo item in obj.GetType().GetProperties().Where(n => n.CanRead == true))
            {
                if (item.Name != "generic" && item.Name != "list")
                {
                    res.AddOrUpdateKey(predict + item.Name, item.GetValue(obj));
                }
            }
        }
        public static void AddOrUpdateKey(this Dictionary<string, object> res, string name, object value)
        {
            if (res.ContainsKey(name))
            {
                res[name] = value;
            }
            else
            {
                res.Add(name, value);
            }
        }
    }
}
