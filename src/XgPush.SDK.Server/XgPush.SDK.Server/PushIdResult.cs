using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class PushIdResult : BaseSerializeObject, IResultV2
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(Constants.push_id)]
        public uint PushId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            var p0 = jToken?[Constants.push_id];
            if (p0 != null && !p0.HasValues)
            {
                PushId = p0.Value<uint>();
            }
        }
    }
}