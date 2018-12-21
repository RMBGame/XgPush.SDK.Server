using System;
using System.Collections.Generic;
using System.Globalization;
using static XgPush.SDK.Server.Internal.Constants;

namespace XgPush.SDK.Server.Internal
{
    /// <summary>
    ///
    /// </summary>
    internal static class InternalExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string NullToEmpty(this string value) => value ?? string.Empty;

        /// <summary>
        ///
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="cycles"></param>
        /// <returns></returns>
        internal static IEnumerable<string> GetMessages
            (this Exception exception, ushort cycles = byte.MaxValue)
        {
            ushort i = 0;
            while (exception != null && i++ < cycles)
            {
                var temp = exception.Message;
                exception = exception.InnerException;
                yield return temp;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="separator"></param>
        /// <param name="cycles"></param>
        /// <returns></returns>
        internal static string GetAllMessage(this Exception exception, string separator = null, ushort cycles = byte.MaxValue)
            => string.Join(separator ?? Environment.NewLine, exception.GetMessages(cycles));

        /// <summary>
        ///
        /// </summary>
        /// <param name="tagTokenPair"></param>
        /// <returns></returns>
        internal static string[] ToArrayInternal(ITagTokenPair tagTokenPair) => new[] { tagTokenPair.Tag, tagTokenPair.Token };

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static byte ToByte(this bool value) => value ? (byte)1 : (byte)0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static int ToInt32(this bool value) => value ? 1 : 0;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static uint ToUInt32(this bool value) => value ? 1u : 0u;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this byte value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this sbyte value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this short value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this ushort value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this int value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this uint value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this long value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static bool ToBoolean(this ulong value)
        {
            switch (value)
            {
                //case 0:
                //    return false;

                case 1:
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 尝试将数字的字符串表示形式转换为它的等效 32 位有符号整数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static int? TryParseInt32(this string value)
            => string.IsNullOrEmpty(value) ? null :
            (int.TryParse(value, out var temp) ? temp as int? : null);

        /// <summary>
        /// 尝试将数字的字符串表示形式转换为它的等效 32 位无符号整数。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static uint? TryParseUInt32(this string value)
            => string.IsNullOrEmpty(value) ? null :
            (uint.TryParse(value, out var temp) ? temp as uint? : null);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DateTime? TryParseDateTime(this string value)
            => string.IsNullOrEmpty(value) ? null :
            (DateTime.TryParseExact(value, DateTimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var temp) ? temp :
                (DateTime.TryParse(value, out var temp2) ? temp2 as DateTime? : null));

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        internal static bool IsDefined<T>(this T @enum) where T : Enum
        {
            return Enum.IsDefined(typeof(T), @enum);
        }
    }
}