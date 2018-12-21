using Newtonsoft.Json;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// (通用基础返回值) http://docs.developer.qq.com/xg/push_faq/server_api/rest_api_v3.html#%E9%80%9A%E7%94%A8%E5%9F%BA%E7%A1%80%E8%BF%94%E5%9B%9E%E5%80%BC
    /// </summary>
    public class XingePushClientResultV3 : BaseSerializeObject<XingePushClientResultV3>
    {
        /// <summary>
        /// 与请求包一致(如果请求包是非法json 该字段为0)。
        /// <para>类型：int64</para>
        /// <para>必需：是</para>
        /// </summary>
        [JsonProperty("seq")]
        public long Seq { get; set; }

        /// <summary>
        /// 错误码。
        /// <para>类型：int</para>
        /// <para>必需：是</para>
        /// </summary>
        [JsonProperty("ret_code")]
        public XingePushClientResultCodeV3 ResultCode { get; set; }

        /// <summary>
        /// 结果描述。
        /// <para>类型：string</para>
        /// <para>必需：否</para>
        /// </summary>
        [JsonProperty("err_msg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 请求正确时，若有额外数据要返回，则结果封装在该字段的json中，若无额外数据，则可能无此字段。
        /// <para>类型：string</para>
        /// <para>必需：否</para>
        /// </summary>
        [JsonProperty("result")]
        public string Result { get; set; }

        /// <summary>
        /// (Push API应答参数) https://xg.qq.com/docs/server_api/v3/push_api_v3.html#push-api%E5%BA%94%E7%AD%94%E5%8F%82%E6%95%B0
        /// </summary>
        public class Push : XingePushClientResultV3
        {
            /// <summary>
            /// 推送id。
            /// <para>类型：string</para>
            /// <para>必需：是</para>
            /// </summary>
            [JsonProperty("push_id")]
            public string PushId { get; set; }

            /// <summary>
            /// 用户指定推送环境，仅支持iOS。
            /// <para>类型：string</para>
            /// <para>必需：是</para>
            /// </summary>
            [JsonProperty("environment")]
            public iOSEnvironmentV3 Environment { get; set; }
        }

        /// <summary>
        /// (Tag API应答参数) https://xg.qq.com/docs/server_api/v3/tag_api_v3.html
        /// </summary>
        public class Tag : XingePushClientResultV3
        {
        }
    }
}