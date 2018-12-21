using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class QueryDeviceCountResult : BaseSerializeObject<QueryDeviceCountResult>, IResultV2
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(Constants.device_num)]
        public int DeviceNum { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            var p = jToken[Constants.device_num];
            if (p != null && p.Type == JTokenType.Integer)
                DeviceNum = p.Value<int>();
        }
    }
}