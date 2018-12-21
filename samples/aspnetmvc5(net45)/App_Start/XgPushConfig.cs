using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using XgPush.SDK.Server;

namespace Sample.XgPush.SDK.Server
{
    public static class XgPushConfig
    {
        public static XingePushClient Client { get; private set; }

        public static void Register(HttpClient httpClient)
        {
            var app_id = WebConfigurationManager.AppSettings["XgPushConfig:AppId"];
            var access_id = long.Parse(WebConfigurationManager.AppSettings["XgPushConfig:AccessId"]);
            var secret_key = WebConfigurationManager.AppSettings["XgPushConfig:SecretKey"];
            var env = WebConfigurationManager.AppSettings["XgPushConfig:Environment"];
            //Client = XingePushClient.Create(app_id, access_id, secret_key, env, httpClient);
            Client = new MonoCompat(app_id, access_id, secret_key, env, httpClient);
        }

        private sealed class MonoCompat : XingePushClient
        {
            private static readonly bool IsRunningOnMono = Type.GetType("Mono.Runtime") != null;

            public MonoCompat(string app_id, long access_id, string secret_key, iOSEnvironment env, HttpClient httpClient) : base(app_id, access_id, secret_key, env, httpClient)
            {
            }

            protected override async Task<string> GetStringAsync(string requestUri)
            {
                if (!IsRunningOnMono)
                    return await base.GetStringAsync(requestUri);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = HttpMethod.Get.ToString();
                httpWebRequest.Timeout = 20000;
                using (var webResponse = await httpWebRequest.GetResponseAsync())
                {
                    var responseStream = ((HttpWebResponse)webResponse).GetResponseStream();
                    if (responseStream == null) throw new ArgumentNullException(nameof(responseStream));
                    using (var streamReader = new StreamReader(responseStream))
                    {
                        var responseContent = streamReader.ReadToEnd();
                        return responseContent;
                    }
                }
            }
        }
    }
}