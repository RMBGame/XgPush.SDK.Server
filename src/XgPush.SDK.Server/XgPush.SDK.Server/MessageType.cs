namespace XgPush.SDK.Server
{
    /// <summary>
    /// 消息类型(iOS平台用，必须为0，不区分通知栏消息和静默消息)。
    /// </summary>
    public enum MessageType : uint
    {
        /// <summary>
        /// Android/iOS 普通消息。
        /// </summary>
        Notification = 1,

        /// <summary>
        /// Android 透传消息 / iOS 静默消息。
        /// </summary>
        Message = 2,
    }
}