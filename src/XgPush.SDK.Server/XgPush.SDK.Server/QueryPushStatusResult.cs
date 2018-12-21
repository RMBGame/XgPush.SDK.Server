using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    [JsonObject]
    public class QueryPushStatusResult : BaseSerializeObject<QueryPushStatusResult>, IResultV2, IDictionarySerialize,
#if NET40
        IEnumerable<QueryPushStatusResult.ItemResult>
#else
        IReadOnlyList<QueryPushStatusResult.ItemResult>
#endif
    {
        /// <summary>
        ///
        /// </summary>
        public List<ItemResult> Values { get; set; } = new List<ItemResult>();

        /// <summary>
        ///
        /// </summary>
        public int Count => Values.Count;

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ItemResult this[int index] => Values[index];

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public virtual void Init(JToken jToken)
        {
            JToken list;
            var countString = jToken["Total"];
            list = jToken[nameof(list)];
            if (countString != null && !countString.HasValues && list != null && list.HasValues && list.Type == JTokenType.Object)
            {
                var count = countString.Value<string>().TryParseInt32();
                if (count.HasValue)
                {
                    var values = new ItemResult[count.Value];
                    for (var i = 0; i < values.Length; i++)
                    {
                        var item = list[i.ToString()];
                        if (item != null && item.HasValues && item.Type == JTokenType.Object)
                        {
                            var item_ = new ItemResult();
                            item_.Init(item);
                            values[i] = item_;
                        }
                    }
                    Values.Clear();
                    Values.AddRange(values);
                }
            }
        }

        IEnumerator<ItemResult> IEnumerable<ItemResult>.GetEnumerator()
        {
            return ((IEnumerable<ItemResult>)Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<ItemResult>)Values).GetEnumerator();
        }

        private Dictionary<string, object> _Values
        {
            get
            {
                var keyValuePairs = new Dictionary<string, object>();
                for (int i = 0; i < Values.Count; i++)
                {
                    keyValuePairs.Add(i.ToString(), Values[i]);
                }
                return keyValuePairs;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionarySerialize()
            => new Dictionary<string, object> { { "Total", Count.ToString() }, { "list", _Values }, };

        /// <summary>
        ///
        /// </summary>
        public QueryPushStatusResult() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resultCodes"></param>
        public QueryPushStatusResult(IEnumerable<ItemResult> resultCodes)
        {
            if (resultCodes is List<ItemResult> list)
            {
                Values = list;
            }
            else
            {
                Values = resultCodes.ToList();
            }
        }

        /// <summary>
        ///
        /// </summary>
        public class ItemResult : IResultV2
        {
            /// <summary>
            /// 推送消息体
            /// </summary>
            [JsonProperty("Content")]
            public string Content { get; set; }

            /// <summary>
            /// Android 离线保存时间
            /// </summary>
            [JsonProperty("OfflineSave")]
            public string OfflineSave { get; set; }

            /// <summary>
            /// Android 离线保存时间
            /// </summary>
            [JsonIgnore]
            public int? OfflineSave_Int32 { get; private set; }

            /// <summary>
            /// ScheduleSendTime
            /// </summary>
            [JsonProperty("ScheduleSendTime")]
            public string ScheduleSendTime { get; set; }

            /// <summary>
            /// ScheduleSendTime
            /// </summary>
            [JsonIgnore]
            public DateTime? ScheduleSendTime_DateTime { get; private set; }

            /// <summary>
            /// 根据算法从tobe_sent_tome、creat_time\start_time中选择
            /// </summary>
            [JsonProperty("SendTime")]
            public string SendTime { get; set; }

            /// <summary>
            /// 根据算法从tobe_sent_tome、creat_time\start_time中选择
            /// </summary>
            [JsonIgnore]
            public DateTime? SendTime_DateTime { get; private set; }

            /// <summary>
            /// 标签时的标签列表
            /// </summary>
            [JsonProperty("TagsList")]
            public string TagsList { get; set; }

            /// <summary>
            /// 推送标题
            /// </summary>
            [JsonProperty("Title")]
            public string Title { get; set; }

            /// <summary>
            /// 任务类型：3-全推、4-标签推送
            /// </summary>
            [JsonProperty("Type")]
            public string Type { get; set; }

            /// <summary>
            /// 任务类型：3-全推、4-标签推送
            /// </summary>
            [JsonIgnore]
            public int? Type_Int32 { get; private set; }

            /// <summary>
            /// 清除
            /// </summary>
            [JsonProperty("cleanup")]
            public string Cleanup { get; set; }

            /// <summary>
            /// 清除
            /// </summary>
            [JsonIgnore]
            public int? Cleanup_Int32 { get; private set; }

            /// <summary>
            /// 点击
            /// </summary>
            [JsonProperty("click")]
            public string Click { get; set; }

            /// <summary>
            /// 点击
            /// </summary>
            [JsonIgnore]
            public int? Click_Int32 { get; private set; }

            /// <summary>
            /// 任务创建时间
            /// </summary>
            [JsonProperty("create_time")]
            public string CreateTime { get; set; }

            /// <summary>
            /// 任务创建时间
            /// </summary>
            [JsonIgnore]
            public DateTime? CreateTime_DateTime { get; private set; }

            /// <summary>
            /// Android最近30天活跃设备
            /// </summary>
            [JsonProperty("push_active")]
            public string PushActive { get; set; }

            /// <summary>
            /// Android最近30天活跃设备
            /// </summary>
            [JsonIgnore]
            public int? PushActive_Int32 { get; private set; }

            /// <summary>
            /// push_id
            /// </summary>
            [JsonProperty("push_id")]
            public string PushId { get; set; }

            /// <summary>
            /// push_id
            /// </summary>
            [JsonIgnore]
            public uint? PushId_UInt32 { get; private set; }

            /// <summary>
            /// push_online
            /// </summary>
            [JsonProperty("push_online")]
            public string PushOnline { get; set; }

            /// <summary>
            /// push_online
            /// </summary>
            [JsonIgnore]
            public int? PushOnline_Int32 { get; private set; }

            /// <summary>
            /// 任务开始推送时间
            /// </summary>
            [JsonProperty("start_time")]
            public string StartTime { get; set; }

            /// <summary>
            /// 任务开始推送时间
            /// </summary>
            [JsonIgnore]
            public DateTime? StartTime_DateTime { get; private set; }

            /// <summary>
            /// 0-创建任务，1-推送中，2-推送完成，4-非法任务，3-推送失败，9-内容重复，10-频率控制，11-删除离线消息，12-用户取消推送
            /// </summary>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// 0-创建任务，1-推送中，2-推送完成，4-非法任务，3-推送失败，9-内容重复，10-频率控制，11-删除离线消息，12-用户取消推送
            /// </summary>
            [JsonIgnore]
            public int? Status_Int32 { get; private set; }

            /// <summary>
            /// 展示
            /// </summary>
            [JsonProperty("verify")]
            public string Verify { get; set; }

            /// <summary>
            /// 到达设备数量
            /// </summary>
            [JsonProperty("verify_svc")]
            public string VerifySvc { get; set; }

            /// <summary>
            /// 到达设备数量
            /// </summary>
            [JsonIgnore]
            public int? VerifySvc_Int32 { get; private set; }

            /// <summary>
            /// cal_type
            /// </summary>
            [JsonProperty("cal_type")]
            public string CalType { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <param name="jToken"></param>
            public void Init(JToken jToken)
            {
                Content = jToken[nameof(Content)]?.Value<string>();
                OfflineSave = jToken[nameof(OfflineSave)]?.Value<string>();
                OfflineSave_Int32 = OfflineSave.TryParseInt32();
                ScheduleSendTime = jToken[nameof(ScheduleSendTime)]?.Value<string>();
                ScheduleSendTime_DateTime = ScheduleSendTime.TryParseDateTime();
                SendTime = jToken[nameof(SendTime)]?.Value<string>();
                SendTime_DateTime = SendTime.TryParseDateTime();
                TagsList = jToken[nameof(TagsList)]?.Value<string>();
                Title = jToken[nameof(Title)]?.Value<string>();
                Type = jToken[nameof(Type)]?.Value<string>();
                Type_Int32 = Type.TryParseInt32();
                Cleanup = jToken["cleanup"]?.Value<string>();
                Cleanup_Int32 = Cleanup.TryParseInt32();
                Click = jToken["click"]?.Value<string>();
                Click_Int32 = Click.TryParseInt32();
                CreateTime = jToken["create_time"]?.Value<string>();
                CreateTime_DateTime = CreateTime.TryParseDateTime();
                PushActive = jToken["push_active"]?.Value<string>();
                PushActive_Int32 = PushActive.TryParseInt32();
                PushId = jToken["push_id"]?.Value<string>();
                PushId_UInt32 = PushId.TryParseUInt32();
                PushOnline = jToken["push_online"]?.Value<string>();
                PushOnline_Int32 = PushOnline.TryParseInt32();
                StartTime = jToken["start_time"]?.Value<string>();
                StartTime_DateTime = StartTime.TryParseDateTime();
                Status = jToken["status"]?.Value<string>();
                Status_Int32 = Status.TryParseInt32();
                Verify = jToken["verify"]?.Value<string>();
                VerifySvc = jToken["verify_svc"]?.Value<string>();
                VerifySvc_Int32 = VerifySvc.TryParseInt32();
                CalType = jToken["cal_type"]?.Value<string>();
            }
        }
    }
}