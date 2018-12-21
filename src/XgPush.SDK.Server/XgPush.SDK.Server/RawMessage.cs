using System;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public sealed class RawMessage : MessageBase, IToDictionaryV3
    {
        /// <summary>
        ///
        /// </summary>
        [Obsolete("", true)]
        public RawMessage() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="raw"></param>
        public RawMessage(string raw)
        {
            Raw = raw;
        }

        /// <summary>
        ///
        /// </summary>
        protected override bool IsValid => true;
    }
}