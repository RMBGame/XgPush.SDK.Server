using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        ///
        /// </summary>
        public static Func<object, string> SerializeDelegate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public static Func<string, Type, object> DeserializeDelegate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public static JsonSerializerSettings DefaultJsonSerializerSettings { get; set; }
            = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            if (obj is IRaw raw)
            {
                if (!string.IsNullOrWhiteSpace(raw.Raw))
                    return raw.Raw;
            }

            if (obj is IToDictionaryV3 todict_v3)
            {
                return Serialize(todict_v3.ToDictionaryV3());
            }

            if (obj is IDictionarySerialize dictionarySerialize)
            {
                return Serialize(dictionarySerialize.ToDictionarySerialize());
            }

            if (SerializeDelegate != null)
            {
                return SerializeDelegate(obj);
            }
            else
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, DefaultJsonSerializerSettings);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonString) where T : new()
        {
            return DeserializeInternal<T>(jsonString);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        internal static T DeserializeInternal<T>(string jsonString)
        {
            var type = typeof(T);

            if (typeof(IResultV2).IsAssignableFrom(type))
            {
                var result = (T)Activator.CreateInstance(type);
                ((IResultV2)result).Init(GetJObject());
                return result;
            }

            if (typeof(IXingePushClientResultV2).IsAssignableFrom(type))
            {
                var result = (T)Activator.CreateInstance(type);
                ((IXingePushClientResultV2)result).Init(GetJObject());
                return result;
            }

            if (typeof(IRaw).IsAssignableFrom(type))
            {
                var result = (T)Activator.CreateInstance(type);
                ((IRaw)result).Raw = jsonString;
                return result;
            }

            if (DeserializeDelegate != null)
            {
                return (T)DeserializeDelegate(jsonString, type);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(jsonString, DefaultJsonSerializerSettings);
            }

            JObject GetJObject() => DeserializeInternal<JObject>(jsonString);
        }
    }

    /// <summary>
    ///
    /// </summary>
    public interface IRaw
    {
        /// <summary>
        /// 获取或设置消息JSON字符串。
        /// </summary>
        string Raw { get; set; }
    }
}

public static partial class XgPush_Server_SDK_GlobalExtensions
{
    internal static string Serialize(this object obj)
    {
        return JsonHelper.Serialize(obj);
    }

    internal static T Deserialize<T>(this string jsonString) where T : new()
    {
        return JsonHelper.DeserializeInternal<T>(jsonString);
    }
}