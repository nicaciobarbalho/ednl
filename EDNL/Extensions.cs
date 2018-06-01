using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EDNL
{
    public static class Extensions
    {
        public static T Clone<T>(this T value)
        {
            string json = JsonConvert.SerializeObject(value);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
