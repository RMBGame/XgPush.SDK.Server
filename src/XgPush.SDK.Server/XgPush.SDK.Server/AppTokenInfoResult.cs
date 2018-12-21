using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class AppTokenInfoResult : BaseSerializeObject<AppTokenInfoResult>, IResultV2
    {
        /// <summary>
        /// 1为token已注册，0为未注册
        /// </summary>
        [JsonProperty("isReg")]
        public int IsReg { get; set; }

        /// <summary>
        /// 最新活跃时间戳
        /// </summary>
        [JsonProperty("connTimestamp")]
        public long ConnTimestamp { get; set; }

        /// <summary>
        /// 该应用的离线消息数
        /// </summary>
        [JsonProperty("msgsNum")]
        public int MsgsNum { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            JToken isReg;
            isReg = jToken[nameof(isReg)];
            if (isReg != null && isReg.Type == JTokenType.Integer) IsReg = isReg.Value<int>();
            JToken connTimestamp;
            connTimestamp = jToken[nameof(connTimestamp)];
            if (connTimestamp != null && connTimestamp.Type == JTokenType.Integer) ConnTimestamp = connTimestamp.Value<long>();
            JToken msgsNum;
            msgsNum = jToken[nameof(msgsNum)];
            if (msgsNum != null && msgsNum.Type == JTokenType.Integer) MsgsNum = msgsNum.Value<int>();
        }
    }
}