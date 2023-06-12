using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common.Attribute
{
    public static class TypeExtension
    {
        public static string GetPrimaryKeyName(this Type type)
        {
            PropertyInfo properties = type.GetProperties().FirstOrDefault(n=> n.GetCustomAttribute(typeof(KeyAttribute)) != null);
            if(properties != null)
            {
                return properties.Name;
            }
            return null;
        }

        public static string GetForeignKeyName(this Type type)
        {
            PropertyInfo properties = type.GetProperties().FirstOrDefault(n => n.GetCustomAttribute(typeof(ForeignKeyAttribute)) != null);
            if (properties != null)
            {
                return properties.Name;
            }
            return null;
        }

        public static string GetTableName(this Type type)
        {
            var attribute = type.GetCustomAttributes(false).ToDictionary(a => a.GetType().Name, a => a).FirstOrDefault(n => n.Key == "ConfigTableAttribute");
            dynamic configTable = attribute.Value;
            return configTable.ConfigTable;
        }
    }
}
