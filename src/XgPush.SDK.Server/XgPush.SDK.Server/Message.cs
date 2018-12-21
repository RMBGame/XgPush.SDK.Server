using System.Collections.Generic;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TCustom"></typeparam>
    public class Message<TCustom> : MessageBase<TCustom>.Android
    {
        /// <summary>
        ///
        /// </summary>
        public const MessageType TYPE_NOTIFICATION = MessageType.Notification;

        /// <summary>
        ///
        /// </summary>
        public const MessageType TYPE_MESSAGE = MessageType.Message;
    }

    /// <summary>
    ///
    /// </summary>
    public class Message : Message<IDictionary<string, object>>
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
        /// <param name="class"></param>
        /// <returns></returns>
        public static bool isValid(this IsValid @class) => @class.IsValid();

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        public static Message setCustom(this Message message, IDictionary<string, object> custom)
        {
            return message.setCustom<Message, IDictionary<string, object>>(custom);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static Message setTitle(this Message message, string title)
        {
            message.Title = title;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Message setContent(this Message message, string content)
        {
            message.Content = content;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="multiPkg"></param>
        /// <returns></returns>
        public static Message setMultiPkg(this Message message, DigitBoolean multiPkg)
        {
            message.MultiPkg = multiPkg;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int getMultiPkg(this Message message)
        {
            return message.MultiPkg;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static Message setStyle(this Message message, Style style)
        {
            message.Style = style;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Message setAction(this Message message, ClickAction action)
        {
            message.Action = action;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string toJson(this Message message) => message.ToString();
    }

#pragma warning restore IDE1006 // 命名样式
}