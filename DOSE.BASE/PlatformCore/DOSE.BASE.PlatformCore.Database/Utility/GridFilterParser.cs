using DOSE.BASE.PlatformCore.Common.Utility;
using DOSE.BASE.PlatformCore.Database.SQLHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.Database.Utility
{
    public class GridFilterParser
    {
        public static WhereParameter Parse( string param1, string param2 = "")
        {
            var input1 = ParseFilterWhere(param1);
            var input2 = ParseFilterWhere(param2, "WP");
            if(input2 != null)
            {
                input1.AddWhere(input2);
            }
            return input1;
        }
        public static WhereParameter ParseFilterWhere(string input, string suffixParamName = "WP")
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }
            var builder = new StringBuilder("(");
            var dicParam = new Dictionary<string, object>();
            JArray obj = Converter.Deserialize<JArray>(input);
            ConvertArrayRecursive(obj, builder, dicParam, suffixParamName);
            builder.Append(")");
            var whereParameter = new WhereParameter(builder.ToString(), dicParam);
            return whereParameter;
        }
        public static void ConvertArrayRecursive(JArray array, StringBuilder builder, Dictionary<string, object> dicParam, string suffixParamName = "WP")
        {
            if(array.First != null && array.First.Type == JTokenType.Array)
            {
                foreach (JToken item in array)
                {
                    if (item.Type == JTokenType.Array)
                    {
                        builder.Append(" (");
                        ConvertArrayRecursive((JArray)item, builder, dicParam, suffixParamName);
                        builder.Append(") ");
                    }
                    else
                    {
                        if (string.Compare(item.Value<string>(), "and", true) == 0)
                        {
                            builder.Append(" AND ");
                        }
                        else if (string.Compare(item.Value<string>(), "or", true) == 0)
                        {
                            builder.Append(" OR ");
                        }
                    }
                }
            }
            else
            {
                builder.Append(" (");
                builder.Append(ConvertFilterItem(array, dicParam, suffixParamName));
                builder.Append(") ");
            }
        }
        public static string ConvertFilterItem(JArray array, Dictionary<string, object> dicParam, string suffixParamName = "WP")
        {
            var propName = array.First.Value<string>();
            var operation = array.First.Next.Value<string>();
            var paramName = $"@p{dicParam.Count + 1}{suffixParamName}";
            string paramNameAlias = paramName;
            string operationAlias = operation;
            string commonPattern = " `{0}` {1} {2}";
            var patternParam = new List<object>();
            object paramValue;
            //Check value
            switch (array.Last.Type)
            {
                case JTokenType.Date:
                    {
                        paramValue = array.Last.Value<DateTime>();
                        break;
                    }
                case JTokenType.Integer:
                    {
                        paramValue = array.Last.Value<int>();
                        break;
                    }
                default:
                    {
                        paramValue = array.Last.Value<object>();
                        break;
                    }
            }

            //Parse sql
            switch (operation.ToLower())
            {
                case "contains":
                    {
                        operationAlias = " LIKE ";
                        paramNameAlias = $" CONCAT('%', {paramName}, '%') ";
                        break;
                    }
                case "notcontains":
                case "startswith":
                case "endswith":
                case "in":
                case "notin":
                case "isnullorempty":
                case "isnull":
                case "notnull":
                case "hasvalue":
                case "ft":
                case "notft":
                case "<>":
                    {
                        break;
                    }
            }
            dicParam.Add(paramName, paramValue);
            if(patternParam.Count == 0)
            {
                patternParam.AddRange(new object[] { propName, operationAlias, paramNameAlias });
            }
            string result = string.Format(commonPattern, patternParam.ToArray());
            return result;

        }
    }
}
