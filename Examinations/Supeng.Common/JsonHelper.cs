using Newtonsoft.Json;

namespace Supeng.Common
{
    public static class JsonHelper
    {
        public static string ObjectToString(this object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T StringToObject<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static TD CloneDataToTarget<TS, TD>(this TS source)
        {
            return StringToObject<TD>(ObjectToString(source));
        }
    }
}