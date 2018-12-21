using Newtonsoft.Json;
using System;
using System.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// 推送目标。
    /// </summary>
    [JsonConverter(typeof(AudienceTypeConverter))]
    public enum AudienceType : byte
    {
        /// <summary>
        /// 全量推送
        /// </summary>
        All = (byte)sbyte.MaxValue,

        /// <summary>
        /// 标签推送
        /// </summary>
        Tag,

        /// <summary>
        /// 单设备推送
        /// </summary>
        Token,

        /// <summary>
        /// 设备列表推送
        /// </summary>
        TokenList,

        /// <summary>
        /// 单账号推送
        /// </summary>
        Account,

        /// <summary>
        /// 账号列表推送
        /// </summary>
        AccountList,
    }
}

namespace XgPush.SDK.Server.Internal
{
    internal sealed class AudienceTypeConverter : Converter<AudienceType?>
    {
        public override void WriteJson(JsonWriter writer, AudienceType? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                writer.WriteValue(value.Value.GetString());
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override AudienceType? ReadJson(JsonReader reader, Type objectType,
            object existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var readerValue = reader.Value;
            if (readerValue == null)
            {
                return null;
            }
            else if (readerValue is string value)
            {
                return XgPush_Server_SDK_GlobalExtensions.mDefineds_AudienceType
                    .FirstOrDefault(x => x.Value == value).Key;
            }
            else
            {
                return default(AudienceType);
            }
        }
    }
}