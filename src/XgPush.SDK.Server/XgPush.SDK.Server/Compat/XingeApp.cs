using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using XgPush.SDK.Server.Internal;
using static XgPush.SDK.Server.Internal.Constants;

#pragma warning disable IDE1006 // 命名样式

namespace XgPush.SDK.Server.Compat
{
    /// <summary>
    ///
    /// </summary>
    [Obsolete("XingeApp is deprecated, please use XingePushClient instead.")]
    public partial class XingeApp : XingePushClientBase
    {
        /// <summary>
        ///
        /// </summary>
        public new static uint IOSENV_PROD => XingePushClientBase.IOSENV_PROD;

        /// <summary>
        ///
        /// </summary>
        public new static uint IOSENV_DEV => XingePushClientBase.IOSENV_DEV;

        /// <summary>
        ///
        /// </summary>
        public new static long IOS_MIN_ID => XingePushClientBase.IOS_MIN_ID;

        /// <summary>
        ///
        /// </summary>
        /// <param name="accessID"></param>
        /// <param name="secretKey"></param>
        public XingeApp(long accessID, [NotNull]string secretKey) : base(accessID, secretKey) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        protected string stringToMD5([NotNull]string inputString)
            => ComputeMD5(inputString, Encoding.ASCII);

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        protected bool isValidToken([NotNull]string token) => IsValidToken(token);

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected bool isValidMessageType([NotNull]Message msg) => IsValidMessageType(msg);

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        protected bool isValidMessageType([NotNull]MessageIOS msg, int env)
            => m_access_id >= IOS_MIN_ID && (env == IOSENV_PROD || env == IOSENV_DEV);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected Dictionary<string, object> initParams() => InitParams();

        /// <summary>
        ///
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        protected JArray toJArray([NotNull]IEnumerable<string> enumerable) => ToJArray(enumerable);

        /// <summary>
        ///
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string generateSign(string method, string url, IDictionary<string, object> param)
        {
            var paramStr = param.OrderBy(x => x.Key)
                .Aggregate<KeyValuePair<string, object>, string>
                (null, (current, kvp) => current + kvp.Key + "=" + kvp.Value);
            var u = new Uri(url);
            var md5Str = method + u.Host + u.AbsolutePath + paramStr + m_secret_key;
            md5Str = TFMs_Compat.UrlDecode(md5Str);
            return ComputeMD5(md5Str);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual string callRestful(string url, IDictionary<string, object> param)
        {
            url = BaseAddress + url;
            var sign = generateSign(GET, url, param);
            if (string.IsNullOrEmpty(sign)) return "generate sign error";
            param.Add(nameof(sign), sign);
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(url);
            stringBuilder.Append("?");
            var i = 0;
            foreach (var kvp in param)
            {
                stringBuilder.Append(kvp.Key);
                stringBuilder.Append("=");
                stringBuilder.Append(TFMs_Compat.UrlEncode(kvp.Value.ToString()));
                if (i++ != param.Count - 1) stringBuilder.Append("&");
            }
            url = stringBuilder.ToString();
            return Send(url, JSON_MIME, GET, 20000);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentType"></param>
        /// <param name="method"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected virtual string Send(string url, string contentType, string method, int timeout)
        {
#if NETSTANDARD1_3
            throw new PlatformNotSupportedException();
#else
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = method;
            httpWebRequest.Timeout = timeout;
            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    string responseContent = streamReader.ReadToEnd();
                    return responseContent;
                }
            }
#endif
        }
    }
}