using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.BaseTypes
{
    /// <summary>
    /// <see cref="Nullable"/>&lt;<see cref="DateTime"/>&gt;(<see cref="DateTime"/>?) / <see cref="string"/>(<see cref="Format"/>(yyyy-MM-dd HH:mm:ss))
    /// </summary>
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public struct NullableDateTime : IEquatable<NullableDateTime>,
        IEquatable<string>,
        IEquatable<DateTime>,
        IEquatable<DateTime?>,
        IConvertible
    {
        private readonly string mValue;

        /// <summary>
        ///
        /// </summary>
        public const string Format = Constants.DateTimeFormat;

        /// <summary>
        ///
        /// </summary>
        public static readonly NullableDateTime Empty = new NullableDateTime(null);

        /// <summary>
        ///
        /// </summary>
        internal bool HasValue => !string.IsNullOrEmpty(mValue);

        internal NullableDateTime(DateTime value) => mValue = value.ToString(Format);

        internal NullableDateTime(DateTimeOffset value) => mValue = value.DateTime.ToString(Format);

        internal NullableDateTime(string value) => mValue = ToDateTimeString(value);

        internal static string ToDateTimeString(string value)
        {
            return string.IsNullOrEmpty(value) ?
            string.Empty : (DateTime.TryParseExact
            (value, Format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var dateTime) ?
            dateTime.ToString(Format) : string.Empty);
        }

        internal NullableDateTime(object value)
        {
            mValue = null;
            if (value != null)
            {
                switch (value)
                {
                    case string s:
                        mValue = ToDateTimeString(s);
                        break;

                    case DateTime dateTime:
                        mValue = dateTime.ToString(Format);
                        break;

                    case DateTimeOffset dateTimeOffset:
                        mValue = dateTimeOffset.DateTime.ToString(Format);
                        break;

                    case NullableDateTime nullableDateTime:
                        mValue = nullableDateTime.mValue;
                        break;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(NullableDateTime value) => value.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator NullableDateTime(string value) => new NullableDateTime(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator NullableDateTime(DateTime value) => new NullableDateTime(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator NullableDateTime(DateTimeOffset value) => new NullableDateTime(value);

        /// <summary>
        /// 将此实例的数值转换为其等效的字符串表示形式。
        /// </summary>
        /// <returns></returns>
        public override string ToString() => mValue;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return -952055727 + EqualityComparer<string>.Default.GetHashCode(mValue);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null && string.IsNullOrWhiteSpace(mValue)) return true;

            switch (obj)
            {
                case string s:
                    return mValue.Equals(s);

                case DateTime dateTime:
                    return Equals(this, dateTime);

                case NullableDateTime dateTime2:
                    return Equals(this, dateTime2);

                default:
                    return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NullableDateTime other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DateTime other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(string other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DateTime? other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(NullableDateTime left, NullableDateTime right) => left.mValue == right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(NullableDateTime left, string right) => left.mValue.Equals(right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(string left, NullableDateTime right) => left.Equals(right.mValue);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(NullableDateTime left, DateTime right) => !string.IsNullOrEmpty(left.mValue) && left.mValue == right.ToString(Format);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(DateTime left, NullableDateTime right) => Equals(right, left);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(NullableDateTime left, DateTime? right) => string.IsNullOrEmpty(left.mValue) ? !right.HasValue : right.HasValue && Equals(left, right.Value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(DateTime? left, NullableDateTime right) => Equals(right, left);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NullableDateTime left, NullableDateTime right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NullableDateTime left, NullableDateTime right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NullableDateTime left, string right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NullableDateTime left, string right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(string left, NullableDateTime right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(string left, NullableDateTime right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NullableDateTime left, DateTime right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NullableDateTime left, DateTime right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DateTime left, NullableDateTime right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DateTime left, NullableDateTime right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(NullableDateTime left, DateTime? right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(NullableDateTime left, DateTime? right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DateTime? left, NullableDateTime right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DateTime? left, NullableDateTime right) => !Equals(left, right);

        #region System.IConvertible

        /// <summary>
        /// 返回此实例的 System.TypeCode。
        /// </summary>
        /// <returns>枚举常数，它是实现该接口的类或值类型的 System.TypeCode。</returns>
        public TypeCode GetTypeCode()
        {
#if NETSTANDARD1_3
            return ((IConvertible)mValue).GetTypeCode();
#else
            return mValue.GetTypeCode();
#endif
        }

        bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)mValue).ToBoolean(provider);

        byte IConvertible.ToByte(IFormatProvider provider) => ((IConvertible)mValue).ToByte(provider);

        char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)mValue).ToChar(provider);

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)mValue).ToDateTime(provider);

        decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)mValue).ToDecimal(provider);

        double IConvertible.ToDouble(IFormatProvider provider) => ((IConvertible)mValue).ToDouble(provider);

        short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)mValue).ToInt16(provider);

        int IConvertible.ToInt32(IFormatProvider provider) => ((IConvertible)mValue).ToInt32(provider);

        long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)mValue).ToInt64(provider);

        sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)mValue).ToSByte(provider);

        float IConvertible.ToSingle(IFormatProvider provider) => ((IConvertible)mValue).ToSingle(provider);

        string IConvertible.ToString(IFormatProvider provider) => (mValue as IConvertible).ToString(provider);

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)mValue).ToType(conversionType, provider);

        ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)mValue).ToUInt16(provider);

        uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)mValue).ToUInt32(provider);

        ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)mValue).ToUInt64(provider);

        #endregion System.IConvertible
    }

    /// <summary>
    ///
    /// </summary>
    public sealed class NullableDateTimeConverter : Converter<NullableDateTime?>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NullableDateTime ConvertFrom(object value)
        {
            return new NullableDateTime(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, NullableDateTime? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                var temp = value.Value.ToString();
                writer.WriteValue(temp);
            }
            else
            {
                writer.WriteNull();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="hasExistingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override NullableDateTime? ReadJson(JsonReader reader, Type objectType, object existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            if (value != null)
                return ConvertFrom(value);
            else
                return null;
        }
    }
}

/// <summary>
///
/// </summary>
public static partial class XgPush_Server_SDK_GlobalExtensions
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="p0"></param>
    /// <returns></returns>
    public static bool HasValue(this NullableDateTime? p0)
    {
        return p0.HasValue && p0.Value.HasValue;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="p0"></param>
    /// <returns></returns>
    public static bool HasValue(this NullableDateTime p0)
    {
        return p0.HasValue;
    }
}