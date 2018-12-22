using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XgPush.SDK.Server;

namespace Sample.XgPush.SDK.Server.Controllers
{
    [Route("api/[controller]")]
    public class SampleController : Controller
    {
        private readonly XingePushClient pushClient;

        public SampleController(XingePushClient pushClient) => this.pushClient = pushClient;

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            Stop();

            dynamic result = null;

            var msg_android = new Message
            {
                Title = "your title",
                Content = "your content",
            };

            var msg_iOS = new MessageiOS
            {
                AlertStr = "your content",
                Badge = 1,
                Environment = iOSEnvironment.Development,
            };

            #region Push API v3

            // 全量推送

            var req_push_all = new PushRequest<Message>.All
            {
                Message = msg_android,
            };

            var rsp_push_all = await pushClient.v3.Push(req_push_all);

            result.rsp_push_all = rsp_push_all;

            // 标签推送

            var tags = new[] { "tag1", "tag2", "tag3" };

            var req_push_tag = new PushRequest<Message>.Tag
            {
                Message = msg_android,
                Value = new TagList(tags)
                {
                    Operators = Operator.OR // Operator.AND
                },
            };

            var rsp_push_tag = await pushClient.v3.Push(req_push_tag);

            result.rsp_push_tag = rsp_push_tag;

            // 单设备推送

            var req_push_token = new PushRequest<Message>.Token
            {
                Message = msg_android,
                Value = "token1",
            };

            var rsp_push_token = await pushClient.v3.Push(req_push_token);

            result.rsp_push_token = rsp_push_token;

            // 设备列表推送

            var tokens = new[] { "token1", "token2", "token3" };

            var req_push_tokens = new PushRequest<Message>.TokenList
            {
                Message = msg_android,
                Values = tokens,
            };

            var rsp_push_tokens = await pushClient.v3.Push(req_push_tokens);

            result.rsp_push_tokens = rsp_push_tokens;

            // 单账号推送

            var req_push_account = new PushRequest<Message>.Account
            {
                Message = msg_android,
                Value = "account1",
                Type = 0, //  往单个账号的最新的device上推送信息
                // AccountType = AccountType.PhoneNumber, // 账号类型(可选)
            };

            var rsp_push_account = await pushClient.v3.Push(req_push_account);

            result.rsp_push_account = rsp_push_account;

            // 账号列表推送

            var accounts = new[] { "account1", "account2", "account3" };

            var req_push_accounts = new PushRequest<Message>.AccountList
            {
                Message = msg_android,
                Values = accounts,
                //PushId = "123",
            };

            var rsp_push_accounts = await pushClient.v3.Push(req_push_accounts);

            result.rsp_push_accounts = rsp_push_accounts;

            #endregion

            #region Rest API v2

            // 全量推送

            var req_push_all_v2 = await pushClient.PushAllDevice(msg_android);
            result.req_push_all_v2 = req_push_all_v2;

            // 查询消息状态

            var pushIds = new[] { "push_id_1", "push_id_2" };

            var req_query_push_status = await pushClient.QueryPushStatus(pushIds);

            result.req_query_push_status = req_query_push_status;

            // 取消推送

            var req_cancel_push = await pushClient.CancelTimingPush("push_id_1");

            #endregion

            return Ok(result);
        }

        [NonAction]
        private void Stop() => throw new Exception();
    }
}
