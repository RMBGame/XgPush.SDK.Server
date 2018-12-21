using System.Threading.Tasks;

#if DEBUG

namespace XgPush.SDK.Server.DEBUG
{
    /// <summary>
    ///
    /// </summary>
    public interface IDebugXingePushClient
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<XingePushClientResult> DebugTest(string url);
    }
}

#endif