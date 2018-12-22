namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///
        /// </summary>
        public const int ThreeDays = 259200;

        /// <summary>
        ///
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        ///
        /// </summary>
        public const string beep_wav = "beep.wav";

        /// <summary>
        ///
        /// </summary>
        public const string equal = "=";

        #region scheme/host/baseAddress

        /// <summary>
        ///
        /// </summary>
        public const string HttpScheme = "http";

        /// <summary>
        ///
        /// </summary>
        public const string HttpsScheme = "https";

        /// <summary>
        ///
        /// </summary>
        public const string SchemeConcat = "://";

        /// <summary>
        ///
        /// </summary>
        public const string HttpSchemeConcat = HttpScheme + SchemeConcat;

        /// <summary>
        ///
        /// </summary>
        public const string HttpsSchemeConcat = HttpsScheme + SchemeConcat;

        /// <summary>
        ///
        /// </summary>
        public const string Host = "openapi.xg.qq.com";

        /// <summary>
        ///
        /// </summary>
        [System.Obsolete("", true)]
        public const string BaseAddress = HttpScheme + SchemeConcat + Host;

        /// <summary>
        ///
        /// </summary>
        public const string BaseAddress_HTTPS = HttpsScheme + SchemeConcat + Host;

        #endregion scheme/host/baseAddress

        #region get/mime

        /// <summary>
        ///
        /// </summary>
        public const string GET = nameof(GET);

        /// <summary>
        /// 
        /// </summary>
        public const string POST = nameof(POST);

        /// <summary>
        ///
        /// </summary>
        public const string JSON_MIME = "application/json";

        /// <summary>
        ///
        /// </summary>
        public const string ContentType = "Content-Type";

        #endregion get/mime

        #region default

        /// <summary>
        ///
        /// </summary>
        public const string DefaultMessageSendTime = "2013-12-20 18:31:00";

        #endregion default

        #region field

        /// <summary>
        ///
        /// </summary>
        public const string err_msg = nameof(err_msg);

        /// <summary>
        ///
        /// </summary>
        public const string ret_code = nameof(ret_code);

        /// <summary>
        ///
        /// </summary>
        public const string result = nameof(result);

        /// <summary>
        ///
        /// </summary>
        public const string sign = nameof(sign);

        /// <summary>
        ///
        /// </summary>
        public const string tokens = nameof(tokens);

        /// <summary>
        ///
        /// </summary>
        public const string timestamp = nameof(timestamp);

        /// <summary>
        ///
        /// </summary>
        public const string action_type = nameof(action_type);

        /// <summary>
        ///
        /// </summary>
        public const string activity = nameof(activity);

        /// <summary>
        ///
        /// </summary>
        public const string intent = nameof(intent);

        /// <summary>
        ///
        /// </summary>
        public const string browser = nameof(browser);

        /// <summary>
        ///
        /// </summary>
        public const string url = nameof(url);

        /// <summary>
        ///
        /// </summary>
        public const string confirm = nameof(confirm);

        /// <summary>
        ///
        /// </summary>
        public const string title = nameof(title);

        /// <summary>
        ///
        /// </summary>
        public const string content = nameof(content);

        /// <summary>
        ///
        /// </summary>
        public const string accept_time = nameof(accept_time);

        /// <summary>
        ///
        /// </summary>
        public const string builder_id = nameof(builder_id);

        /// <summary>
        ///
        /// </summary>
        public const string ring = nameof(ring);

        /// <summary>
        ///
        /// </summary>
        public const string vibrate = nameof(vibrate);

        /// <summary>
        ///
        /// </summary>
        public const string clearable = nameof(clearable);

        /// <summary>
        ///
        /// </summary>
        public const string n_id = nameof(n_id);

        /// <summary>
        ///
        /// </summary>
        public const string ring_raw = nameof(ring_raw);

        /// <summary>
        ///
        /// </summary>
        public const string lights = nameof(lights);

        /// <summary>
        ///
        /// </summary>
        public const string icon_type = nameof(icon_type);

        /// <summary>
        ///
        /// </summary>
        public const string icon_res = nameof(icon_res);

        /// <summary>
        ///
        /// </summary>
        public const string style_id = nameof(style_id);

        /// <summary>
        ///
        /// </summary>
        public const string small_icon = nameof(small_icon);

        /// <summary>
        ///
        /// </summary>
        public const string action = nameof(action);

        /// <summary>
        ///
        /// </summary>
        public const string custom_content = nameof(custom_content);

        /// <summary>
        ///
        /// </summary>
        public const string start = nameof(start);

        /// <summary>
        ///
        /// </summary>
        public const string end = nameof(end);

        /// <summary>
        ///
        /// </summary>
        public const string hour = nameof(hour);

        /// <summary>
        ///
        /// </summary>
        public const string min = nameof(min);

        /// <summary>
        ///
        /// </summary>
        public const string access_id = nameof(access_id);

        /// <summary>
        ///
        /// </summary>
        public const string expire_time = nameof(expire_time);

        /// <summary>
        ///
        /// </summary>
        public const string send_time = nameof(send_time);

        /// <summary>
        ///
        /// </summary>
        public const string multi_pkg = nameof(multi_pkg);

        /// <summary>
        ///
        /// </summary>
        public const string device_token = nameof(device_token);

        /// <summary>
        ///
        /// </summary>
        public const string message_type = nameof(message_type);

        /// <summary>
        ///
        /// </summary>
        public const string message = nameof(message);

        /// <summary>
        ///
        /// </summary>
        public const string environment = nameof(environment);

        /// <summary>
        ///
        /// </summary>
        public const string loop_interval = nameof(loop_interval);

        /// <summary>
        ///
        /// </summary>
        public const string loop_times = nameof(loop_times);

        /// <summary>
        ///
        /// </summary>
        public const string alert = nameof(alert);

        /// <summary>
        ///
        /// </summary>
        public const string badge = nameof(badge);

        /// <summary>
        ///
        /// </summary>
        public const string sound = nameof(sound);

        /// <summary>
        ///
        /// </summary>
        public const string category = nameof(category);

        /// <summary>
        ///
        /// </summary>
        public const string aps = nameof(aps);

        /// <summary>
        ///
        /// </summary>
        public const string account_list = nameof(account_list);

        /// <summary>
        ///
        /// </summary>
        public const string tags_list = nameof(tags_list);

        /// <summary>
        ///
        /// </summary>
        public const string tags_op = nameof(tags_op);

        /// <summary>
        ///
        /// </summary>
        public const string push_id = nameof(push_id);

        /// <summary>
        ///
        /// </summary>
        public const string sub_push_ids = nameof(sub_push_ids);

        /// <summary>
        ///
        /// </summary>
        public const string device_list = nameof(device_list);

        /// <summary>
        ///
        /// </summary>
        public const string push_ids = nameof(push_ids);

        /// <summary>
        ///
        /// </summary>
        public const string tag = nameof(tag);

        /// <summary>
        ///
        /// </summary>
        public const string token = nameof(token);

        /// <summary>
        ///
        /// </summary>
        public const string account = nameof(account);

        /// <summary>
        ///
        /// </summary>
        public const string device_num = nameof(device_num);

        /// <summary>
        ///
        /// </summary>
        public const string total = nameof(total);

        /// <summary>
        ///
        /// </summary>
        public const string tags = nameof(tags);

        /// <summary>
        ///
        /// </summary>
        public const string status = nameof(status);

        /// <summary>
        ///
        /// </summary>
        public const string tag_token_list = nameof(tag_token_list);

        #endregion field

        /// <summary>
        ///
        /// </summary>
        public const string ErrorMessageType = "message type error!";

        /// <summary>
        ///
        /// </summary>
        public const string ErrorMessageInvalid = "message is invalid!";
    }
}