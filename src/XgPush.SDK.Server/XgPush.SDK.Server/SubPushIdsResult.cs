using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class SubPushIdsResult : PushIdResult
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(Constants.sub_push_ids)]
        public ICollection<uint> SubPushIds { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public bool HasSubPushIds => SubPushIds != null && SubPushIds.Any();

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public override void Init(JToken jToken)
        {
            base.Init(jToken);
            var p1 = jToken?[Constants.sub_push_ids];
            if (p1 != null && p1.HasValues)
            {
                SubPushIds = p1.Values<uint>().ToArray();
            }
        }
    }
}