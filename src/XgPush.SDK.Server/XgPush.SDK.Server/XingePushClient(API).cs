using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using XgPush.SDK.Server.Internal;
using d = System.Collections.Generic.Dictionary<string, object>;
using p = System.Collections.Generic.KeyValuePair<string, object>;

namespace XgPush.SDK.Server
{
    public partial class XingePushClient
    {
        /// <summary>
        /// 账号集合最大总数限制(帐号群推)。
        /// <para>单次发送account不超过100个。</para>
        /// </summary>
        public const int SendSingleAccountsMaxCount = 100;

        /// <summary>
        /// 超大批量集合最大总数限制(超大批量群推)。
        /// <para>单次发送account不超过1000个。</para>
        /// <para>单次发送token不超过1000个。</para>
        /// </summary>
        public const int SendSingleMultipleMaxCount = 1000;

        /// <summary>
        /// 标签组最大总数限制(标签群推)
        /// <para>限制不能超过50个，否则消息将下发失败，若超过50个，推荐使用全量接口推送消息。</para>
        /// </summary>
        public const int SendSingleTagsMaxCount = 50;

        #region 1.7.2. 全量推送 此接口用于对全部设备推送消息，后台对本接口的调用频率有限制: 1小时内不能推相同内容的消息； 1小时内最多调用此接口30次

        /// <summary>
        /// 全量推送(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%85%A8%E9%87%8F%E6%8E%A8%E9%80%81)。
        /// <para>此接口用于对全部设备推送消息，后台对本接口的调用频率有限制：</para>
        /// <para>- 1小时内不能推相同内容的消息。</para>
        /// <para>- 1小时内最多调用此接口30次。</para>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<XingePushClientResult<SubPushIdsResult>> PushAllDevice([NotNull]MessageBase message)
        {
            ValidationUtils.ArgumentNotNull(message, nameof(message));
            return GetAsync<SubPushIdsResult>(RESTAPI_PUSHALLDEVICE, message);
        }

        #endregion

        #region 1.7.3. 群推送，根据标签、账号、设备(Token)进行群组推送 http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E7%BE%A4%E6%8E%A8%E9%80%81

        /// <summary>
        /// 标签群推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E7%BE%A4%E6%8E%A8%E9%80%81)。
        /// <para>可以针对设置过标签的设备进行推送。如：性别、身份等。</para>
        /// <para>注意：单次发送tag不超过50个(<see cref="SendSingleTagsMaxCount"/>)个，若超过50个，推荐使用全量接口推送消息。</para>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="tags"></param>
        /// <param name="operators">运算符，取值为 <see cref="Operator.AND"/> 或 <see cref="Operator.OR"/> ，默认值为 <see langword="null"/>(<see cref="Operator.OR"/>)。</param>
        /// <returns></returns>
        public Task<XingePushClientResult<SubPushIdsResult>> PushTags(
            [NotNull]MessageBase message, [NotNull]ICollection<string> tags, Operator? operators = null)
        {
            ValidationUtils.ArgumentNotNull(message, nameof(message));
            ValidationUtils.ArgumentNotNull(tags, nameof(tags));
            if (tags.Count > SendSingleTagsMaxCount) throw new ArgumentOutOfRangeException(nameof(tags));
            return GetAsync<SubPushIdsResult>(RESTAPI_PUSHTAGS, message,
                new p(Constants.tags_list, tags),
                new p(Constants.tags_op, operators ?? Operator.OR));
        }

        /// <summary>
        /// 帐号群推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E7%BE%A4%E6%8E%A8%E9%80%81)。
        /// <para>账号群推是指，对通过客户端SDK绑定接口绑定的账号的群组推送，iOS和Android的SDK都提供相应的接口。</para>
        /// <para>注意：单次发送account不超过100个(<see cref="SendSingleAccountsMaxCount"/>)个。</para>
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<XingePushClientResult<ResultCodesResult>> PushAccounts([NotNull]MessageBase message, [NotNull]ICollection<string> accounts)
        {
            ValidationUtils.ArgumentNotNull(message, nameof(message));
            ValidationUtils.ArgumentNotNull(accounts, nameof(accounts));
            if (accounts.Count > SendSingleAccountsMaxCount) throw new ArgumentOutOfRangeException(nameof(accounts));
            return GetAsync<ResultCodesResult>(RESTAPI_PUSHACCOUNTLIST, message,
                new p(Constants.account_list, accounts));
        }

        /// <summary>
        /// 超大批量账号群推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E7%BE%A4%E6%8E%A8%E9%80%81)。
        /// <para>如果推送目标帐号数量很大（比如>100），推荐使用此方法，分为以下两步：</para>
        /// <para>√(1)第一步，创建推送消息。</para>
        /// <para>(2)第二步，使用超大批量推送接口进行消息推送。</para>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<XingePushClientResult<PushIdResult>> CreateMultiPush([NotNull]MessageBase message)
        {
            ValidationUtils.ArgumentNotNull(message, nameof(message));
            return GetAsync<PushIdResult>(RESTAPI_CREATEMULTIPUSH, message);
        }

        /// <summary>
        /// 超大批量[账号]群推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E7%BE%A4%E6%8E%A8%E9%80%81)。
        /// <para>注意：单次发送account不超过1000(<see cref="SendSingleMultipleMaxCount"/>)个。</para>
        /// <para>如果推送目标帐号数量很大（比如>100），推荐使用此方法，分为以下两步：</para>
        /// <para>(1)第一步，创建推送消息。</para>
        /// <para>√(2)第二步，使用超大批量推送接口进行消息推送。</para>
        /// </summary>
        /// <param name="pushId"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        public Task<XingePushClientResult> PushAccounts(uint pushId, [NotNull]ICollection<string> accounts)
        {
            ValidationUtils.ArgumentNotNull(accounts, nameof(accounts));
            if (pushId == 0) throw new ArgumentOutOfRangeException(nameof(pushId));
            if (accounts.Count > SendSingleMultipleMaxCount) throw new ArgumentOutOfRangeException(nameof(accounts));
            return GetAsync(RESTAPI_PUSHACCOUNTLISTMULTIPLE,
                    new p(Constants.push_id, pushId),
                    new p(Constants.account_list, accounts));
        }

        /// <summary>
        /// 超大批量[设备]群推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E7%BE%A4%E6%8E%A8%E9%80%81)。
        /// <para>注意：单次发送token不超过1000(<see cref="SendSingleMultipleMaxCount"/>)个。</para>
        /// <para>如果设备目标帐号数量很大（比如>100），推荐使用此方法，分为以下两步：</para>
        /// <para>(1)第一步，创建推送消息。</para>
        /// <para>√(2)第二步，使用超大批量推送接口进行消息推送。</para>
        /// </summary>
        /// <param name="pushId"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public Task<XingePushClientResult> PushDevices(uint pushId, [NotNull]ICollection<string> devices)
        {
            ValidationUtils.ArgumentNotNull(devices, nameof(devices));
            if (pushId == 0) throw new ArgumentOutOfRangeException(nameof(pushId));
            if (devices.Count > SendSingleMultipleMaxCount) throw new ArgumentOutOfRangeException(nameof(devices));
            return GetAsync(RESTAPI_PUSHDEVICELISTMULTIPLE,
                new p(Constants.push_id, pushId),
                new p(Constants.device_list, devices));
        }

        #endregion

        #region 1.7.4. 单推，指定账号，设备标识（Device Token）进行推送 http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%8D%95%E6%8E%A8

        /// <summary>
        /// 账号单推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%8D%95%E6%8E%A8)。
        /// <para>账号单推是指，对通过客户端SDK绑定接口绑定的指定单个账号的推送，iOS和Android的SDK都提供相应的接口。</para>
        /// </summary>
        /// <param name="account"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<XingePushClientResult> PushAccount([NotNull]MessageBase message, [NotNull]string account)
        {
            ValidationUtils.ArgumentNotNull(account, nameof(account));
            ValidationUtils.ArgumentNotNull(message, nameof(message));
            return GetAsync(RESTAPI_PUSHSINGLEACCOUNT, message,
                new p(Constants.account, account));
        }

        /// <summary>
        /// 设备单推(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%8D%95%E6%8E%A8)。
        /// <para>设备单推是指，使用指定的一个设备标识(Device Token)进行消息的推送，关于设备标识的获取，客户端SDK有相应的接口。</para>
        /// </summary>
        /// <param name="device_token"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<XingePushClientResult> PushDevice([NotNull]MessageBase message, [NotNull]string device_token)
        {
            ValidationUtils.ArgumentNotNull(device_token, nameof(device_token));
            ValidationUtils.ArgumentNotNull(message, nameof(message));
            return GetAsync(RESTAPI_PUSHSINGLEACCOUNT, message,
                new p(Constants.device_token, device_token));
        }

        #endregion

        #region 1.7.5. 消息体格式

        /**
         * http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%B6%88%E6%81%AF%E4%BD%93%E6%A0%BC%E5%BC%8F
         */

        #endregion

        #region 1.7.6. 查询消息状态

        public const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        ///
        /// </summary>
        /// <param name="pushIds"></param>
        /// <returns></returns>
        protected virtual string GetPushIds(ICollection<string> pushIds)
            => new JArray(pushIds.Select(x => new { push_id = x })).ToString(Formatting.None);

        /// <summary>
        ///
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected virtual string ToDateFormat(DateTime dt) => dt.ToString(DateFormat);

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected virtual bool TryParseDateFormat(string s, out DateTime dt)
            => DateTime.TryParseExact(s, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

        /// <summary>
        /// 查询消息状态(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%9F%A5%E8%AF%A2%E6%B6%88%E6%81%AF%E7%8A%B6%E6%80%81)。
        /// <para>此接口目前仅支持全量推送和标签群推消息的发送状态的查询，不支持其他推送方式的查询。</para>
        /// </summary>
        /// <param name="pushIds">必需：是，消息唯一标识，可在管理台查看。</param>
        /// <returns></returns>
        public Task<XingePushClientResult<QueryPushStatusResult>> QueryPushStatus([NotNull] ICollection<string> pushIds)
        {
            ValidationUtils.ArgumentNotNull(pushIds, nameof(pushIds));
            return GetAsync<QueryPushStatusResult>(RESTAPI_QUERYPUSHSTATUS,
                new p(Constants.push_ids, GetPushIds(pushIds)));
        }

        /// <summary>
        /// 查询消息状态(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%9F%A5%E8%AF%A2%E6%B6%88%E6%81%AF%E7%8A%B6%E6%80%81)。
        /// <para>此接口目前仅支持全量推送和标签群推消息的发送状态的查询，不支持其他推送方式的查询。</para>
        /// </summary>
        /// <param name="pushIds">必需：是，消息唯一标识，可在管理台查看。</param>
        /// <param name="start">必需：是，起始记录id，分页查询使用。</param>
        /// <param name="length">必需：是，查询记录条数。</param>
        /// <param name="type">必需：是，推送消息类型：
        /// <para>- 1=通知栏消息</para>
        /// <para>- 2=透传消息(应用内消息)</para></param>
        /// <param name="push_type">必需：否，方式：WEB(1)/restAPI(2)/all(3)。</param>
        /// <param name="task_type">必需：否，推送取值范围：
        /// <para>- 0=所有范围（设备、账号、标签）</para>
        /// <para>- 1=设备</para>
        /// <para>- 2=帐号</para>
        /// <para>- 3=标签</para>
        /// </param>
        /// <param name="platform">必需：否，平台：Android(1)/iOS(2)。</param>
        /// <param name="start_date">必需：否，开始时间，格式： yyyy-MM-dd。</param>
        /// <param name="end_date">必需：否，结束时间，格式： yyyy-MM-dd。</param>
        /// <param name="status">必需：否，状态：
        /// <para>- 0=所有状态</para>
        /// <para>- 1=待推送</para>
        /// <para>- 2=推送中</para>
        /// <para>- 3=推送完成</para>
        /// <para>- 4=推送失败</para>
        /// <para>- 5=非法任务</para>
        /// <para>- 6=其他状态</para>
        /// </param>
        /// <param name="message">必需：否，根据消息内容进行模糊查找。</param>
        /// <param name="operation">必需：否，建议取值 1。</param>
        /// <returns></returns>
        public Task<XingePushClientResult<QueryPushStatusResult>> QueryPushStatus(
            [NotNull]ICollection<string> pushIds,
            int start,
            int length,
            int type,
            int? push_type = null,
            int? task_type = null,
            Platform? platform = null,
            DateTime? start_date = null,
            DateTime? end_date = null,
            int? status = null,
            string message = null,
            int? operation = null)
        {
            if (pushIds == null) throw new ArgumentNullException(nameof(pushIds));
            var param = new Dictionary<string, object>
            {
                { Constants.push_ids, GetPushIds(pushIds) },
                { nameof(start), start },
                { nameof(length), length },
                { nameof(type), type },
            };
            if (push_type.HasValue)
                param.Add(nameof(push_type), push_type.Value);
            if (task_type.HasValue) // task_type 类型(string)
                param.Add(nameof(task_type), task_type.Value.ToString());
            if (platform.HasValue)
                param.Add(nameof(platform), (int)platform.Value);
            if (start_date.HasValue)
                param.Add(nameof(start_date), ToDateFormat(start_date.Value));
            if (end_date.HasValue)
                param.Add(nameof(end_date), ToDateFormat(end_date.Value));
            if (status.HasValue)
                param.Add(nameof(status), status.Value);
            if (!string.IsNullOrEmpty(message))
                param.Add(nameof(message), message);
            if (operation.HasValue)
                param.Add(nameof(operation), operation.Value);
            return GetAsync<QueryPushStatusResult>(RESTAPI_QUERYPUSHSTATUS, param);
        }

        #endregion

        #region 1.7.7. 取消推送

        /// <summary>
        /// 取消推送(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%8F%96%E6%B6%88%E6%8E%A8%E9%80%81)。
        /// 目前V2版本支持根据消息ID来取消尚未触发的、定时的、全量推送或标签群推的推送消息。
        /// </summary>
        /// <param name="push_id">已创建的全量推送或标签群推消息的唯一标识，信鸽管理台可以查看。</param>
        /// <returns></returns>
        public Task<XingePushClientResult<StatusResult>> CancelTimingPush([NotNull] string push_id)
        {
            ValidationUtils.ArgumentNotNull(push_id, nameof(push_id));
            return GetAsync<StatusResult>(RESTAPI_CANCELTIMINGPUSH,
                new p(Constants.push_id, push_id));
        }

        #endregion

        #region 1.8. 标签(Tag)接口 标签接口主要是用来对标签进行查询、设置、删除操作 http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3

        // V2版本支持的具体接口如下：

        #region 1.批量新增标签

        /// <summary>
        /// 批量新增标签(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3)。
        /// 批量新增标签，可以给多定的设备(Device Token)设置标签，但是一个 App 下面最多只能有1万个标签，若超出，此接口将返回失败。
        /// </summary>
        /// <param name="tagTokenPairs">每次调用最多允许设置20个。</param>
        /// <returns></returns>
        public Task<XingePushClientResult> BatchSetTag([NotNull] ICollection<TagTokenPair> tagTokenPairs)
        {
            ValidationUtils.ArgumentNotNull(tagTokenPairs, nameof(tagTokenPairs));
            return GetAsync(RESTAPI_BATCHSETTAG,
                new p(Constants.tag_token_list, tagTokenPairs.Select(x => x.ToArray())));
        }

        /// <summary>
        /// 批量新增标签(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3)。
        /// 批量新增标签，可以给多定的设备(Device Token)设置标签，但是一个 App 下面最多只能有1万个标签，若超出，此接口将返回失败。
        /// </summary>
        /// <param name="tagTokenPairs">每次调用最多允许设置20个。</param>
        /// <returns></returns>
        public Task<XingePushClientResult> BatchSetTag([NotNull] params TagTokenPair[] tagTokenPairs)
        {
            ICollection<TagTokenPair> collection = tagTokenPairs;
            return BatchSetTag(collection);
        }

        #endregion

        #region 2.批量删除标签

        /// <summary>
        /// 批量删除标签(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3)。
        /// </summary>
        /// <param name="tagTokenPairs">每次调用最多允许设置20个。</param>
        /// <returns></returns>
        public Task<XingePushClientResult> BatchDelTag([NotNull] ICollection<TagTokenPair> tagTokenPairs)
        {
            ValidationUtils.ArgumentNotNull(tagTokenPairs, nameof(tagTokenPairs));
            return GetAsync(RESTAPI_BATCHDELTAG,
                new p(Constants.tag_token_list, tagTokenPairs.Select(x => x.ToArray())));
        }

        /// <summary>
        /// 批量删除标签(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3)。
        /// </summary>
        /// <param name="tagTokenPairs">每次调用最多允许设置20个。</param>
        /// <returns></returns>
        public Task<XingePushClientResult> BatchDelTag([NotNull] params TagTokenPair[] tagTokenPairs)
        {
            ICollection<TagTokenPair> collection = tagTokenPairs;
            return BatchDelTag(collection);
        }

        #endregion

        #region 3.查询全部标签

        /// <summary>
        /// 查询全部标签(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3)。
        /// <para>此接口用来查询当前指定应用下被设置的全部标签数量和对应的标签名。</para>
        /// </summary>
        /// <param name="start">默认值：0，开始索引值。</param>
        /// <param name="limit">默认值：100，限制单次查询数量，建议小于200。</param>
        /// <returns></returns>
        public Task<XingePushClientResult<QueryTotalTagsResult>> QueryTags(uint start = 0, uint limit = 100)
            => GetAsync<QueryTotalTagsResult>(RESTAPI_QUERYTAGS,
                new d
                {
                    { nameof(start), start },
                    { nameof(limit), limit },
                });

        #endregion

        #region 4.查询单个设备(根据Device Token)的标签

        /// <summary>
        /// 查询单个指定设备的标签(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E6%A0%87%E7%AD%BEtag%E6%8E%A5%E5%8F%A3)。
        /// 此接口根据设备标识(Device Token)来查询相应设备被设置的全部标签，请务必保证设备标识（Device Token）的合法性。
        /// </summary>
        /// <param name="device_token">设备接收消息的标识。</param>
        /// <returns></returns>
        public Task<XingePushClientResult<QueryTagsResult>> QueryTokenTags([NotNull]string device_token)
        {
            ValidationUtils.ArgumentNotNull(device_token, nameof(device_token));
            return GetAsync<QueryTagsResult>(RESTAPI_QUERYTOKENTAGS,
                new p(nameof(device_token), device_token));
        }

        #endregion

        #region 5.查询单个标签的设备(Device Token)总数

        /// <summary>
        /// 查询单个指定标签的Token总数(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%B7%A5%E5%85%B7%E7%B1%BB%E6%8E%A5%E5%8F%A3)。
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Task<XingePushClientResult<QueryDeviceCountResult>> QueryTagTokenNum([NotNull]string tag)
        {
            ValidationUtils.ArgumentNotNull(tag, nameof(tag));
            return GetAsync<QueryDeviceCountResult>(RESTAPI_QUERYTAGTOKENNUM,
                new p(nameof(tag), tag));
        }

        #endregion

        #endregion

        #region 1.9. 账号(Account)接口 http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E8%B4%A6%E5%8F%B7account%E6%8E%A5%E5%8F%A3

        /// <summary>
        /// 查询单个账号关联的设备(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E8%B4%A6%E5%8F%B7account%E6%8E%A5%E5%8F%A3)。
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Obsolete(ObsoleteMessageAccount)]
        public Task<XingePushClientResult<TokensResult>> QueryTokensOfAccount([NotNull]string account)
        {
            ValidationUtils.ArgumentNotNull(account, nameof(account));
            return GetAsync<TokensResult>(RESTAPI_QUERYTOKENSOFACCOUNT,
                new p(Constants.account, account));
        }

        /// <summary>
        /// 删除单个账号关联的单个设备Token(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E8%B4%A6%E5%8F%B7account%E6%8E%A5%E5%8F%A3)。
        /// </summary>
        /// <param name="account"></param>
        /// <param name="device_token"></param>
        /// <returns></returns>
        [Obsolete(ObsoleteMessageAccount)]
        public Task<XingePushClientResult<TokensResult>> DeleteTokenOfAccount([NotNull]string account, [NotNull]string device_token)
        {
            ValidationUtils.ArgumentNotNull(account, nameof(account));
            ValidationUtils.ArgumentNotNull(device_token, nameof(device_token));
            return GetAsync<TokensResult>(RESTAPI_DELETETOKENOFACCOUNT,
                new p(Constants.account, account),
                new p(Constants.device_token, device_token));
        }

        /// <summary>
        /// 删除单个账号关联的全部设备Token(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E8%B4%A6%E5%8F%B7account%E6%8E%A5%E5%8F%A3)。
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Obsolete(ObsoleteMessageAccount)]
        public Task<XingePushClientResult> DeleteAllTokensOfAccount([NotNull]string account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return GetAsync(RESTAPI_DELETEALLTOKENSOFACCOUNT,
                new p(Constants.account, account));
        }

        #endregion

        #region 1.10. 工具类接口

        #region 1.10.1. 查询应用覆盖的设备Token总数

        /// <summary>
        /// 查询应用覆盖的设备Token总数(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%B7%A5%E5%85%B7%E7%B1%BB%E6%8E%A5%E5%8F%A3)。
        /// <para>此接口用来查询指定应用的全部已注册的设备标识(Device Token)的总数。</para>
        /// </summary>
        /// <returns></returns>
        [Obsolete(ObsoleteMessageUtil)]
        public Task<XingePushClientResult<QueryDeviceCountResult>> QueryDeviceCount()
            => GetAsync<QueryDeviceCountResult>(RESTAPI_QUERYDEVICECOUNT);

        #endregion

        #region 1.10.2. 查询指定设备Token的注册状态

        /// <summary>
        /// 查询指定设备Token的注册状态(http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E5%B7%A5%E5%85%B7%E7%B1%BB%E6%8E%A5%E5%8F%A3)。
        /// <para>此接口是为了查询指定设备(Device Token)在信鸽服务器上注册的状态，设备能收到信鸽推送的消息的首要条件是设备(Device Token)已经被注册到信鸽的后台，否则信鸽无法给指定设备下发消息的。</para>
        /// </summary>
        /// <param name="device_token"></param>
        /// <returns></returns>
        [Obsolete(ObsoleteMessageUtil)]
        public Task<XingePushClientResult<AppTokenInfoResult>> QueryTokenInfo([NotNull]string device_token)
        {
            ValidationUtils.ArgumentNotNull(device_token, nameof(device_token));
            return GetAsync<AppTokenInfoResult>(RESTAPI_QUERYINFOOFTOKEN,
                new p(nameof(device_token), device_token));
        }

        #endregion

        #endregion

        #region 1.11. 返回码一览 http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E8%BF%94%E5%9B%9E%E7%A0%81%E4%B8%80%E8%A7%88

        // 参考 XingePushClientResultCode

        #endregion

        private const string ObsoleteMessageAccount =
            "1.9. 账号(Account)接口（v2不可用，请使用v3）" +
            "https://xg.qq.com/docs/server_api/v2/rest.html#%E8%B4%A6%E5%8F%B7account%E6%8E%A5%E5%8F%A3";

        private const string ObsoleteMessageUtil =
            "1.10. 工具类接口（v2不可用，未来将在v3支持）" +
            "https://xg.qq.com/docs/server_api/v2/rest.html#%E8%B4%A6%E5%8F%B7account%E6%8E%A5%E5%8F%A3";
    }
}