using Newtonsoft.Json;

namespace eKYC.Common.Helpers
{
    public static class JsonHelper
    {
        public static string ToJSONString<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static T FromJSONString<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
