using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static XgPush.SDK.Server.Internal.Constants;
using static XgPush.SDK.Server.Internal.TFMs_Compat;

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public abstract class XingePushClientBase
    {
        /// <summary>
        ///
        /// </summary>
        protected const uint IOSENV_PROD = iOSEnvironment.ProductionUInt32;

        /// <summary>
        ///
        /// </summary>
        protected const uint IOSENV_DEV = iOSEnvironment.DevelopmentUInt32;

        /// <summary>
        ///
        /// </summary>
        protected const long IOS_MIN_ID = 2200000000L;

        /// <summary>
        /// 信鸽系统生成的ID，需要配置到客户端SDK中。此外，在调用 PUSH API V2 时也需要使用。
        /// </summary>
        protected readonly long m_access_id;

        /// <summary>
        /// 用于验证API调用的密钥，用于验证调用的合法性。
        /// </summary>
        protected readonly string m_secret_key;

        /// <summary>
        ///
        /// </summary>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        protected XingePushClientBase(long access_id, [NotNull]string secret_key)
        {
            m_access_id = access_id;
            m_secret_key = secret_key ?? throw new ArgumentNullException(nameof(secret_key));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        protected virtual string ComputeMD5([NotNull]string inputString, Encoding encoding = null)
        {
            if (inputString == null) throw new ArgumentNullException(nameof(inputString));
            byte[] encryptedBytes;
            using (var md5 = CreateMD5())
                encryptedBytes =
                    md5.ComputeHash((encoding ?? Encoding.UTF8).GetBytes(inputString));
            return string.Join(null, encryptedBytes.Select(x => x.ToString("x2")));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected bool IsValidToken([NotNull]string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (m_access_id > IOS_MIN_ID) return token.Length == 64;
            return token.Length == 40 || token.Length == 64;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        protected bool IsValidMessageType(Platform platform)
        {
            switch (platform)
            {
                case Platform.Android:
                    return m_access_id < IOS_MIN_ID;

                case Platform.iOS:
                    return
                          m_access_id >= IOS_MIN_ID;
                default:
                    throw new ArgumentOutOfRangeException(nameof(platform));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool IsValidMessageType(MessageBase.IPlatform message)
            => IsValidMessageType(message.Platform);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected long GetTimestamp() => (DateTime.UtcNow.Ticks - 621355968000000000) / 10000000;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> InitParams() => new Dictionary<string, object>
        {
            { access_id, m_access_id },
            { timestamp, GetTimestamp() },
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        protected IDictionary<string, object> TryInitParams(IDictionary<string, object> dict)
        {
            if (dict == null) return InitParams();
            if (!dict.ContainsKey(access_id)) dict.Add(access_id, m_access_id);
            if (!dict.ContainsKey(timestamp)) dict.Add(timestamp, GetTimestamp());
            return dict;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        protected JArray ToJArray<T>([NotNull] IEnumerable<T> enumerable)
            => new JArray(enumerable);

        internal static async Task<string> ReadAsStringAsync([NotNull]HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            if (response.Content == null) throw new ArgumentNullException(nameof(response.Content));
            if (!response.Content.Headers.ContentLength.HasValue) throw new ArgumentOutOfRangeException(nameof(response.Content.Headers.ContentLength));
            var responseString = await response.Content.ReadAsStringAsync();
            if (responseString == null) throw new ArgumentNullException(nameof(responseString));
            if (string.IsNullOrWhiteSpace(responseString)) throw new ArgumentOutOfRangeException(nameof(responseString));
            return responseString;
        }

        #region API_URL

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

        public const string RESTAPI_PUSHSINGLEDEVICE = "/v2/push/single_device";
        public const string RESTAPI_PUSHSINGLEACCOUNT = "/v2/push/single_account";
        public const string RESTAPI_PUSHACCOUNTLIST = "/v2/push/account_list";
        public const string RESTAPI_PUSHALLDEVICE = "/v2/push/all_device";
        public const string RESTAPI_PUSHTAGS = "/v2/push/tags_device";
        public const string RESTAPI_CREATEMULTIPUSH = "/v2/push/create_multipush";
        public const string RESTAPI_PUSHACCOUNTLISTMULTIPLE = "/v2/push/account_list_multiple";
        public const string RESTAPI_PUSHDEVICELISTMULTIPLE = "/v2/push/device_list_multiple";
        public const string RESTAPI_QUERYPUSHSTATUS = "/v2/push/get_msg_status";
        public const string RESTAPI_QUERYDEVICECOUNT = "/v2/application/get_app_device_num";
        public const string RESTAPI_QUERYTAGS = "/v2/tags/query_app_tags";
        public const string RESTAPI_CANCELTIMINGPUSH = "/v2/push/cancel_timing_task";
        public const string RESTAPI_BATCHSETTAG = "/v2/tags/batch_set";
        public const string RESTAPI_BATCHDELTAG = "/v2/tags/batch_del";
        public const string RESTAPI_QUERYTOKENTAGS = "/v2/tags/query_token_tags";
        public const string RESTAPI_QUERYTAGTOKENNUM = "/v2/tags/query_tag_token_num";
        public const string RESTAPI_QUERYINFOOFTOKEN = "/v2/application/get_app_token_info";
        public const string RESTAPI_QUERYTOKENSOFACCOUNT = "/v2/application/get_app_account_tokens";
        public const string RESTAPI_DELETETOKENOFACCOUNT = "/v2/application/del_app_account_tokens";
        public const string RESTAPI_DELETEALLTOKENSOFACCOUNT = "/v2/application/del_app_account_all_tokens";

        public const string RESTAPI_V3_PUSH = "/v3/push/app";
        public const string RESTAPI_V3_TAG = "/v3/device/tag";

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

        #endregion
    }

    public partial class MessageBase
    {
        /// <summary>
        /// 平台消息接口。
        /// </summary>
        public interface IPlatform
        {
            /// <summary>
            /// 所属平台。
            /// </summary>
            Platform Platform { get; }
        }
    }
}