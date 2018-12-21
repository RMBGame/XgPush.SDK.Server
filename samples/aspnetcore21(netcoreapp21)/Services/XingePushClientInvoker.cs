using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Http;
using XgPush.SDK.Server;

namespace Sample.XgPush.SDK.Server.Services
{
    internal sealed class XingePushClientInvoker : XingePushClient
    {
        public XingePushClientInvoker(
            HttpClient httpClient,
            IOptions<XgPushOptions> options,
            IHostingEnvironment env) : base(options.Value.AppId, options.Value.AccessId, options.Value.SecretKey, env.IsDevelopment() ? iOSEnvironment.Development : iOSEnvironment.Production, httpClient)
        {
        }
    }
}