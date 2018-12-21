namespace XgPush.SDK.Server
{
    /// <summary>
    /// (账号类型)客户端调用SDK接口绑定。
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 手机号
        /// </summary>
        PhoneNumber = 1,

        /// <summary>
        /// 邮箱
        /// </summary>
        Email = 2,

        /// <summary>
        /// 微信 OpenID
        /// </summary>
        WeChat = 1000,

        /// <summary>
        /// QQ OpenID
        /// </summary>
        QQ = 1001,

        /// <summary>
        /// 新浪微博
        /// </summary>
        SinaWeibo = 1002,

        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 1003,

        /// <summary>
        /// 淘宝
        /// </summary>
        Taobao = 1004,

        /// <summary>
        /// 豆瓣
        /// </summary>
        Douban = 1005,

        /// <summary>
        /// Facebook
        /// </summary>
        Facebook = 1006,

        /// <summary>
        /// Twitter
        /// </summary>
        Twitter = 1007,

        /// <summary>
        /// Google
        /// </summary>
        Google = 1008,

        /// <summary>
        /// 百度
        /// </summary>
        Baidu = 1009,

        /// <summary>
        /// 京东
        /// </summary>
        JD = 1010,

        // linkin ??? LinkedIn?? https://xg.qq.com/docs/server_api/v3/push_api_v3.html#%E8%B4%A6%E5%8F%B7%E7%B1%BB%E5%9E%8B

        /// <summary>
        /// 其他
        /// </summary>
        Other = 1999,

        /// <summary>
        /// 游客登录
        /// </summary>
        Guest = 2000,

        /// <summary>
        /// 用户自定义
        /// </summary>
        Custom = 2001,
    }
}