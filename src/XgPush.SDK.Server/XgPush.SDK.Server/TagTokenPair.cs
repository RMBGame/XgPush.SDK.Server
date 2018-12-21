using JetBrains.Annotations;
using Newtonsoft.Json;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class TagTokenPair : BaseSerializeObject<TagTokenPair>, ITagTokenPair
    {
#pragma warning disable CS0618 // 类型或成员已过时

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TagTokenPair(Compat.TagTokenPair value)
            => new TagTokenPair(value.tag, value.token);

#pragma warning restore CS0618 // 类型或成员已过时

        /// <summary>
        ///
        /// </summary>
        public TagTokenPair() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="token"></param>
        public TagTokenPair([NotNull]string tag, [NotNull]string token)
        {
            Tag = tag;
            Token = token;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual string[] ToArray() => InternalExtensions.ToArrayInternal(this);

        /// <summary>
        ///
        /// </summary>
        [NotNull]
        [JsonProperty(Constants.tag)]
        public string Tag { get; set; }

        /// <summary>
        ///
        /// </summary>
        [NotNull]
        [JsonProperty(Constants.token)]
        public string Token { get; set; }
    }
}