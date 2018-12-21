using Newtonsoft.Json;
using System.Collections.Generic;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;
using static XgPush.SDK.Server.Internal.Constants;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// 设置点击通知栏之后的行为，默认为打开app。
    /// </summary>
    public class ClickAction : BaseSerializeObject<ClickAction>, IsValid, IToDictionary
    {
        /// <summary>
        /// 动作类型。
        /// </summary>
        [JsonProperty(action_type)]
        public Type ActionType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(activity)]
        public string Activity { get; set; } = string.Empty;

        /// <summary>
        /// 客户端 Android SDK版本需要大于等于3.2.3，然后在客户端的intent配置data标签，并设置scheme属性。
        /// </summary>
        [JsonProperty(intent)]
        public string Intent { get; set; } = string.Empty;

        [JsonProperty(browser)]
        internal Browser Internal_Browser { get; set; } = new Browser();

        /// <summary>
        /// 仅支持http、https。
        /// </summary>
        [JsonIgnore]
        public string Url { get => Internal_Browser.Url; set => Internal_Browser.Url = value; }

        /// <summary>
        /// 是否需要用户确认。
        /// </summary>
        [JsonIgnore]
        public DigitBoolean ConfirmUrl { get => Internal_Browser.ConfirmUrl; set => Internal_Browser.ConfirmUrl = value; }

        #region const

        /// <summary>
        /// (动作类型)打开activity或app本身。
        /// </summary>
        public const int TYPE_ACTIVITY = (int)Type.Activity;

        /// <summary>
        /// (动作类型)打开浏览器。
        /// </summary>
        public const int TYPE_URL = (int)Type.Url;

        /// <summary>
        /// (动作类型)打开Intent。
        /// </summary>
        public const int TYPE_INTENT = (int)Type.Intent;

        #endregion

        /// <summary>
        /// 动作类型(设置点击通知栏之后的行为)。
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// 打开activity或app本身。
            /// </summary>
            Activity = 1,

            /// <summary>
            /// 打开浏览器。
            /// </summary>
            Url = 2,

            /// <summary>
            /// 打开Intent。
            /// </summary>
            Intent = 3,
        }

        /// <summary>
        ///
        /// </summary>
        internal class Browser
        {
            /// <summary>
            /// 仅支持http、https。
            /// </summary>
            [JsonProperty(url)]
            public string Url { get; set; } = string.Empty;

            /// <summary>
            /// 是否需要用户确认。
            /// </summary>
            [JsonProperty(confirm)]
            public DigitBoolean ConfirmUrl { get; set; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            if (!ActionType.IsDefined()) return false;
            if (ActionType == Type.Url) return !string.IsNullOrEmpty(Url);
            if (ActionType == Type.Intent) return !string.IsNullOrEmpty(Intent);
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                { action_type, (int)ActionType },
                { activity, Activity },
                { intent, Intent },
                {
                    browser, new Dictionary<string, object>
                    {
                        { url, Url },
                        { confirm, ConfirmUrl.ToInt32() },
                    }
                }
            };
        }
    }
}

namespace XgPush.SDK.Server.Compat
{
#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    ///
    /// </summary>
    public static partial class XgPush_Server_SDK_Compat_Extensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="clickAction"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public static ClickAction setActionType(this ClickAction clickAction, int actionType)
        {
            clickAction.ActionType = (ClickAction.Type)actionType;
            return clickAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="clickAction"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static ClickAction setActivity(this ClickAction clickAction, string activity)
        {
            clickAction.Activity = activity;
            return clickAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="clickAction"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static ClickAction setUrl(this ClickAction clickAction, string url)
        {
            clickAction.Url = url;
            return clickAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="clickAction"></param>
        /// <param name="confirmUrl"></param>
        /// <returns></returns>
        public static ClickAction setConfirmUrl(this ClickAction clickAction, uint confirmUrl)
        {
            clickAction.ConfirmUrl = confirmUrl;
            return clickAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="clickAction"></param>
        /// <param name="confirmUrl"></param>
        /// <returns></returns>
        public static ClickAction setConfirmUrl(this ClickAction clickAction, int confirmUrl)
        {
            clickAction.ConfirmUrl = (uint)confirmUrl;
            return clickAction;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="clickAction"></param>
        /// <param name="intent"></param>
        /// <returns></returns>
        public static ClickAction setIntent(this ClickAction clickAction, string intent)
        {
            clickAction.Intent = intent;
            return clickAction;
        }
    }
}