using System;
using System.Collections.Generic;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.Compat
{
    /// <summary>
    ///
    /// </summary>
    [Obsolete("MessageIOS is deprecated, please use MessageiOS instead.", true)]
    public sealed class MessageIOS : MessageBase<IDictionary<string, object>>.iOS
    {
        /// <summary>
        ///
        /// </summary>
        public const MessageType TYPE_APNS_NOTIFICATION = MessageType.Notification;

        //public const int TYPE_REMOTE_NOTIFICATION = 12;
    }
}