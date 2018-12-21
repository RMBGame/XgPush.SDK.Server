using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;

namespace XgPush.SDK.Server.Test.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8117")
                .Configure(app =>
                {
                    app.Run(async context =>
                    {
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                        {
                            ret_code = 0,
                            err_msg = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                            result = context.Request.Headers.ToDictionary(k => k.Key, v => v.Value.ToString()),
                        }));
                    });
                })
                .Build()
                .Run();
        }
    }
}