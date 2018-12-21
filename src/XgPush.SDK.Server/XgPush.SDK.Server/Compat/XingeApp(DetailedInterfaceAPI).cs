using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using XgPush.SDK.Server.Internal;

// 详细接口api

#pragma warning disable IDE1006 // 命名样式

namespace XgPush.SDK.Server.Compat
{
    public partial class XingeApp
    {
        /// <summary>
        /// 推送单个设备，限Android系统使用。
        /// </summary>
        /// <param name="devicetoken"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string PushSingleDevice(string devicetoken, Message msg)
        {
            if (!isValidMessageType(msg))
                return "message type msg!";
            if (!msg.isValid())
                return "message is invalid!";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.multi_pkg, msg.getMultiPkg() },
                { Constants.device_token, devicetoken },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_PUSHSINGLEDEVICE, param);
            return ret;
        }

        /// <summary>
        /// 推送单个设备，限iOS系统使用。
        /// </summary>
        /// <param name="deviceToken"></param>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string PushSingleDevice(string deviceToken, MessageIOS msg, iOSEnvironment env)
        {
            if (!isValidMessageType(msg, env))
            {
                return "{'ret_code':-1,'err_msg':'message type or environment error!'}";
            }

            if (!msg.isValid())
            {
                return "{'ret_code':-1,'err_msg':'message invalid!'}";
            }

            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.device_token, deviceToken },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() },
                { Constants.environment, env }
            };

            if (msg.getLoopInterval() > 0 && msg.getLoopTimes() > 0)
            {
                param.Add(Constants.loop_interval, msg.getLoopInterval());
                param.Add(Constants.loop_times, msg.getLoopTimes());
            }
            var ret = callRestful(RESTAPI_PUSHSINGLEDEVICE, param);
            return ret;
        }

        /// <summary>
        /// 推送单个账号，限Android系统使用。
        /// </summary>
        /// <param name="account"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string PushSingleAccount(string account, Message msg)
        {
            if (!isValidMessageType(msg))
                return "message type error!";
            if (!msg.isValid())
                return "message is invalid!";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.multi_pkg, msg.getMultiPkg() },
                { Constants.account, account },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_PUSHSINGLEACCOUNT, param);
            return ret;
        }

        /// <summary>
        /// 推送单个账号，限iOS系统使用。
        /// </summary>
        /// <param name="account"></param>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string PushSingleAccount(string account, MessageIOS msg, iOSEnvironment env)
        {
            if (!isValidMessageType(msg, env))
            {
                return "{'ret_code':-1,'err_msg':'message type or environment error!'}";
            }
            if (!msg.isValid())
            {
                return "{'ret_code':-1,'err_msg':'message invalid!'}";
            }
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.account, account },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() },
                { Constants.environment, env }
            };
            var ret = callRestful(RESTAPI_PUSHSINGLEACCOUNT, param);
            return ret;
        }

        /// <summary>
        /// 推送账号列表，限Android系统使用。
        /// </summary>
        /// <param name="accountList"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string PushAccountList(IEnumerable<string> accountList, Message msg)
        {
            if (!isValidMessageType(msg))
                return "message type error!";
            if (!msg.isValid())
                return "    !";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.multi_pkg, msg.getMultiPkg() },
                { Constants.account_list, toJArray(accountList) },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_PUSHACCOUNTLIST, param);
            return ret;
        }

        /// <summary>
        /// 推送账号列表，限iOS系统使用。
        /// </summary>
        /// <param name="accountList"></param>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string PushAccountList(IEnumerable<string> accountList, MessageIOS msg, iOSEnvironment env)
        {
            if (!isValidMessageType(msg, env))
            {
                return "{'ret_code':-1,'err_msg':'message type or environment error!'}";
            }
            if (!msg.isValid())
            {
                return "{'ret_code':-1,'err_msg':'message invalid!'}";
            }
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.account_list, toJArray(accountList) },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() },
                { Constants.environment, env }
            };
            var ret = callRestful(RESTAPI_PUSHACCOUNTLIST, param);
            return ret;
        }

        /// <summary>
        /// 推送全部设备，限Android系统使用。
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string PushAllDevice(Message msg)
        {
            if (!isValidMessageType(msg))
                return "message type error!";
            if (!msg.isValid())
                return "message is invalid!";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.multi_pkg, msg.getMultiPkg() },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() }
            };
            if (msg.getLoopInterval() > 0 && msg.getLoopTimes() > 0)
            {
                param.Add(Constants.loop_interval, msg.getLoopInterval());
                param.Add(Constants.loop_times, msg.getLoopTimes());
            }
            var ret = callRestful(RESTAPI_PUSHALLDEVICE, param);
            return ret;
        }

        /// <summary>
        /// 推送全部设备，限iOS系统使用。
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string PushAllDevice(MessageIOS msg, iOSEnvironment env)
        {
            if (!isValidMessageType(msg, env))
            {
                return "{'ret_code':-1,'err_msg':'message type or environment error!'}";
            }
            if (!msg.isValid())
            {
                return "{'ret_code':-1,'err_msg':'message invalid!'}";
            }
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() },
                { Constants.environment, env }
            };
            if (msg.getLoopInterval() > 0 && msg.getLoopTimes() > 0)
            {
                param.Add(Constants.loop_interval, msg.getLoopInterval());
                param.Add(Constants.loop_times, msg.getLoopTimes());
            }
            var ret = callRestful(RESTAPI_PUSHALLDEVICE, param);
            return ret;
        }

        /// <summary>
        /// 推送标签，限Android系统使用。
        /// </summary>
        /// <param name="tagList"></param>
        /// <param name="tagOp"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string PushTags(IEnumerable<string> tagList, Operator tagOp, Message msg)
        {
            if (!isValidMessageType(msg))
                return "message type error!";
            if (!msg.isValid() || tagList.Any() || tagOp != Operator.AND && tagOp != Operator.OR)
            {
                return "paramas invalid!";
            }
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.multi_pkg, msg.getMultiPkg() },
                { Constants.message_type, msg.getType() },
                { Constants.tags_list, toJArray(tagList) },
                { Constants.tags_op, tagOp },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() }
            };
            if (msg.getLoopInterval() > 0 && msg.getLoopTimes() > 0)
            {
                param.Add(Constants.loop_interval, msg.getLoopInterval());
                param.Add(Constants.loop_times, msg.getLoopTimes());
            }
            var ret = callRestful(RESTAPI_PUSHTAGS, param);
            return ret;
        }

        /// <summary>
        /// 推送标签，限iOS系统使用。
        /// </summary>
        /// <param name="tagList"></param>
        /// <param name="tagOp"></param>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string PushTags(IEnumerable<string> tagList, Operator tagOp, MessageIOS msg,
            iOSEnvironment env)
        {
            if (!isValidMessageType(msg, env))
            {
                return "{'ret_code':-1,'err_msg':'message type or environment error!'}";
            }
            if (!msg.isValid())
            {
                return "{'ret_code':-1,'err_msg':'message invalid!'}";
            }
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.send_time, msg.getSendTime() },
                { Constants.message_type, msg.getType() },
                { Constants.tags_list, toJArray(tagList) },
                { Constants.tags_op, tagOp },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 },
                { Constants.environment, env }
            };
            if (msg.getLoopInterval() > 0 && msg.getLoopTimes() > 0)
            {
                param.Add(Constants.loop_interval, msg.getLoopInterval());
                param.Add(Constants.loop_times, msg.getLoopTimes());
            }
            var ret = callRestful(RESTAPI_PUSHTAGS, param);
            return ret;
        }

        /// <summary>
        /// 创建批量推送消息，后续利用生成的 pushid 配合 PushAccountListMultiple
        /// 或 PushDeviceListMultiple 接口使用，限Android系统使用。
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string CreateMultipush(Message msg)
        {
            if (!isValidMessageType(msg))
                return "message type error!";
            if (!msg.isValid())
                return "message is invalid!";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.multi_pkg, msg.getMultiPkg() },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp()}
            };
            var ret = callRestful(RESTAPI_CREATEMULTIPUSH, param);
            return ret;
        }

        /// <summary>
        /// 创建批量推送消息，后续利用生成的 pushid 配合 PushAccountListMultiple
        /// 或 PushDeviceListMultiple 接口使用，限iOS系统使用。
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public string CreateMultipush(MessageIOS msg, iOSEnvironment env)
        {
            if (!isValidMessageType(msg, env))
            {
                return "{'ret_code':-1,'err_msg':'message type or environment error!'}";
            }

            if (!msg.isValid())
            {
                return "{'ret_code':-1,'err_msg':'message invalid!'}";
            }

            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.expire_time, msg.getExpireTime() },
                { Constants.message_type, msg.getType() },
                { Constants.message, msg.toJson() },
                { Constants.timestamp, GetTimestamp() },
                { Constants.environment, env }
            };
            var ret = callRestful(RESTAPI_CREATEMULTIPUSH, param);
            return ret;
        }

        /// <summary>
        /// 推送消息给大批量账号。
        /// </summary>
        /// <param name="pushId"></param>
        /// <param name="accountList"></param>
        /// <returns></returns>
        public string PushAccountListMultiple(long pushId, IEnumerable<string> accountList)
        {
            if (pushId <= 0)
                return "{'ret_code':-1,'err_msg':'pushId invalid!'}";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.push_id, pushId },
                { Constants.account_list, toJArray(accountList) },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_PUSHACCOUNTLISTMULTIPLE, param);
            return ret;
        }

        /// <summary>
        /// 推送消息给大批量设备。
        /// </summary>
        /// <param name="pushId"></param>
        /// <param name="deviceList"></param>
        /// <returns></returns>
        public string PushDeviceListMultiple(long pushId, IEnumerable<string> deviceList)
        {
            if (pushId <= 0)
                return "{'ret_code':-1,'err_msg':'pushId invalid!'}";
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.push_id, pushId },
                { Constants.device_list, toJArray(deviceList) },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_PUSHDEVICELISTMULTIPLE, param);
            return ret;
        }

        /// <summary>
        /// 查询群发消息状态。
        /// </summary>
        /// <param name="pushIdList"></param>
        /// <returns></returns>
        public string QueryPushStatus(IEnumerable<string> pushIdList)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.timestamp, GetTimestamp() }
            };
            var ja = new JArray();
            foreach (var pushId in pushIdList)
            {
                var jo = new JObject
                {
                    { Constants.push_id, pushId }
                };
                ja.Add(jo);
            }
            param.Add(Constants.push_ids, ja.ToString());
            var ret = callRestful(RESTAPI_QUERYPUSHSTATUS, param);
            return ret;
        }

        /// <summary>
        /// 查询群发消息状态。
        /// </summary>
        /// <param name="pushIdList"></param>
        /// <returns></returns>
        public string QueryPushStatus(params string[] pushIdList)
        {
            IEnumerable<string> collection = pushIdList;
            return QueryPushStatus(collection);
        }

        /// <summary>
        /// 查询消息覆盖的设备数。
        /// </summary>
        /// <returns></returns>
        public string QueryDeviceCount()
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_QUERYDEVICECOUNT, param);
            return ret;
        }

        /// <summary>
        /// 查询应用当前所有的tags。
        /// </summary>
        /// <param name="start">从哪个index开始。</param>
        /// <param name="limit">限制结果数量，最多取多少个tag。</param>
        /// <returns></returns>
        public string QueryTags(int start, int limit)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { nameof(start), start },
                { nameof(limit), limit },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_QUERYTAGS, param);
            return ret;
        }

        /// <summary>
        /// 查询应用所有的tags，如果超过100个，取前100个。
        /// </summary>
        /// <returns></returns>
        public string QueryTags() => QueryTags(0, 100);

        /// <summary>
        /// 查询带有指定tag的设备数量。
        /// </summary>
        /// <param name="tag">指定的标签。</param>
        /// <returns></returns>
        public string queryTagTokenNum(string tag)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { nameof(tag), tag },
                { Constants.timestamp, GetTimestamp() }
            };
            var ret = callRestful(RESTAPI_QUERYTAGTOKENNUM, param);
            return ret;
        }

        /// <summary>
        /// 查询设备下所有的tag。
        /// </summary>
        /// <param name="deviceToken">目标设备token。</param>
        /// <returns></returns>
        public string queryTokenTags(string deviceToken)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.device_token, deviceToken },
                { Constants.timestamp, GetTimestamp() }
            };

            var ret = callRestful(RESTAPI_QUERYTOKENTAGS, param);
            return ret;
        }

        /// <summary>
        /// 取消尚未推送的定时任务。
        /// </summary>
        /// <param name="pushId">各类推送任务返回的push_id。</param>
        /// <returns></returns>
        public string cancelTimingPush(string pushId)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.push_id, pushId },
                { Constants.timestamp, GetTimestamp() }
            };

            var ret = callRestful(RESTAPI_CANCELTIMINGPUSH, param);
            return ret;
        }

        /// <summary>
        /// 批量为token设置标签。
        /// </summary>
        /// <param name="tagTokenPairs"></param>
        /// <returns></returns>
        public string BatchSetTag(IEnumerable<TagTokenPair> tagTokenPairs)
        {
            foreach (var pair in tagTokenPairs)
            {
                if (!isValidToken(pair.token))
                {
                    return "{\"ret_code\":-1,\"err_msg\":\"invalid token " + pair.token + "\"}";
                }
            }
            var param = initParams();
            param.Add("tag_token_list", tagTokenPairs.Select(x => x.ToArray()));
            var ret = callRestful(RESTAPI_BATCHSETTAG, param);
            return ret;
        }

        /// <summary>
        /// 批量为token设置标签。
        /// </summary>
        /// <param name="tagTokenPairs"></param>
        /// <returns></returns>
        public string BatchSetTag(params TagTokenPair[] tagTokenPairs)
        {
            IEnumerable<TagTokenPair> collection = tagTokenPairs;
            return BatchSetTag(collection);
        }

        /// <summary>
        /// 批量为token删除标签。
        /// </summary>
        /// <param name="tagTokenPairs"></param>
        /// <returns></returns>
        public string BatchDelTag(IEnumerable<TagTokenPair> tagTokenPairs)
        {
            foreach (var pair in tagTokenPairs)
            {
                if (!isValidToken(pair.token))
                {
                    return "{\"ret_code\":-1,\"err_msg\":\"invalid token " + pair.token + "\"}";
                }
            }
            var param = initParams();
            param.Add("tag_token_list", tagTokenPairs.Select(x => x.ToArray()));
            var ret = callRestful(RESTAPI_BATCHDELTAG, param);
            return ret;
        }

        /// <summary>
        /// 批量为token删除标签。
        /// </summary>
        /// <param name="tagTokenPairs"></param>
        /// <returns></returns>
        public string BatchDelTag(params TagTokenPair[] tagTokenPairs)
        {
            IEnumerable<TagTokenPair> collection = tagTokenPairs;
            return BatchDelTag(collection);
        }

        /// <summary>
        /// 查询token相关的信息，包括最近一次活跃时间，离线消息数等。
        /// </summary>
        /// <param name="deviceToken">目标设备token。</param>
        /// <returns></returns>
        public string queryInfoOfToken(string deviceToken)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.device_token, deviceToken },
                { Constants.timestamp, GetTimestamp() }
            };

            var ret = callRestful(RESTAPI_QUERYINFOOFTOKEN, param);
            return ret;
        }

        /// <summary>
        /// 查询账号绑定的token。
        /// </summary>
        /// <param name="account">目标账号。</param>
        /// <returns></returns>
        public string queryTokensOfAccount(string account)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.account, account },
                { Constants.timestamp, GetTimestamp() }
            };

            var ret = callRestful(RESTAPI_QUERYTOKENSOFACCOUNT, param);
            return ret;
        }

        /// <summary>
        /// 删除指定账号和token的绑定关系(token仍然有效)。
        /// </summary>
        /// <param name="account">目标账号。</param>
        /// <param name="deviceToken">目标设备token。</param>
        /// <returns></returns>
        public string deleteTokenOfAccount(string account, string deviceToken)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.account, account },
                { Constants.device_token, deviceToken },
                { Constants.timestamp, GetTimestamp() }
            };

            var ret = callRestful(RESTAPI_DELETETOKENOFACCOUNT, param);
            return ret;
        }

        /// <summary>
        /// 删除指定账号绑定的所有token(token仍然有效)。
        /// </summary>
        /// <param name="account">目标账号。</param>
        /// <returns></returns>
        public string deleteAllTokensOfAccount(string account)
        {
            var param = new Dictionary<string, object>
            {
                { Constants.access_id, m_access_id },
                { Constants.account, account },
                { Constants.timestamp, GetTimestamp() }
            };

            var ret = callRestful(RESTAPI_DELETEALLTOKENSOFACCOUNT, param);
            return ret;
        }
    }
}