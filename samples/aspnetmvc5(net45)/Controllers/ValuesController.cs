using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using XgPush.SDK.Server;
using XgPushSDKServerProperties = XgPush.SDK.Server.Properties;

namespace Sample.XgPush.SDK.Server.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly XingePushClient pushClient;

        public ValuesController() => pushClient = XgPushConfig.Client;

        private Task<XingePushClientResult> DebugTest()
        {
#if DEBUG
            return ((XgPush.SDK.Server.DEBUG.IDebugXingePushClient)pushClient).DebugTest("http://localhost:8117/");
#else
            return Task.FromResult<XingePushClientResult>(null);
#endif
        }

        // GET api/values
        public async Task<object> Get()
        {
            var assembly = XgPushSDKServerProperties.AssemblyInfo.Assembly;
            var result = await DebugTest();
            return new
            {
                Name = assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title,
                Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
                iOSEnvironment = $"{(string)pushClient.iOSEnvironment}({(uint)pushClient.iOSEnvironment})",
                Test = result,
            };
        }

        // POST api/values
        public async Task<XingePushClientResultV3.Push> Post([FromBody]string value)
        {
            var msg = new Message
            {
                Title = $"test {DateTimeOffset.Now}",
                Content = $"test push msg, {value}",
            };

            var request = new PushRequest<Message>.All
            {
                Message = msg,
            };

            var rsp = await pushClient.v3.Push(request);
            return rsp;
        }
    }
}