using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class QueryTotalTagsResult : QueryTagsResult
    {
        /// <summary>
        /// 指定应用的总tag数
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public override void Init(JToken jToken)
        {
            base.Init(jToken);
            var len = jToken[Constants.total];
            if (len != null && len.Type == JTokenType.Integer)
            {
                Total = len.Value<int>();
            }
        }
    }
}