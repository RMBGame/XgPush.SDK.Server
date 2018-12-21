using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using XgPush.SDK.Server;

namespace Sample.XgPush.SDK.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            XingePushClient.InitHttpClientCompatDefaultMagic();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}