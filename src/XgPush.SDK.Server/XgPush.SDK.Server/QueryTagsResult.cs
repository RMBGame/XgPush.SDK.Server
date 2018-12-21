using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class QueryTagsResult : BaseSerializeObject, IResultV2
    {
        /// <summary>
        /// 依据limit参数查询出的标签数组
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            var values = jToken[Constants.tags];
            if (values != null && values.HasValues)
            {
                Tags = values.Values<string>().ToArray();
            }
        }
    }
}