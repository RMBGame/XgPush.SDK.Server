using JetBrains.Annotations;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    public partial class XingePushClient
    {
        /// <summary>
        ///
        /// </summary>
        public class V3
        {
            /// <summary>
            /// 信鸽系统生成的用于标识应用的ID，在调用 PUSH API V3 时需要使用。
            /// </summary>
            protected readonly string APPID;

            /// <summary>
            /// 用于验证API调用的密钥，用于验证调用的合法性。
            /// </summary>
            protected readonly string SECRETKEY;

            /// <summary>
            ///
            /// </summary>
            protected readonly HttpClient client;

#pragma warning disable IDE1006 // 命名样式

            /// <summary>
            ///
            /// </summary>
            public iOSEnvironmentV3 iOSEnvironment { get; set; }

#pragma warning restore IDE1006 // 命名样式

            /// <summary>
            ///
            /// </summary>
            /// <param name="app_id"></param>
            /// <param name="secret_key"></param>
            public V3([NotNull]string app_id, [NotNull]string secret_key)
                : this(app_id, secret_key, iOSEnvironmentV3.Production, new HttpClient())
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="app_id"></param>
            /// <param name="secret_key"></param>
            /// <param name="env"></param>
            public V3([NotNull]string app_id, [NotNull]string secret_key, iOSEnvironmentV3 env)
                : this(app_id, secret_key, env, new HttpClient())
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="app_id"></param>
            /// <param name="secret_key"></param>
            /// <param name="env"></param>
            /// <param name="client"></param>
            public V3([NotNull]string app_id, [NotNull]string secret_key, iOSEnvironmentV3 env,
                HttpClient client)
            {
                APPID = app_id;
                iOSEnvironment = env;
                SECRETKEY = secret_key;
                this.client = client;
            }

            /// <summary>
            /// <see cref="Encoding.UTF8"/> + <see cref="Convert.ToBase64String(byte[])"/>
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            protected virtual string ToBase64String(string s)
                => Convert.ToBase64String(Encoding.UTF8.GetBytes(s));

            /// <summary>
            ///
            /// </summary>
            /// <param name="o"></param>
            /// <returns></returns>
            protected virtual string Serialize(object o) => o.Serialize();

            /// <summary>
            ///
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="s"></param>
            /// <returns></returns>
            protected virtual T Deserialize<T>(string s) where T : new() => s.Deserialize<T>();

            #region HTTP

            /// <summary>
            ///
            /// </summary>
            /// <param name="response"></param>
            /// <returns></returns>
            protected virtual Task<string> ReadAsStringAsync(HttpResponseMessage response)
            {
                return XingePushClientBase.ReadAsStringAsync(response);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="requestUri"></param>
            /// <param name="content"></param>
            /// <returns></returns>
            protected virtual async Task<string> PostAsync
                ([NotNull]string requestUri, [NotNull]string content)
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, requestUri))
                {
                    var base64_auth_string = ToBase64String($"{APPID}:{SECRETKEY}");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64_auth_string);
                    request.Content = new StringContent(content, Encoding.UTF8, Constants.JSON_MIME);
                    using (var response = await client.SendAsync(request))
                    {
                        return await ReadAsStringAsync(response);
                    }
                }
            }

            /// <summary>
            ///
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="requestUri"></param>
            /// <param name="content"></param>
            /// <returns></returns>
            protected virtual async Task<T> PostAsync<T>
                ([NotNull] string requestUri, [NotNull] object content) where T : new()
            {
                var contentString = Serialize(content);

                if (!requestUri.IsHttpUrl())
                {
                    var baseAddress = client.BaseAddress;
                    if (baseAddress == null)
                    {
                        requestUri = baseAddress.ToString() + requestUri;
                    }
                    else
                    {
                        requestUri = Constants.BaseAddress_HTTPS + requestUri;
                    }
                }

                var rsp = await PostAsync(requestUri, contentString);
                var result = Deserialize<T>(rsp);
                return result;
            }

            #endregion

            /// <summary>
            ///
            /// </summary>
            /// <typeparam name="TMessage"></typeparam>
            /// <param name="request"></param>
            /// <returns></returns>
            public async Task<XingePushClientResultV3.Push> Push<TMessage>
                (PushRequest<TMessage> request) where TMessage : MessageBase, IToDictionaryV3
            {
                var result = await PostAsync<XingePushClientResultV3.Push>(RESTAPI_V3_PUSH, request);
                return result;
            }
        }
    }
}