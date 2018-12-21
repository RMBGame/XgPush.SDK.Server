using Newtonsoft.Json;
using System;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.BaseTypes
{
    /// <summary>
    ///
    /// </summary>
    [JsonConverter(typeof(SecondConverter))]
    public struct Second : IEquatable<Second>,
        IEquatable<TimeSpan>,
        IEquatable<int>,
        IEquatable<uint>,
        IConvertible
    {
        #region field

        private readonly uint mValue;

        /// <summary>
        ///
        /// </summary>
        public static readonly Second ThreeDays = new Second(Constants.ThreeDays);

        #endregion

        #region ctor

        internal static uint ToUInt32(int value)
        {
            if (value < 0) return 0;
            return (uint)value;
        }

        internal static uint ToUInt32(long value)
        {
            if (value < 0) return 0;
            else if (value > uint.MaxValue) return uint.MaxValue;
            return (uint)value;
        }

        internal static uint ToUInt32(ulong value)
        {
            if (value < 0) return 0;
            else if (value > uint.MaxValue) return uint.MaxValue;
            return (uint)value;
        }

        internal static uint ToUInt32(TimeSpan value)
        {
            return (uint)value.TotalSeconds;
        }

        internal Second(int value) : this(ToUInt32(value))
        {
        }

        internal Second(uint value) => mValue = value;

        internal Second(TimeSpan value) : this(ToUInt32(value))
        {
        }

        internal Second(object value)
        {
            switch (value)
            {
                case byte @byte:
                    mValue = @byte;
                    break;

                case sbyte @sbyte:
                    mValue = ToUInt32(@sbyte);
                    break;

                case short @short:
                    mValue = ToUInt32(@short);
                    break;

                case ushort @ushort:
                    mValue = @ushort;
                    break;

                case int @int:
                    mValue = ToUInt32(@int);
                    break;

                case uint @uint:
                    mValue = @uint;
                    break;

                case long @long:
                    mValue = ToUInt32(@long);
                    break;

                case ulong @ulong:
                    mValue = ToUInt32(@ulong);
                    break;

                case TimeSpan timeSpan:
                    mValue = ToUInt32(timeSpan);
                    break;

                case Second seconds:
                    mValue = seconds.mValue;
                    break;

                default:
                    mValue = 0;
                    break;
            }
        }

        #endregion

        #region implicit operator base type

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator uint(Second value) => value.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(Second value) => (int)value.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator TimeSpan(Second value) => TimeSpan.FromSeconds(value.mValue);

        #endregion

        #region implicit operator this type

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Second(TimeSpan value) => new Second(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Second(uint value) => new Second(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Second(int value) => new Second(value);

        #endregion

        #region functions

        /// <summary>
        ///
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public Second Add(TimeSpan ts) => mValue + (uint)ts.TotalSeconds;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Second FromDays(double value) => TimeSpan.FromDays(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Second FromHours(double value) => TimeSpan.FromHours(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Second FromMinutes(double value) => TimeSpan.FromMinutes(value);

        #endregion

        #region static Equals functions

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Second left, Second right) => left.mValue == right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Second left, uint right) => left.mValue == right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(uint left, Second right) => left == right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Second left, int right) => left.mValue == right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(int left, Second right) => left == right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Second left, TimeSpan right) => left.mValue == (uint)right.TotalSeconds;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(TimeSpan left, Second right) => (uint)left.TotalSeconds == right.mValue;

        #endregion

        #region operator ==/!=/<>=

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Second left, Second right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Second left, Second right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Second left, int right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Second left, int right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(int left, Second right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(int left, Second right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Second left, uint right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Second left, uint right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(uint left, Second right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(uint left, Second right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Second left, TimeSpan right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Second left, TimeSpan right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(TimeSpan left, Second right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(TimeSpan left, Second right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Second left, Second right) => left.mValue > right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Second left, Second right) => left.mValue < right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Second left, Second right) => left.mValue >= right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(Second left, Second right) => left.mValue <= right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Second left, uint right) => left.mValue > right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Second left, uint right) => left.mValue < right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Second left, uint right) => left.mValue >= right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(Second left, uint right) => left.mValue <= right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(uint left, Second right) => left > right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(uint left, Second right) => left < right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(uint left, Second right) => left >= right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(uint left, Second right) => left <= right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Second left, int right) => left.mValue > right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Second left, int right) => left.mValue < right;

        /// <summary>
        /// Seconds
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(Second left, int right) => left.mValue >= right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(Second left, int right) => left.mValue <= right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(int left, Second right) => left > right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(int left, Second right) => left < right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >=(int left, Second right) => left >= right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <=(int left, Second right) => left <= right.mValue;

        #endregion

        #region System.IEquatable`1

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TimeSpan other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(uint other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(int other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Second other) => Equals(this, other);

        #endregion

        #region override System.Object

        /// <summary>
        /// 将此实例的数值转换为其等效的字符串表示形式。
        /// </summary>
        /// <returns>此实例的值的字符串表示形式，由一系列从 0 到 9 之间的数字组成，不包含符号或前导零。</returns>
        public override string ToString() => mValue.ToString();

        /// <summary>
        /// 返回此实例的哈希代码。
        /// </summary>
        /// <returns>当前实例的哈希代码。</returns>
        public override int GetHashCode()
        {
            return -952055727 + mValue.GetHashCode();
        }

        /// <summary>
        /// 指示此实例与指定对象是否相等。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null) return Equals(this, new Second(obj));
            return false;
        }

        #endregion

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
    public sealed class SecondConverter : Converter<Second?>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Second ConvertFrom(object value)
        {
            return new Second(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, Second? value, JsonSerializer serializer)
        {
            if (value.HasValue)
            {
                uint temp = value.Value;
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
        public override Second? ReadJson(JsonReader reader, Type objectType, object existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            if (value != null)
                return ConvertFrom(value);
            else
                return null;
        }
    }
}