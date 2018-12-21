using System.Collections.Generic;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    public interface IToDictionary
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> ToDictionary();
    }

    /// <summary>
    ///
    /// </summary>
    public interface IToDictionaryV3
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> ToDictionaryV3();
    }

    /// <summary>
    ///
    /// </summary>
    public interface IDictionarySerialize
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        Dictionary<string, object> ToDictionarySerialize();
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
        /// <param name="class"></param>
        /// <returns></returns>
        public static Dictionary<string, object> toJson(this IToDictionary @class)
        {
            return @class.ToDictionary();
        }
    }
}