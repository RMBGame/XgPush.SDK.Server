using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.Compat
{
    /// <summary>
    ///
    /// </summary>
    [Obsolete(ObsoleteMessage)]
    public sealed class TagTokenPair : BaseSerializeObject<TagTokenPair>, ITagTokenPair
    {
        private const string ObsoleteMessage =
            "XgPush.SDK.Server.Compat.TagTokenPair is deprecated, " +
            "please use XgPush.SDK.Server.TagTokenPair instead.";

#pragma warning disable IDE1006 // 命名样式

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
            this.tag = tag;
            this.token = token;
        }

        /// <summary>
        ///
        /// </summary>
        [NotNull]
        [JsonProperty(Constants.tag)]
        public string tag { get; set; }

        /// <summary>
        ///
        /// </summary>
        [NotNull]
        [JsonProperty(Constants.token)]
        public string token { get; set; }

#pragma warning restore IDE1006 // 命名样式

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public string[] ToArray() => InternalExtensions.ToArrayInternal(this);

        string ITagTokenPair.Tag
        {
            get => tag;
            set => tag = value;
        }

        string ITagTokenPair.Token
        {
            get => token;
            set => token = value;
        }
    }
}