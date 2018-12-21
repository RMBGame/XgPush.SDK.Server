using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class TokensResult : BaseSerializeObject<TokensResult>, IResultV2
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("tokens")]
        public string[] Tokens { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            var values = jToken[Constants.tokens];
            if (values != null && values.HasValues)
            {
                Tokens = values.Values<string>().ToArray();
            }
        }
    }
}