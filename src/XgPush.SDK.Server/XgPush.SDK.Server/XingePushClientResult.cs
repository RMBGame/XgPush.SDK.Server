using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// (通用基础返回值) <see cref="T:XgPush.SDK.Server.Results.IXingePushClientResult" /> http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E9%80%9A%E7%94%A8%E5%9F%BA%E7%A1%80%E8%BF%94%E5%9B%9E%E5%80%BC
    /// <para>{&quot;ret_code&quot;:0,&quot;err_msg&quot;:&quot;&quot;,&quot;result&quot;:{&quot;&quot;:&quot;&quot;}}</para>
    /// </summary>
    public class XingePushClientResult : XingePushClientResultBase<JToken>
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(Constants.result)]
        public override JToken Result
        {
            get => base.Result;
            set => base.Result = value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public override void SetResult(JToken jToken) => Result = jToken;

        /// <summary>
        ///
        /// </summary>
        public XingePushClientResult() : base() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="errMsg"></param>
        public XingePushClientResult(XingePushClientResultCode? resultCode, string errMsg) : base(resultCode, errMsg) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="raw"></param>
        public XingePushClientResult(string raw) : base(raw) { }
    }

    /// <summary>
    /// (通用基础返回值) <see cref="T:XgPush.SDK.Server.Results.IXingePushClientResult" /> http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E9%80%9A%E7%94%A8%E5%9F%BA%E7%A1%80%E8%BF%94%E5%9B%9E%E5%80%BC
    /// <para>{&quot;ret_code&quot;:0,&quot;err_msg&quot;:&quot;&quot;,&quot;result&quot;:{&quot;&quot;:&quot;&quot;}}</para>
    /// </summary>
    /// <typeparam name="TResult">请求正确时且有额外数据类型。</typeparam>
    public class XingePushClientResult<TResult> : XingePushClientResult where TResult : IResultV2, new()
    {
        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public new TResult Result { get; set; }

        /// <summary>
        ///
        /// </summary>
        public override bool IsSuccess
        {
            get
            {
                if (Result != null && Result is IsSuccess @is && base.IsSuccess && @is.IsSuccess())
                    return true;
                return base.IsSuccess;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public override void SetResult(JToken jToken)
        {
            base.Result = jToken;
            var result = new TResult();
            result.Init(jToken);
            Result = result;
        }

        /// <summary>
        ///
        /// </summary>
        public XingePushClientResult() : base() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="errMsg"></param>
        public XingePushClientResult(XingePushClientResultCode? resultCode, string errMsg) : base(resultCode, errMsg) { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="raw"></param>
        public XingePushClientResult(string raw) : base(raw) { }
    }
}

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public interface IXingePushClientResultV2
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="jObject"></param>
        void Init(JObject jObject);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class XingePushClientResultBase<TResult> : BaseSerializeObject, IRaw, IsSuccess, IXingePushClientResultV2
    {
        /// <summary>
        /// 返回码。
        /// </summary>
        [JsonProperty(Constants.ret_code)]
        public XingePushClientResultCode ResultCode { get; set; }

        /// <summary>
        /// 结果描述。
        /// </summary>
        [JsonProperty(Constants.err_msg)]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 请求正确时且有额外数据，则结果封装在该字段中。
        /// </summary>
        public virtual TResult Result { get; set; }

        /// <summary>
        /// <see cref="ResultCode"/>(返回码) 与 <see cref="Result"/>(result) 是否成功。
        /// </summary>
        [JsonIgnore]
        public virtual bool IsSuccess => ResultCode.IsSuccess();

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public string Raw { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jObject"></param>
        public virtual void Init(JObject jObject)
        {
            if (!jObject.TryGetValue(Constants.ret_code, StringComparison.OrdinalIgnoreCase, out var retCode))
                throw new ArgumentException(Constants.ret_code + Environment.NewLine
                    + jObject.ToString(Formatting.None));
            ResultCode = (XingePushClientResultCode)retCode.Value<int>();
            if (jObject.TryGetValue(Constants.err_msg, StringComparison.OrdinalIgnoreCase, out var errMsg))
                ErrMsg = errMsg.Value<string>();
            if (!jObject.TryGetValue(Constants.result, StringComparison.OrdinalIgnoreCase, out var ret))
                return;
            SetResult(ret);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        public abstract void SetResult(JToken jToken);

        /// <summary>
        ///
        /// </summary>
        public XingePushClientResultBase()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="errMsg"></param>
        public XingePushClientResultBase(XingePushClientResultCode? resultCode, string errMsg)
        {
            ResultCode = resultCode ?? XingePushClientResultCode.参数错误;
            ErrMsg = errMsg;
            Raw = "{\"ret_code\":" + ResultCode + ",\"err_msg\":\"" + ErrMsg + "\"}";
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="raw"></param>
        public XingePushClientResultBase(string raw)
        {
            Raw = raw;
        }

        bool IsSuccess.IsSuccess() => IsSuccess;
    }
}