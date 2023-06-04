using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace eKYC.Common.Helpers;

public static class ObjectHelper
{
    public static byte[] GetByteArrayFromObject(object value)
    {
        return Encoding.UTF8.GetBytes(JsonHelper.ToJSONString(value));
    }

    public static object GetObjectFromByteArray(byte[] value)
    {
        return JsonHelper.FromJSONString<object>(Encoding.UTF8.GetString(value));
    }

    public static T GetObjectFromByteArray<T>(byte[] value)
    {
        return JsonHelper.FromJSONString<T>(Encoding.UTF8.GetString(value));
    }

    public static T DeepClone<T>(this T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }
}
