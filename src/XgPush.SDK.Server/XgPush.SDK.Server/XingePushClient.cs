using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public partial class XingePushClient : XingePushClientBase
#if DEBUG
        , DEBUG.IDebugXingePushClient
#endif
    {
        /// <summary>
        ///
        /// </summary>
        protected readonly HttpClient client;

        private HttpMethod mHttpMethod = HttpMethod.Get;

        /// <summary>
        /// Rest API V2 请求方式
        /// <para>仅支持 <see cref="HttpMethod.Get"/> || <see cref="HttpMethod.Post"/> </para>
        /// </summary>
        public HttpMethod HttpMethod
        {
            get
            {
                return mHttpMethod;
            }
            set
            {
                if (value != HttpMethod.Get && value != HttpMethod.Post)
                    throw new ArgumentOutOfRangeException(nameof(HttpMethod));
                mHttpMethod = value;
            }
        }

#pragma warning disable IDE1006 // 命名样式

        /// <summary>
        ///
        /// </summary>
        public V3 v3 { get; private set; }

#pragma warning restore IDE1006 // 命名样式

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual V3 CreateV3(string app_id)
            => string.IsNullOrWhiteSpace(app_id) ? default :
            new V3(app_id, m_secret_key, iOSEnvironment, client);

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <returns></returns>
        public static XingePushClient Create([NotNull] string app_id, long access_id, [NotNull] string secret_key)
        {
            return new XingePushClient(app_id, access_id, secret_key);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public static XingePushClient Create([NotNull] string app_id, long access_id, [NotNull] string secret_key, [NotNull]HttpClient httpClient)
        {
            return new XingePushClient(app_id, access_id, secret_key, httpClient);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static XingePushClient Create([NotNull] string app_id, long access_id, [NotNull] string secret_key, iOSEnvironment env)
        {
            return new XingePushClient(app_id, access_id, secret_key, env, new HttpClient());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="env"></param>
        /// <param name="httpClient"></param>
        /// <returns></returns>
        public static XingePushClient Create([NotNull] string app_id, long access_id, [NotNull] string secret_key, iOSEnvironment env, [NotNull]HttpClient httpClient)
        {
            return new XingePushClient(app_id, access_id, secret_key, env, httpClient);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="env"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XingePushClient Create([NotNull] string app_id, long access_id, [NotNull] string secret_key, iOSEnvironment env, [NotNull] HttpMessageHandler handler)
        {
            return new XingePushClient(app_id, access_id, secret_key, env, new HttpClient(handler));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static XingePushClient Create([NotNull] string app_id, long access_id, [NotNull] string secret_key, [NotNull] HttpMessageHandler handler)
        {
            return new XingePushClient(app_id, access_id, secret_key, new HttpClient(handler));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        protected XingePushClient([NotNull] string app_id, long access_id,
            [NotNull] string secret_key)
            : this(app_id, access_id, secret_key, iOSEnvironment.Production, new HttpClient()) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="env"></param>
        protected XingePushClient([NotNull] string app_id, long access_id,
            [NotNull] string secret_key, iOSEnvironment env)
            : this(app_id, access_id, secret_key, env, new HttpClient()) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="httpClient"></param>
        protected XingePushClient([NotNull] string app_id, long access_id,
            [NotNull] string secret_key, [NotNull] HttpClient httpClient)
            : this(app_id, access_id, secret_key, iOSEnvironment.Production, httpClient) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="access_id"></param>
        /// <param name="secret_key"></param>
        /// <param name="env"></param>
        /// <param name="httpClient"></param>
        protected XingePushClient([NotNull] string app_id, long access_id,
            [NotNull] string secret_key, iOSEnvironment env, [NotNull] HttpClient httpClient) : base(access_id, secret_key)
        {
            iOSEnvironment = env;
            client = httpClient;
            v3 = CreateV3(app_id);
        }

#pragma warning disable IDE1006 // 命名样式

        /// <summary>
        ///
        /// </summary>
        public iOSEnvironment iOSEnvironment { get; set; }

#pragma warning restore IDE1006 // 命名样式

        #region CompatMagic

        /// <summary>
        ///
        /// </summary>
        public static void InitHttpClientCompatDefaultMagic()
            => TFMs_Compat.InitHttpClientCompatDefaultMagic();

        /// <summary>
        ///
        /// </summary>
        /// <param name="magics"></param>
        public static void InitHttpClientCompatCustomMagic(params Func<Dictionary<string, Action>>[] magics)
            => TFMs_Compat.InitHttpClientCompatCustomMagic(magics);

        /// <summary>
        ///
        /// </summary>
        /// <param name="magics"></param>
        public static void InitHttpClientCompatCustomMagic([NotNull]IEnumerable<Func<Dictionary<string, Action>>> magics)
            => TFMs_Compat.InitHttpClientCompatCustomMagic(magics);

        #endregion

        #region HTTP

        /// <summary>
        ///
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="value"></param>
        protected virtual void TryAddContentType([NotNull]HttpRequestHeaders headers, [NotNull] string value)
        {
            XgPush_Server_SDK_GlobalExtensions.TryAddContentType(headers, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected new virtual Task<string> ReadAsStringAsync([NotNull]HttpResponseMessage response)
        {
            return XingePushClientBase.ReadAsStringAsync(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        protected virtual async Task<string> GetStringAsync([NotNull]string requestUri)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                TryAddContentType(client.DefaultRequestHeaders, Constants.JSON_MIME);
                using (var response = await client.SendAsync(request))
                {
                    return await ReadAsStringAsync(response);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="nameValueCollection"></param>
        /// <returns></returns>
        protected virtual async Task<string> PostStringAsync([NotNull]string requestUri, IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, requestUri))
            {
                if (nameValueCollection != default)
                    request.Content = new FormUrlEncodedContent(nameValueCollection);
                using (var response = await client.SendAsync(request))
                {
                    return await ReadAsStringAsync(response);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected virtual XingePushClientResult CreateErrorResult(string errMsg)
        {
            return new XingePushClientResult(null, errMsg);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        protected virtual XingePushClientResult<TResult> CreateErrorResult<TResult>(string errMsg) where TResult : IResultV2, new()
        {
            return new XingePushClientResult<TResult>(null, errMsg);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult> SendAsync(
            string requestUri,
            params KeyValuePair<string, object>[] args1)
        {
            return SendAsync(requestUri, null, args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="args0"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult> SendAsync(
            string requestUri,
            IToDictionary args0,
            params KeyValuePair<string, object>[] args1)
        {
            return SendAsync(requestUri, args0, (IEnumerable<KeyValuePair<string, object>>)args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult<TResult>> SendAsync<TResult>(
            string requestUri,
            params KeyValuePair<string, object>[] args1) where TResult : IResultV2, new()
        {
            return SendAsync<TResult>(requestUri, null, args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="args0"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult<TResult>> SendAsync<TResult>(
            string requestUri,
            IToDictionary args0,
            params KeyValuePair<string, object>[] args1) where TResult : IResultV2, new()
        {
            return SendAsync<TResult>(requestUri, args0, (IEnumerable<KeyValuePair<string, object>>)args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult> SendAsync(
            string requestUri,
            IEnumerable<KeyValuePair<string, object>> args1)
        {
            return SendAsync(requestUri, null, args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="args0"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult> SendAsync(
            string requestUri,
            IToDictionary args0,
            IEnumerable<KeyValuePair<string, object>> args1 = null)
        {
            return SendAsync(CreateErrorResult, requestUri, args0, args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult<TResult>> SendAsync<TResult>(
            string requestUri,
            IEnumerable<KeyValuePair<string, object>> args1) where TResult : IResultV2, new()
        {
            return SendAsync<TResult>(requestUri, null, args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="requestUri"></param>
        /// <param name="args0"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected Task<XingePushClientResult<TResult>> SendAsync<TResult>(
            string requestUri,
            IToDictionary args0,
            IEnumerable<KeyValuePair<string, object>> args1 = null) where TResult : IResultV2, new()
        {
            return SendAsync(CreateErrorResult<TResult>, requestUri, args0, args1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="createErrorResult"></param>
        /// <param name="requestUri"></param>
        /// <param name="args0"></param>
        /// <param name="args1"></param>
        /// <returns></returns>
        protected async Task<TResult> SendAsync<TResult>(
            Func<string, TResult> createErrorResult,
            string requestUri,
            IToDictionary args0 = null,
            IEnumerable<KeyValuePair<string, object>> args1 = null) where TResult : new()
        {
            var is_iOS_msg = false;
            IDictionary<string, object> args = null;

            #region args0

            var args0_ = args0.ToDictionary();
            if (args0_ != null)
            {
                args = args0_;
            }
            else
            {
                if (args0 is MessageBase.IPlatform message)
                {
                    if (!IsValidMessageType(message))
                        return createErrorResult(Constants.ErrorMessageType);
                    is_iOS_msg = message.Platform == Platform.iOS;
                }

                if (args0 is IsValid @is && !@is.IsValid())
                {
                    return createErrorResult(Constants.ErrorMessageInvalid);
                }
            }

            #endregion

            #region args1

            if (args1 != null)
            {
                if (args == null)
                {
                    if (args1 is IDictionary<string, object> args1_)
                    {
                        args = args1_;
                    }
                    else
                    {
                        args = args1.ToDictionary(k => k.Key, v => v.Value);
                    }
                }
                else
                {
                    foreach (var arg in args1)
                    {
                        args.Add(arg.Key, arg.Value);
                    }
                }
            }

            #endregion

            args = TryInitParams(args);

            if (is_iOS_msg && !args.ContainsKey(Constants.environment))
            {
                args.Add(Constants.environment, iOSEnvironment);
            }

            IDictionary<string, string> postArgs = null;
            StringBuilder requestUriStringBuilder = null;
            StringBuilder signStringBuilder;

            if (HttpMethod == HttpMethod.Get)
            {
                requestUriStringBuilder = new StringBuilder();
                signStringBuilder = new StringBuilder(Constants.GET);
            }
            else if (HttpMethod == HttpMethod.Post)
            {
                postArgs = new Dictionary<string, string>();
                signStringBuilder = new StringBuilder(Constants.POST);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(HttpMethod));
            }

            if (requestUri.IsHttpUrl())
            {
                var u = new Uri(requestUri);
                requestUriStringBuilder?.Append(requestUri);
                signStringBuilder.Append(u.Host);
                signStringBuilder.Append(u.AbsolutePath);
            }
            else
            {
                var baseAddress = client.BaseAddress;
                if (baseAddress == null)
                {
                    requestUri = Constants.BaseAddress_HTTPS + requestUri;
                    requestUriStringBuilder.Append(Constants.BaseAddress_HTTPS);
                    signStringBuilder.Append(Constants.Host);
                }
                else
                {
                    requestUri = baseAddress.ToString() + requestUri;
                    requestUriStringBuilder.Append(requestUri);
                    signStringBuilder.Append(baseAddress.Host);
                }
                signStringBuilder.Append(requestUri);
            }

            foreach (var arg in args.OrderBy(arg => arg.Key))
            {
                signStringBuilder.Append(arg.Key);
                signStringBuilder.Append(Constants.equal);
                var value = arg.Value == null ? string.Empty :
                    (arg.Value is JArray jArray ?
                    jArray.ToString(Formatting.None) : arg.Value.ToString());
                signStringBuilder.Append(value);

                if (postArgs != null)
                {
                    postArgs.Add(arg.Key, value);
                }

                if (requestUriStringBuilder != null)
                {
                    requestUriStringBuilder.Append(arg.Key);
                    requestUriStringBuilder.Append(Constants.equal);
                    value = TFMs_Compat.UrlEncode(value);
                    requestUriStringBuilder.Append(value);
                }
            }

            signStringBuilder.Append(m_secret_key);

            var signString = signStringBuilder.ToString();
            signString = ComputeMD5(signString);

            if (requestUriStringBuilder != null)
            {
                requestUriStringBuilder.Append(Constants.sign);
                requestUriStringBuilder.Append(Constants.equal);
                requestUriStringBuilder.Append(signString);

                requestUri = requestUriStringBuilder.ToString();
            }

            if (HttpMethod == HttpMethod.Post)
            {
                args.Add(Constants.sign, signString);
            }

            string @string;

            if (HttpMethod == HttpMethod.Get)
            {
                @string = await GetStringAsync(requestUri);
            }
            else if (HttpMethod == HttpMethod.Post)
            {
                @string = await PostStringAsync(requestUri, postArgs);
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(HttpMethod));
            }

            return @string.Deserialize<TResult>();
        }

        #endregion

#if DEBUG

        Task<XingePushClientResult> DEBUG.IDebugXingePushClient.DebugTest(string url) => SendAsync(url);

#endif
    }
}