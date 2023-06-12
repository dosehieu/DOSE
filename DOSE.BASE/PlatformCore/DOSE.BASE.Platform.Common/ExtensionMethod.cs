using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DOSE.BASE.PlatformCore.Common
{
    public static class ExtensionMethod
    {
        public static T ToObject<T>(this byte[] bytes)
        {
            if(bytes == null || bytes.Length == 0)
            {
                return default(T);
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream(bytes))
            {
                return (T)bf.Deserialize(ms);
            }
        }
    }
}
