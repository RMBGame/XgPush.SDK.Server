using System;
using System.Collections.Generic;
using System.Linq;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// (V3) Push API 参数。
    /// </summary>
    public abstract class PushRequest<TMessage> : BaseSerializeObject, IToDictionaryV3 where TMessage : MessageBase, IToDictionaryV3
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator AudienceType(PushRequest<TMessage> value) => value.AudienceType;

        /// <summary>
        /// 推送目标。
        /// </summary>
        protected abstract AudienceType AudienceType { get; }

        /// <summary>
        /// 消息体，参见消息体格式。
        /// </summary>
        public TMessage Message { get; set; }

        /// <summary>
        /// 客户端平台类型。
        /// <para>android：安卓</para>
        /// <para>ios：苹果</para>
        /// <para>all：安卓&amp;&amp;苹果，仅支持全量推送和标签推送</para>
        /// </summary>
        public string Platform
        {
            get
            {
                if (Message is MessageBase.IPlatform message)
                {
                    switch (message.Platform)
                    {
                        case Internal.Platform.Android:
                            return "android";

                        case Internal.Platform.iOS:
                            return "ios";
                    }
                }

                if (AudienceType == AudienceType.All || AudienceType == AudienceType.Tag)
                {
                    return "all";
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(Message));
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string MessageType
        {
            get
            {
                switch (Message.Type)
                {
                    case Server.MessageType.Notification:
                        return "notify";

                    case Server.MessageType.Message:
                        return "message";

                    default:
                        throw new ArgumentOutOfRangeException(nameof(MessageType));
                }
            }
        }

        /// <summary>
        /// 统计标签，用于聚合统计。
        /// <para>使用场景(示例)：</para>
        /// <para>现在有一个活动id：active_picture_123，需要给10000个设备通过单推接口（或者列表推送等推送形式）下发消息，同时设置该字段为 active_picture_123 推送完成之后可以使用v3统计查询接口，根据该标签 active_picture_123 查询这 10000 个设备的实发、抵达、展示、点击数据。</para>
        /// </summary>
        public string StatTag { get; set; }

        /// <summary>
        /// 接口调用时，在应答包中信鸽会回射该字段，可用于异步请求。
        /// <para>使用场景：异步服务中可以通过该字段找到server端返回的对应应答包。</para>
        /// </summary>
        public long? Seq { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected virtual Dictionary<string, object> KeyValuePairsV3
        {
            get
            {
                var param = new Dictionary<string, object>
                {
                    {"audience_type", AudienceType},
                    {"platform", Platform},
                    {"message_type", MessageType}
                };
                if (!string.IsNullOrWhiteSpace(StatTag))
                    param.Add("stat_tag", StatTag);
                if (Seq.HasValue)
                    param.Add("seq", Seq);
                foreach (var item in Message.ToDictionaryV3())
                    param.Add(item.Key, item.Value);
                return param;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionaryV3()
        {
            return KeyValuePairsV3;
        }

        /// <summary>
        /// (V3) Push API (全量推送)。
        /// </summary>
        public class All : PushRequest<TMessage>
        {
            /// <inheritdoc cref="PushRequest{TMessage}.AudienceType" />
            protected override AudienceType AudienceType => AudienceType.All;
        }

        /// <summary>
        /// (V3) Push API (标签推送)。
        /// </summary>
        public class Tag : PushRequest<TMessage>
        {
            /// <inheritdoc cref="PushRequest{TMessage}.AudienceType" />
            protected override AudienceType AudienceType => AudienceType.Tag;

            /// <summary>
            ///
            /// </summary>
            public TagList Value { get; set; }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    param.Add("tag_list", Value);
                    return param;
                }
            }
        }

        /// <summary>
        /// (V3) Push API (单设备推送)。
        /// </summary>
        public class Token : PushRequest<TMessage>
        {
            /// <inheritdoc cref="PushRequest{TMessage}.AudienceType" />
            protected override AudienceType AudienceType => AudienceType.Token;

            /// <summary>
            /// token。
            /// </summary>
            public string Value
            {
                get => token_list.FirstOrDefault();
                set => token_list = new string[] { value };
            }

            /// <summary>
            /// 如果该参数包含多个token 只会推送第一个token。
            /// </summary>
            protected ICollection<string> token_list;

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    param.Add(nameof(token_list), token_list);
                    return param;
                }
            }
        }

        /// <summary>
        /// (V3) Push API (设备列表推送)。
        /// </summary>
        public class TokenList : Token
        {
            /// <inheritdoc cref="PushRequest{TMessage}.AudienceType" />
            protected override AudienceType AudienceType => AudienceType.TokenList;

            /// <summary>
            /// 最多 1000 个 token。
            /// </summary>
            public ICollection<string> Values { get => token_list; set => token_list = value; }

            /// <summary>
            /// 1. 第一次推送该值填0，系统会创建对应的推送任务，并且返回对应的 pushid：123。
            /// <para></para>
            /// 2. 后续推送push_id 填123(同一个文案）表示使用与123 id 对应的文案进行推送。
            /// </summary>
            public string PushId { get; set; }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    if (string.IsNullOrWhiteSpace(PushId))
                        PushId = "0";
                    param.Add("push_id", PushId);
                    return param;
                }
            }
        }

        /// <summary>
        /// (V3) Push API (单账号推送)。
        /// </summary>
        public class Account : PushRequestBase<TMessage>.Accounts
        {
            /// <inheritdoc cref="PushRequest{TMessage}.AudienceType" />
            protected override AudienceType AudienceType => AudienceType.Account;

            /// <summary>
            /// 该参数有多个账号时，仅推送第一个账号。
            /// </summary>
            public string Value
            {
                get => account_list?.FirstOrDefault();
                set => account_list = new string[] { value };
            }

            /// <summary>
            /// 1) 0: 往单个账号的最新的device上推送信息
            /// <para></para>
            /// 2) 1: 往单个账号关联的所有device设备上推送信息
            /// </summary>
            public DigitBoolean Type { get; set; }

            /// <summary>
            /// 1）账号类型，参考后面账号说明
            /// <para></para>
            /// 2）必须与账号绑定时设定的账号类型一致
            /// </summary>
            public AccountType AccountType { get; set; }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    param.Add("account_push_type", Type.ToInt32());
                    param.Add("account_type", (int)AccountType);
                    return param;
                }
            }
        }

        /// <summary>
        /// (V3) Push API (账号列表推送)。
        /// </summary>
        public class AccountList : PushRequestBase<TMessage>.Accounts
        {
            /// <inheritdoc cref="PushRequest{TMessage}.AudienceType" />
            protected override AudienceType AudienceType => AudienceType.AccountList;

            /// <summary>
            /// 最多 1000 个 account。
            /// </summary>
            public ICollection<string> Values
            {
                get => account_list;
                set => account_list = value;
            }

            /// <summary>
            /// 1. 第一次推送该值填0，系统会创建对应的推送任务，并且返回对应的 pushid：123。
            /// <para></para>
            /// 2. 后续推送push_id 填123(同一个文案）表示使用与123 id 对应的文案进行推送。
            /// </summary>
            public string PushId { get; set; }

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    if (string.IsNullOrWhiteSpace(PushId))
                        PushId = "0";
                    param.Add("push_id", PushId);
                    return param;
                }
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public sealed class RawPushRequest : PushRequest<RawMessage>, IRaw
    {
        /// <summary>
        ///
        /// </summary>
        [Obsolete("", true)]
        public RawPushRequest() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="raw"></param>
        public RawPushRequest(string raw)
        {
            Raw = raw;
        }

        /// <summary>
        /// 获取或设置消息JSON字符串。
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        ///
        /// </summary>
        protected override AudienceType AudienceType => throw new NotImplementedException();
    }
}

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public static class PushRequestBase<TMessage> where TMessage : MessageBase, IToDictionaryV3
    {
        /// <summary>
        ///
        /// </summary>
        public abstract class Accounts : PushRequest<TMessage>
        {
            /// <summary>
            /// (不公开)账号参数，多个或单个。
            /// </summary>
            protected ICollection<string> account_list;

            /// <summary>
            ///
            /// </summary>
            protected override Dictionary<string, object> KeyValuePairsV3
            {
                get
                {
                    var param = base.KeyValuePairsV3;
                    param.Add(nameof(account_list), account_list);
                    return param;
                }
            }
        }
    }
}