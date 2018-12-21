using Newtonsoft.Json.Linq;

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public interface IResultV2
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="jToken"></param>
        void Init(JToken jToken);
    }
}