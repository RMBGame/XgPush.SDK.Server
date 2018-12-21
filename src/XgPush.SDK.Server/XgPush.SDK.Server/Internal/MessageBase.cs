using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Compat;
using XgPush.SDK.Server.Internal;
using static XgPush.SDK.Server.Internal.Constants;

namespace XgPush.SDK.Server.Internal
{
    #region 字段/属性

    /// <summary>
    /// 消息模型(抽象基类)。
    /// </summary>
    public abstract partial class MessageBase : IRaw
    {
        /// <summary>
        /// 消息离线存储时间（单位为秒），最长存储时间3天。
        /// <para>
        /// 若设置为0，则默认值（3天）
        /// </para>
        /// <para>
        /// 建议取值区间[800, 86400x3]
        /// </para>
        /// </summary>
        public Second ExpireTime { get; set; } = Second.ThreeDays;

        /// <summary>
        /// <para>
        /// 1.指定推送时间,格式为yyyy-MM-DD HH:MM:SS(<see cref="NullableDateTime.Format"/>)
        /// </para>
        /// <para>
        /// 2.若小于服务器当前时间，则会立即推送
        /// </para>
        /// <para>
        /// 3.仅全量推送和标签群推支持此字段
        /// </para>
        /// </summary>
        public NullableDateTime SendTime { get; set; } = DefaultMessageSendTime;

        /// <summary>
        /// <see cref="MessageType"/> 消息类型(iOS平台用，必须为0，不区分通知栏消息和静默消息)。
        /// </summary>
        public MessageType Type { get; set; } = MessageType.Notification;

        /// <summary>
        /// 循环执行消息下发的间隔，以天为单位，取值[1, 14]。loop_times和loop_interval一起表示消息下发任务的循环规则，不可超过14天。
        /// </summary>
        public int LoopInterval { get; set; } = -1;

        /// <summary>
        /// 消息将在哪些时间段允许推送给用户，建议小于10个。
        /// </summary>
        public ICollection<TimeInterval> AcceptTimes { get; set; } = new List<TimeInterval>();

        /// <summary>
        /// 循环执行消息下发的次数，建议取值[1, 15]。
        /// </summary>
        public int LoopTimes { get; set; } = -1;

        /// <summary>
        /// 获取或设置消息JSON字符串。
        /// </summary>
        public string Raw { get; set; }
    }

    #endregion

    #region 函数

    public partial class MessageBase
    {
        /// <summary>
        /// 添加允许推送给用户的时间段。
        /// </summary>
        /// <param name="acceptTime"></param>
        public virtual void AddAcceptTime(TimeInterval acceptTime)
        {
            if (AcceptTimes.IsReadOnly) AcceptTimes = new List<TimeInterval>(AcceptTimes);
            AcceptTimes.Add(acceptTime);
        }
    }

    #endregion

    #region 接口实现与抽象

    public partial class MessageBase : IsValid,
        IToDictionary,
        IToDictionaryV3
    {
        /// <summary>
        /// 当前模型是否通过验证。
        /// </summary>
        protected abstract bool IsValid { get; }

        bool IsValid.IsValid() => IsValid;

        /// <summary>
        ///
        /// </summary>
        protected virtual Dictionary<string, object> KeyValuePairs
        {
            get
            {
                var param = new Dictionary<string, object>
                {
                    { expire_time, ExpireTime },
                    { send_time, SendTime },
                    { message_type, Type },
                    { message, ToString() },
                };

                if (LoopInterval > 0 && LoopTimes > 0)
                {
                    param.Add(loop_interval, LoopInterval);
                    param.Add(loop_times, LoopTimes);
                }

                return param;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary() => KeyValuePairs;

        /// <summary>
        ///
        /// </summary>
        protected virtual Dictionary<string, object> KeyValuePairsV3
        {
            get
            {
                var param = new Dictionary<string, object>();
                if (ExpireTime < Second.ThreeDays && ExpireTime != 0)
                    param.Add(expire_time, ExpireTime);
                if (SendTime.HasValue)
                    param.Add(send_time, SendTime);
                if (LoopInterval > 0 && LoopInterval <= 14 && LoopTimes > 0)
                {
                    param.Add(loop_interval, LoopInterval);
                    param.Add(loop_times, LoopTimes);
                }
                return param;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionaryV3() => KeyValuePairsV3;
    }

    #endregion

    #region 泛型版本

    /// <summary>
    /// 消息(自定义)(泛型抽象基类)。
    /// </summary>
    /// <typeparam name="TCustom"></typeparam>
    public abstract class MessageBase<TCustom> : MessageBase
    {
        /// <summary>
        /// 自定义消息。
        /// </summary>
        public TCustom Custom { get; set; }

        #endregion

        #region iOS平台消息

#pragma warning disable IDE1006 // 命名样式

        /// <summary>
        /// iOS平台消息(泛型抽象基类)。
        /// </summary>
        public abstract class iOS : MessageBase<TCustom>, IPlatform
        {
            Platform IPlatform.Platform => Platform.iOS;

            /// <summary>
            /// iOS环境设置，值为 <see langword="null"/> 则采用 <see cref="XingePushClient.iOSEnvironment"/> 中的值，不为null则使用设置的值，默认值 <see langword="null"/> 。
            /// </summary>
            public iOSEnvironment? Environment { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string AlertStr { get; set; }

            /// <summary>
            ///
            /// </summary>
            public int Badge { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string Sound { get; set; } = beep_wav;

            /// <summary>
            ///
            /// </summary>
            public string Category { get; set; }

            /// <summary>
            ///
            /// </summary>
            protected override bool IsValid
            {
                get
                {
                    if (!string.IsNullOrWhiteSpace(Raw))
                        return true;

                    if (ExpireTime < 0 || ExpireTime > Second.ThreeDays)
                        return false;

                    if (AcceptTimes.Any(ti => !ti.IsValid()))
                    {
                        return false;
                    }

                    if (LoopInterval > 0 && LoopTimes > 0 && (LoopTimes - 1) * LoopInterval + 1 > 15)
                    {
                        return false;
                    }

                    return true;
                }
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                if (!string.IsNullOrWhiteSpace(Raw))
                    return Raw;

                var dict = new Dictionary<string, object>();

                if (Type == MessageType.Notification)
                {
                    dict.Add(alert, AlertStr);
                    if (Badge != 0)
                    {
                        dict.Add(badge, Badge);
                    }
                    if (Sound.Length != 0)
                    {
                        dict.Add(sound, Sound);
                    }
                    if (Category.Length != 0)
                    {
                        dict.Add(category, Category);
                    }
                }
                else if (Type == MessageType.Message)
                {
                    dict.Add("content-available", 1);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Type));
                }

                dict = new Dictionary<string, object>
            {
                { accept_time, AcceptTimes },
                { aps, dict },
                { "custom", Custom }
            };

                return dict.Serialize();
            }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairs
            {
                get
                {
                    var param = base.KeyValuePairs;
                    param.Add("message", ToString());

                    if (Environment.HasValue)
                        param.Add(environment, Environment.Value);

                    return param;
                }
            }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    var msg = new Dictionary<string, object> { { accept_time, AcceptTimes } };
                    var ios_dict = new Dictionary<string, object> { { "custom", Custom } };
                    switch (Type)
                    {
                        case MessageType.Notification:
                            var dict = new Dictionary<string, object>
                        {
                            { alert, AlertStr }
                        };
                            if (Badge != 0) dict.Add(badge, Badge);
                            if (Sound.Length != 0) dict.Add(sound, Sound);
                            if (Category.Length != 0) dict.Add(category, Category);
                            ios_dict.Add(aps, dict);
                            break;

                        case MessageType.Message:
                            ios_dict.Add("content-available", 1);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(Type));
                    }
                    msg.Add("ios", ios_dict);
                    param.Add(message, msg);

                    if (Environment.HasValue)
                        param.Add(environment, ((iOSEnvironmentV3)Environment.Value).ToString());

                    return param;
                }
            }
        }

#pragma warning restore IDE1006 // 命名样式

        #endregion

        #region Android平台消息

        /// <summary>
        /// Android平台消息(泛型抽象基类)。
        /// </summary>
        /// <typeparam name="TCustom"></typeparam>
        public abstract class Android : MessageBase<TCustom>, IPlatform
        {
            Platform IPlatform.Platform => Platform.Android;

            /// <summary>
            /// 消息标题。
            /// </summary>
            public string Title { get; set; } = "";

            /// <summary>
            /// 消息内容。
            /// </summary>
            public string Content { get; set; } = "";

            /// <summary>
            /// 多包名推送。
            /// <para>
            /// 0，按注册时提供的包名分发消息
            /// </para>
            /// <para>
            /// 1，忽略包名，按access id分发消息
            /// </para>
            /// </summary>
            public DigitBoolean MultiPkg { get; set; }

            /// <summary>
            ///
            /// </summary>
            public Style Style { get; set; } = new Style();

            /// <summary>
            ///
            /// </summary>
            public ClickAction Action { get; set; } = new ClickAction();

            /// <summary>
            ///
            /// </summary>
            protected override bool IsValid
            {
                get
                {
                    if (!string.IsNullOrWhiteSpace(Raw))
                        return true;
                    if (!Type.IsDefined())
                        return false;
                    if (Type == MessageType.Notification)
                    {
                        if (Style != null)
                            if (!Style.IsValid()) return false;
                        if (!Action.IsValid()) return false;
                    }
                    if (ExpireTime > Second.ThreeDays)
                        return false;
                    foreach (var ti in AcceptTimes) if (!ti.IsValid()) return false;
                    if (LoopInterval > 0 && LoopTimes > 0 && (LoopTimes - 1) * LoopInterval + 1 > 15) return false;
                    return true;
                }
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                if (!string.IsNullOrWhiteSpace(Raw))
                    return Raw;

                var dict = new Dictionary<string, object>
            {
                { title, Title },
                { content, Content },
                { accept_time, AcceptTimes },
                { custom_content, Custom }
            };

                if (Type == MessageType.Notification)
                {
                    if (Style != null)
                    {
                        dict.Add(builder_id, Style.BuilderId);
                        dict.Add(ring, Style.Ring);
                        dict.Add(vibrate, Style.Vibrate);
                        dict.Add(clearable, Style.Clearable);
                        dict.Add(n_id, Style.NId);
                        dict.Add(ring_raw, Style.RingRaw);
                        dict.Add(lights, Style.Lights);
                        dict.Add(icon_type, Style.IconType);
                        dict.Add(icon_res, Style.IconRes);
                        dict.Add(style_id, Style.StyleId);
                        dict.Add(small_icon, Style.SmallIcon);
                    }

                    dict.Add(action, Action.toJson());
                }

                return dict.Serialize();
            }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairs
            {
                get
                {
                    var param = base.KeyValuePairs;
                    param.Add("message", ToString());
                    param.Add(multi_pkg, MultiPkg);
                    return param;
                }
            }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    var msg = new Dictionary<string, object>
                {
                    { title, Title },
                    { content, Content },
                    { accept_time, AcceptTimes }
                };
                    var android_dict = new Dictionary<string, object> { { custom_content, Custom } };
                    switch (Type)
                    {
                        case MessageType.Notification:
                            android_dict.Add(n_id, Style.NId);
                            android_dict.Add(builder_id, Style.BuilderId);
                            android_dict.Add(ring, Style.Ring);
                            android_dict.Add(ring_raw, Style.RingRaw);
                            android_dict.Add(vibrate, Style.Vibrate);
                            android_dict.Add(lights, Style.Lights);
                            android_dict.Add(clearable, Style.Clearable);
                            android_dict.Add(icon_type, Style.IconType);
                            android_dict.Add(icon_res, Style.IconRes);
                            android_dict.Add(style_id, Style.StyleId);
                            android_dict.Add(action, Action);
                            break;

                        case MessageType.Message:
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(Type));
                    }
                    msg.Add("android", android_dict);
                    param.Add(message, msg);
                    return param;
                }
            }
        }

        #endregion
    }
}

namespace XgPush.SDK.Server.Compat
{
#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    ///
    /// </summary>
    public static partial class XgPush_Server_SDK_Compat_Extensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public static TMessage setExpireTime<TMessage>(
            this TMessage message, Second expireTime) where TMessage : MessageBase
        {
            message.ExpireTime = expireTime;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int getExpireTime(this MessageBase message)
        {
            return message.ExpireTime;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="sendTime"></param>
        /// <returns></returns>
        public static TMessage setSendTime<TMessage>(this TMessage message, NullableDateTime sendTime)
            where TMessage : MessageBase
        {
            message.SendTime = sendTime;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string getSendTime(this MessageBase message)
        {
            return message.SendTime;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TMessage setType<TMessage>(this TMessage message, int type)
            where TMessage : MessageBase
        {
            message.Type = (MessageType)type;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TMessage setType<TMessage>(this TMessage message, MessageType type)
            where TMessage : MessageBase
        {
            message.Type = type;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int getType(this MessageBase message)
        {
            return (int)message.Type;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static TMessage setRaw<TMessage>(this TMessage message, string raw)
            where TMessage : MessageBase
        {
            message.Raw = raw;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="loopInterval"></param>
        /// <returns></returns>
        public static TMessage setLoopInterval<TMessage>(this TMessage message, int loopInterval)
            where TMessage : MessageBase
        {
            message.LoopInterval = loopInterval;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int getLoopInterval(this MessageBase message)
        {
            return message.LoopInterval;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="acceptTime"></param>
        /// <returns></returns>
        public static TMessage addAcceptTime<TMessage>(this TMessage message, TimeInterval acceptTime)
            where TMessage : MessageBase
        {
            message.AddAcceptTime(acceptTime);
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JArray acceptTimeToJsonArray<TMessage>(this TMessage message)
            where TMessage : MessageBase
        {
            return new JArray(message.AcceptTimes);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="message"></param>
        /// <param name="loopInterval"></param>
        /// <returns></returns>
        public static TMessage setLoopTimes<TMessage>(this TMessage message, int loopInterval)
            where TMessage : MessageBase
        {
            message.LoopTimes = loopInterval;
            return message;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int getLoopTimes(this MessageBase message)
        {
            return message.LoopTimes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <typeparam name="TCustomContent"></typeparam>
        /// <param name="message"></param>
        /// <param name="custom"></param>
        /// <returns></returns>
        public static TMessage setCustom<TMessage, TCustomContent>(this TMessage message, TCustomContent custom)
            where TMessage : MessageBase<TCustomContent>
        {
            message.Custom = custom;
            return message;
        }
    }

#pragma warning restore IDE1006 // 命名样式
}