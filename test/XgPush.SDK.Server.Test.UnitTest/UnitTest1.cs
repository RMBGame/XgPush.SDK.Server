using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

#pragma warning disable CS0618 // 类型或成员已过时

namespace XgPush.SDK.Server.Test.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            XingePushClient.InitHttpClientCompatDefaultMagic();

            AudienceType type = new PushRequest<MessageiOS>.Token();

            var m = new Model
            {
                AudienceType = type,
                TimeInterval = new TimeInterval
                {
                    StartHour = 1,
                    StartMin = 2,
                    EndHour = 3,
                    EndMin = 4,
                },
                Operator1 = Operator.AND,
                Operator2 = Operator.OR,
                True = 1,
                False = 0,
                Null = null,
                NullableFalse = 0U,
                NullableTrue = 1U,
                ThreeDays = TimeSpan.FromDays(3),
                UInt32S = 60U,
                Int32S = 120,
                NullableSeconds = null,
                NullableThreeDays = TimeSpan.FromDays(3),
                NullableUInt32S = 60U,
                NullableInt32S = 120,
                TagTokenPair = new TagTokenPair
                {
                    Tag = "abc",
                    Token = "qwert"
                },
                TagTokenPairCompat = new Compat.TagTokenPair
                {
                    tag = "abc1",
                    token = "qwert1"
                },
                Now1 = DateTime.Now,
                Now2 = DateTimeOffset.Now,
                TimeEmpty1 = null,
                TimeEmpty2 = null,
                TimeNull2 = string.Empty,
                D1 = Internal.Constants.DefaultMessageSendTime,
                D2 = Internal.Constants.DefaultMessageSendTime,
                iOSEnv1 = 1,
                iOSEnv2 = 2,
                iOSEnvNullable = null,
                iOSEnv1Nullable = 1,
                iOSEnv2Nullable = 2,
            };

            m.iOSEnv1_V3 = m.iOSEnv1;
            m.iOSEnv2_V3 = m.iOSEnv2;
            m.iOSEnvNullable_V3 = m.iOSEnvNullable;
            m.iOSEnv1Nullable_V3 = m.iOSEnv1Nullable;
            m.iOSEnv2Nullable_V3 = m.iOSEnv2Nullable;

            var envE = m.iOSEnv1_V3 == m.iOSEnv1 &&
                 m.iOSEnv2_V3 == m.iOSEnv2 &&
                 m.iOSEnvNullable_V3 == m.iOSEnvNullable &&
                 m.iOSEnv1Nullable_V3 == m.iOSEnv1Nullable &&
                 m.iOSEnv2Nullable_V3 == m.iOSEnv2Nullable;

            Assert.IsTrue(envE, nameof(envE));

            var json = m.Serialize();
            var obj = json.Deserialize<Model>();
            var json2 = obj.Serialize();

            var isOK = json == json2;

            Assert.IsTrue(isOK, nameof(isOK));

            var iOSEnv = new iOSEnvironment(1);

            Assert.IsTrue(iOSEnv == iOSEnvironment.Production, nameof(iOSEnv));

            iOSEnvironmentV3 iOSEnv3 = iOSEnv;

            Assert.IsTrue(iOSEnv3 == iOSEnvironmentV3.Production, nameof(iOSEnv3));

            Console.WriteLine(json2);

            Console.WriteLine("Size of C# bool is: {0}", sizeof(bool));
            Console.WriteLine("Size of C# char is: {0}", sizeof(char));
            Console.WriteLine("Size of C# short is: {0}", sizeof(short));
            Console.WriteLine("Size of C# int is: {0}", sizeof(int));
            Console.WriteLine("Size of C# long is: {0}", sizeof(long));
            Console.WriteLine("Size of Operator: {0}", Marshal.SizeOf(typeof(Operator)));
            Console.WriteLine("Size of DigitBoolean: {0}", Marshal.SizeOf(typeof(DigitBoolean)));
            Console.WriteLine("Size of Seconds: {0}", Marshal.SizeOf(typeof(Second)));
            Console.WriteLine("Size of NullableDateTime: {0}", Marshal.SizeOf(typeof(NullableDateTime)));
            Console.WriteLine("Size of iOSEnvironment: {0}", Marshal.SizeOf(typeof(iOSEnvironment)));
            Console.WriteLine("Size of iOSEnvironmentV3: {0}", Marshal.SizeOf(typeof(iOSEnvironmentV3)));
        }

        [TestMethod]
        public void TestMethod2()
        {
            var clickAction = new ClickAction
            {
                ActionType = ClickAction.Type.Activity,
                Activity = "qwer2",
                ConfirmUrl = true,
                Intent = "dsagdsag",
                Url = "dsahggdsah://1215"
            };

            var json1 = clickAction.ToString();
            var json2 = clickAction.ToDictionary().Serialize();

            Assert.IsTrue(json1 == json2, "(1)json1 == json2");
            Console.WriteLine(nameof(json1));
            Console.WriteLine(json1);
            Console.WriteLine();
            Console.WriteLine(nameof(json2));
            Console.WriteLine(json2);

            var jsonObj = json2.Deserialize<ClickAction>();
            json2 = jsonObj.Serialize();

            Assert.IsTrue(json1 == json2, "(2)json1 == json2");
        }

        [TestMethod]
        public void TestMethod3()
        {
            var resultCode = XingePushClientResultCode.内部错误;
            var errMsg = nameof(TestMethod3);
            var testJsonString =
                "{\"ret_code\":0,\"err_msg\":\"\",\"result\":{\"push_id\":10000,\"sub_push_ids\":[234,235,236]}}";
            var jObject = JObject.Parse(testJsonString);
            var jToken = jObject.GetValue(Constants.result, StringComparison.OrdinalIgnoreCase);

            var p0 = new XingePushClientResult
            {
                ResultCode = resultCode,
                ErrMsg = errMsg,
            };

            p0.SetResult(jToken);

            var p1 = new XingePushClientResult<TResult>
            {
                ResultCode = resultCode,
                ErrMsg = errMsg,
            };

            p1.SetResult(jToken);

            var s0 = p0.Serialize();
            var s1 = p1.Serialize();

            WriteLine(nameof(s0), s0);
            WriteLine(nameof(s1), s1);

            Assert.IsTrue(s0 == s1, "s0 == s1");

            var d0 = s0.Deserialize<XingePushClientResult>();
            var d1 = s1.Deserialize<XingePushClientResult<TResult>>();

            var ns0 = d0.Serialize();
            var ns1 = d1.Serialize();

            WriteLine(nameof(ns0), s0);
            WriteLine(nameof(ns1), s1);

            Assert.IsTrue(ns0 == ns1, "ns0 == ns1");

            Assert.IsTrue(ns0 == s0, "ns0 == s0");
        }

        [TestMethod]
        public void TestMethod5()
        {
            var version = XgPush.SDK.Server.Properties.AssemblyInfo.Version;

            var array = new[] { XingePushClientResultCode.APNS证书错误, XingePushClientResultCode.Sign不合法 };
            var p0 = new ResultCodesResult(array);
            var jsonString = p0.Serialize();

            var d0 = jsonString.Deserialize<ResultCodesResult>();
            var d1 = jsonString.Deserialize<List<XingePushClientResultCode>>();

            var s0 = d0.Serialize();
            var s1 = d1.Serialize();

            var b0 = s0 == s1 && s1 == jsonString;
            Assert.IsTrue(b0, "s0 == s1 && s1 == jsonString");

            WriteLine(nameof(jsonString), jsonString);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var testJsonStrings = new[] {
                "{\"Total\":\"1\",\"list\":{\"0\":{\"Content\":\"test\",\"OfflineSave\":\"86400\",\"ScheduleSendTime\":\"2017-04-12 17:50:00\",\"SendTime\":\"2017-04-12 17:50:01\",\"TagsList\":\"\",\"Title\":\"this is title\",\"Type\":\"3\",\"cleanup\":\"0\",\"click\":\"0\",\"create_time\":\"2017-04-12 17:49:01\",\"push_active\":\"0\",\"push_id\":\"2511161036\",\"push_online\":\"0\",\"start_time\":\"2017-04-12 17:50:01\",\"status\":\"2\",\"verify\":\"123\",\"verify_svc\":\"0\",\"cal_type\":\"0\"}}}",
                "{\"Total\":\"0\",\"list\":{}}",
                "{\"Total\":\"0\"}",
                "{\"Total\":\"0\",\"list\":null}",
                "{\"Total\":\"2\",\"list\":{\"0\":{\"Content\":\"test\",\"OfflineSave\":\"86400\",\"ScheduleSendTime\":\"2017-04-12 17:50:00\",\"SendTime\":\"2017-04-12 17:50:01\",\"TagsList\":\"\",\"Title\":\"this is title\",\"Type\":\"3\",\"cleanup\":\"0\",\"click\":\"0\",\"create_time\":\"2017-04-12 17:49:01\",\"push_active\":\"0\",\"push_id\":\"2511161036\",\"push_online\":\"0\",\"start_time\":\"2017-04-12 17:50:01\",\"status\":\"2\",\"verify\":\"123\",\"verify_svc\":\"0\",\"cal_type\":\"0\"},\"1\":{\"Content\":\"test\",\"OfflineSave\":\"86400\",\"ScheduleSendTime\":\"2017-04-12 17:50:00\",\"SendTime\":\"2017-04-12 17:50:01\",\"TagsList\":\"\",\"Title\":\"this is title\",\"Type\":\"3\",\"cleanup\":\"0\",\"click\":\"0\",\"create_time\":\"2017-04-12 17:49:01\",\"push_active\":\"0\",\"push_id\":\"2511161036\",\"push_online\":\"0\",\"start_time\":\"2017-04-12 17:50:01\",\"status\":\"2\",\"verify\":\"123\",\"verify_svc\":\"0\",\"cal_type\":\"0\"}}}"
            };

            for (int i = 0; i < testJsonStrings.Length; i++)
            {
                Console.WriteLine(Environment.NewLine);
                WriteLine(i.ToString(), Environment.NewLine);

                var testJsonString = testJsonStrings[i];

                JToken jToken = JObject.Parse(testJsonString);

                var p0 = new QueryPushStatusResult();

                p0.Init(jToken);

                var s0 = p0.Serialize();

                WriteLine(nameof(s0), s0);

                var d0 = s0.Deserialize<QueryPushStatusResult>();

                var ns0 = d0.Serialize();

                WriteLine(nameof(ns0), ns0);

                Assert.IsTrue(ns0 == s0, $"({testJsonString})ns0 == s0");
            }
        }

        private void WriteLine(string name, string value)
        {
            Console.WriteLine($"[{name}]:{Environment.NewLine}{value}");
        }
    }

    public class TResult : IResultV2
    {
        public uint PushId { get; set; }

        public void Init(JToken jToken)
        {
            var p0 = jToken?[Constants.push_id];
            if (p0 != null && !p0.HasValues)
            {
                PushId = p0.Value<uint>();
            }
        }
    }
}