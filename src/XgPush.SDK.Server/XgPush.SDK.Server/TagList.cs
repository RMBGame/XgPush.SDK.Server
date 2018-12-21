using Newtonsoft.Json;
using System.Collections.Generic;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// (V3) Push API (标签推送)参数。
    /// </summary>
    public class TagList : BaseSerializeObject<TagList>
    {
        /// <summary>
        ///
        /// </summary>
        public TagList() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tags"></param>
        public TagList(IEnumerable<string> tags)
        {
            Tags = tags;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tags"></param>
        public TagList(params string[] tags) : this((IEnumerable<string>)tags)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="operator"></param>
        public TagList(IEnumerable<string> tags, Operator @operator) : this(tags)
        {
            Operators = @operator;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="tags"></param>
        public TagList(Operator @operator, IEnumerable<string> tags) : this(tags, @operator) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="tags"></param>
        public TagList(Operator @operator, params string[] tags) : this(@operator, (IEnumerable<string>)tags) { }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("op")]
        public Operator Operators { get; set; }
    }
}