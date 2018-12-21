using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class StatusResult : BaseSerializeObject<StatusResult>, IResultV2, IsSuccess
    {
        /// <summary>
        /// 0为成功，其余为失败
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsSuccess() => Status == 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            JToken status;
            status = jToken[nameof(status)];
            if (status != null && status.Type == JTokenType.Integer)
            {
                Status = status.Value<int>();
            }
        }
    }
}