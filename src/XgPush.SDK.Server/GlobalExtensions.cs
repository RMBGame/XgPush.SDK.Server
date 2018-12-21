using JetBrains.Annotations;
using System.Collections.Generic;
using System.Net.Http.Headers;
using XgPush.SDK.Server;
using XgPush.SDK.Server.Internal;
using static XgPush.SDK.Server.Internal.Constants;

// ReSharper disable once UnusedMember.Global

/// <summary>
/// 全局扩展函数定义类。
/// </summary>
public static partial class XgPush_Server_SDK_GlobalExtensions
{
    /// <summary>
    /// resultCode == <see cref="XingePushClientResultCode.调用成功"/> 。
    /// </summary>
    /// <param name="resultCode"></param>
    /// <returns></returns>
    public static bool IsSuccess(this XingePushClientResultCode resultCode)
        => resultCode == XingePushClientResultCode.调用成功;

    /// <summary>
    /// resultCode == <see cref="XingePushClientResultCode.请求时间戳不在有效期内_0xFFFFFFFE"/> || <see cref="XingePushClientResultCode.请求时间戳不在有效期内_0xFFFFFF9A"/> (检查 timestamp 和 valid_time 参数)。
    /// </summary>
    /// <param name="resultCode"></param>
    /// <returns></returns>
    public static bool IsRequestTimestampIsNotWithinTheValidityPeriod
        (this XingePushClientResultCode resultCode)
        => resultCode == XingePushClientResultCode.请求时间戳不在有效期内_0xFFFFFFFE ||
        resultCode == XingePushClientResultCode.请求时间戳不在有效期内_0xFFFFFF9A;

    /// <summary>
    /// resultCode == <see cref="XingePushClientResultCode.信鸽服务器处理错误_0xFFFFFFFD"/> ||
    /// <see cref="XingePushClientResultCode.信鸽服务器处理错误_0xFFFFFFFB"/> ||
    /// <see cref="XingePushClientResultCode.信鸽服务器处理错误_0x05"/> ||
    /// <see cref="XingePushClientResultCode.内部错误"/>
    /// (稍后重试)。
    /// </summary>
    /// <param name="resultCode"></param>
    /// <returns></returns>
    public static bool IsXgPushServerError(this XingePushClientResultCode resultCode)
        =>
            resultCode == XingePushClientResultCode.信鸽服务器处理错误_0xFFFFFFFD ||
            resultCode == XingePushClientResultCode.信鸽服务器处理错误_0xFFFFFFFB ||
            resultCode == XingePushClientResultCode.信鸽服务器处理错误_0x05 ||
            resultCode == XingePushClientResultCode.内部错误;

    #region XingePushClientResult ErrMsg

    /// <summary>
    ///
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    internal static bool Equals(XingePushClientResult left, string right)
        => left != null && left.ResultCode == XingePushClientResultCode.参数错误 &&
        !string.IsNullOrWhiteSpace(left.ErrMsg) && left.ErrMsg == right;

    /// <summary>
    /// <see cref="ErrorMessageType"/>
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool IsErrorMessageType(this XingePushClientResult result) => Equals(result, ErrorMessageType);

    /// <summary>
    /// <see cref="ErrorMessageInvalid"/>
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool IsErrorMessageInvalid(this XingePushClientResult result) => Equals(result, ErrorMessageInvalid);

    #endregion

    internal static readonly IDictionary<AudienceType, string> mDefineds_AudienceType =
        new Dictionary<AudienceType, string>
    {
        { AudienceType.All, "all" },
        { AudienceType.Tag, "tag" },
        { AudienceType.Token, "token" },
        { AudienceType.TokenList, "token_list" },
        { AudienceType.Account, "account" },
        { AudienceType.AccountList, "account_list" },
    };

    /// <summary>
    ///
    /// </summary>
    /// <param name="audience_type"></param>
    /// <returns></returns>
    public static string GetString(this AudienceType audience_type)
        => mDefineds_AudienceType.ContainsKey(audience_type) ?
        mDefineds_AudienceType[audience_type] : null;

    /// <summary>
    ///
    /// </summary>
    /// <param name="headers"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool TryAddContentType([NotNull]HttpRequestHeaders headers, [NotNull] string value)
    {
        try
        {
            if (TFMs_Compat.AddContentType != null)
            {
                TFMs_Compat.AddContentType.Invoke(headers, value);
            }
            else
            {
                headers.Add(ContentType, value);
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="class"></param>
    /// <returns></returns>
    public static bool IsValid(this IsValid @class) => @class.IsValid();
}