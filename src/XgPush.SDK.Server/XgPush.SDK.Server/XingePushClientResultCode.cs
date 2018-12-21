namespace XgPush.SDK.Server
{
    /// <inheritdoc cref="T:XgPush.SDK.Server.XingePushClientResultCode" />
    /// <summary>
    /// (返回码) <see cref="T:XgPush.SDK.Server.XingePushClientResultCode" /> <see href="http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E8%BF%94%E5%9B%9E%E7%A0%81%E4%B8%80%E8%A7%88" />
    /// </summary>
    public enum XingePushClientResultCode
    {
        /// <summary>(可采取措施)证书过期，需重新上传证书。</summary>
        证书过期 = -106, // -0x0000006A

        /// <summary>(可采取措施)稍后重试。</summary>
        内部错误 = -104, // -0x00000068

        /// <summary>(可采取措施)检查签名生成流程，生成sign是METHOD 必须与请求时所使用的一致。</summary>
        Sign不合法 = -103, // -0x00000067

        /// <summary>(可采取措施)检查timestamp和valid_time参数。</summary>
        请求时间戳不在有效期内_0xFFFFFF9A = -102, // -0x00000066

        /// <summary>
        /// (可采取措施)请检查通用基础参数(<see href="http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#%E9%80%9A%E7%94%A8%E5%9F%BA%E7%A1%80%E5%8F%82%E6%95%B0" />)。
        /// </summary>
        参数错误_0xFFFFFF9B = -101, // -0x00000065

        /// <summary>(可采取措施)稍后重试。</summary>
        信鸽服务器处理错误_0xFFFFFFFB = -5,

        /// <summary>(可采取措施)稍后重试。</summary>
        信鸽服务器处理错误_0xFFFFFFFD = -3,

        /// <summary>(可采取措施)检查 timestamp 和 valid_time 参数。</summary>
        请求时间戳不在有效期内_0xFFFFFFFE = -2,

        /// <summary>(可采取措施)检查参数配置。</summary>
        参数错误 = -1,

        /// <summary>
        /// <see cref="F:XgPush.SDK.Server.XingePushClientResultCode.调用成功" />
        /// </summary>
        调用成功 = 0,

        /// <summary>(可采取措施)绑定标签时，Token或者是标签为空。</summary>
        标签绑定错误 = 2,

        /// <summary>(可采取措施)稍后重试。</summary>
        信鸽服务器处理错误_0x05 = 5,

        /// <summary>(可采取措施)请检查终端设备注册是否成功。</summary>
        设备Token未成功注册 = 6,

        /// <summary>(可采取措施)删除其他未使用的账号(调用账号解绑）。</summary>
        通用错误账号超限 = 7,

        /// <summary>(可采取措施)检查account和token是否有绑定关系。</summary>
        推送的账号没有绑定Token = 48, // 0x00000030

        /// <summary>(可采取措施)目前是4K字节。</summary>
        消息体超限 = 73, // 0x00000049

        /// <summary>(可采取措施)检查消息体即 message 字段内容。</summary>
        消息体格式不符合Json格式 = 75, // 0x0000004B

        /// <summary>(可采取措施)检查loop_time。</summary>
        循环任务参数错误 = 78, // 0x0000004E

        /// <summary>(可采取措施)清理不使用的tag。</summary>
        设备Token关联的tag过多 = 91, // 0x0000005B

        /// <summary>(可采取措施)清理不使用的tag，限制10000。</summary>
        App的关联的Tag过多 = 92, // 0x0000005C

        /// <summary>(可采取措施)信鸽使用的推送证书格式是pem，另外，注意区分生产证书、开发证书的区别。</summary>
        APNS证书错误 = 100, // 0x00000064
    }
}