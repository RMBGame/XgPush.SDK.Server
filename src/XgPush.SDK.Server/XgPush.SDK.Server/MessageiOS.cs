using System.Collections.Generic;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TCustom"></typeparam>
    public class MessageiOS<TCustom> : MessageBase<TCustom>.iOS
    {
        /// <summary>
        ///
        /// </summary>
        public const MessageType TYPE_APNS_NOTIFICATION = MessageType.Notification;
    }

    /// <summary>
    ///
    /// </summary>
    public class MessageiOS : MessageiOS<IDictionary<string, object>>
    {
    }
}

namespace XgPush.SDK.Server.Compat
{
#pragma warning disable IDE1006 // 命名样式

    public static partial class XgPush_Server_SDK_Compat_Extensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessageiOS"></typeparam>
        /// <param name="message"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        public static TMessageiOS setCustom<TMessageiOS>(this TMessageiOS message,
            IDictionary<string, object> custom)
            where TMessageiOS : MessageBase<IDictionary<string, object>>.iOS
        {
            return message.setCustom<TMessageiOS, IDictionary<string, object>>(custom);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessageiOS"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string toJson<TMessageiOS>(this TMessageiOS message)
            where TMessageiOS : MessageBase<IDictionary<string, object>>.iOS
        {
            return message.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessageiOS"></typeparam>
        /// <param name="message"></param>
        /// <param name="alert"></param>
        /// <returns></returns>
        public static TMessageiOS setAlert<TMessageiOS>(this TMessageiOS message,
            string alert) where TMessageiOS : MessageBase<IDictionary<string, object>>.iOS
        {
            message.AlertStr = alert;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessageiOS"></typeparam>
        /// <param name="message"></param>
        /// <param name="badge"></param>
        /// <returns></returns>
        public static TMessageiOS setBadge<TMessageiOS>(this TMessageiOS message,
            int badge) where TMessageiOS : MessageBase<IDictionary<string, object>>.iOS
        {
            message.Badge = badge;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessageiOS"></typeparam>
        /// <param name="message"></param>
        /// <param name="sound"></param>
        /// <returns></returns>
        public static TMessageiOS setSound<TMessageiOS>(this TMessageiOS message,
            string sound) where TMessageiOS : MessageBase<IDictionary<string, object>>.iOS
        {
            message.Sound = sound;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessageiOS"></typeparam>
        /// <param name="message"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static TMessageiOS setCategory<TMessageiOS>(this TMessageiOS message,
            string category) where TMessageiOS : MessageBase<IDictionary<string, object>>.iOS
        {
            message.Category = category;
            return message;
        }
    }

#pragma warning restore IDE1006 // 命名样式
}