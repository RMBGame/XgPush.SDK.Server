using System.Collections.Generic;
using XgPush.SDK.Server.Internal;
using static XgPush.SDK.Server.Operator;

// 简易接口api

#pragma warning disable IDE1006 // 命名样式

namespace XgPush.SDK.Server.Compat
{
    public partial class XingeApp
    {
        /// <summary>
        /// 推送给指定的设备，限Android系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string pushTokenAndroid(long accessId, string secretKey,
            string title, string content, string token)
        {
            var message = new Message();
            message.setType(Message.TYPE_NOTIFICATION);
            message.setTitle(title);
            message.setContent(content);

            var xinge = new XingeApp(accessId, secretKey);
            string ret = xinge.PushSingleDevice(token, message);
            return ret;
        }

        /// <summary>
        /// 推送给指定的设备，限iOS系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string pushTokenIos(long accessId, string secretKey,
            string content, string token, iOSEnvironment env)
        {
            var message = new MessageIOS();
            message.setAlert(content);
            message.setBadge(1);
            message.setSound(Constants.beep_wav);

            var xinge = new XingeApp(accessId, secretKey);
            string ret = xinge.PushSingleDevice(token, message, env);
            return ret;
        }

        /// <summary>
        /// 推送给指定的账号，限Android系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public static string pushAccountAndroid(long accessId, string secretKey,
            string title, string content, string account)
        {
            var message = new Message();
            message.setType(Message.TYPE_NOTIFICATION);
            message.setTitle(title);
            message.setContent(content);

            var xinge = new XingeApp(accessId, secretKey);
            string ret = xinge.PushSingleAccount(account, message);
            return ret;
        }

        /// <summary>
        /// 推送给指定的账号，限iOS系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="content"></param>
        /// <param name="account"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string pushAccountIos(long accessId, string secretKey,
            string content, string account, iOSEnvironment env)
        {
            var message = new MessageIOS();
            message.setAlert(content);
            message.setBadge(1);
            message.setSound(Constants.beep_wav);

            var xinge = new XingeApp(accessId, secretKey);
            var ret = xinge.PushSingleAccount(account, message, env);
            return ret;
        }

        /// <summary>
        /// 推送给全部的设备，限Android系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string pushAllAndroid(long accessId, string secretKey, string title, string content)
        {
            var message = new Message();
            message.setType(Message.TYPE_NOTIFICATION);
            message.setTitle(title);
            message.setContent(content);

            var xinge = new XingeApp(accessId, secretKey);
            string ret = xinge.PushAllDevice(message);
            return ret;
        }

        /// <summary>
        /// 推送给全部的设备，限iOS系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="content"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string pushAllIos(long accessId, string secretKey,
            string content, iOSEnvironment env)
        {
            var message = new MessageIOS();
            message.setAlert(content);
            message.setBadge(1);
            message.setSound(Constants.beep_wav);

            var xinge = new XingeApp(accessId, secretKey);
            string ret = xinge.PushAllDevice(message, env);
            return ret;
        }

        /// <summary>
        /// 推送给绑定标签的设备，限Android系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string pushTagAndroid(long accessId, string secretKey,
            string title, string content, string tag)
        {
            var message = new Message();
            message.setType(Message.TYPE_NOTIFICATION);
            message.setTitle(title);
            message.setContent(content);

            var xinge = new XingeApp(accessId, secretKey);
            var tagList = new List<string> { tag };
            string ret = xinge.PushTags(tagList, OR, message);
            return ret;
        }

        /// <summary>
        /// 推送给绑定标签的设备，限iOS系统使用。
        /// </summary>
        /// <param name="accessId"></param>
        /// <param name="secretKey"></param>
        /// <param name="content"></param>
        /// <param name="tag"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string pushTagIos(long accessId, string secretKey,
            string content, string tag, iOSEnvironment env)
        {
            var message = new MessageIOS();
            message.setAlert(content);
            message.setBadge(1);
            message.setSound(Constants.beep_wav);

            var xinge = new XingeApp(accessId, secretKey);
            var tagList = new List<string> { tag };
            string ret = xinge.PushTags(tagList, OR, message, env);
            return ret;
        }
    }
}