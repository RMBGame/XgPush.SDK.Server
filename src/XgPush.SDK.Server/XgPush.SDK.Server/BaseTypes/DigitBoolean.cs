using Newtonsoft.Json;
using System;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.BaseTypes
{
    /// <summary>
    ///
    /// </summary>
    [JsonConverter(typeof(DigitBooleanConverter))]
    public struct DigitBoolean : IEquatable<DigitBoolean>,
        IEquatable<bool>,
        IEquatable<int>,
        IConvertible
    {
        #region field

        /// <summary>
        /// 将布尔值 true 表示为字符串。
        /// </summary>
        public const string TrueString = "1";

        /// <summary>
        /// 将布尔值 false 表示为字符串
        /// </summary>
        public const string FalseString = "0";

        private bool mValue;

        /// <summary>
        /// <see langword="true"/>
        /// </summary>
        public static readonly DigitBoolean True = new DigitBoolean(true);

        /// <summary>
        /// <see langword="false"/>
        /// </summary>
        public static readonly DigitBoolean False = new DigitBoolean(false);

        #endregion field

        #region ctor

        internal DigitBoolean(bool value) => mValue = value;

        internal DigitBoolean(byte value) => mValue = value.ToBoolean();

        internal DigitBoolean(sbyte value) => mValue = value.ToBoolean();

        internal DigitBoolean(short value) => mValue = value.ToBoolean();

        internal DigitBoolean(ushort value) => mValue = value.ToBoolean();

        internal DigitBoolean(int value) => mValue = value.ToBoolean();

        internal DigitBoolean(uint value) => mValue = value.ToBoolean();

        internal DigitBoolean(long value) => mValue = value.ToBoolean();

        internal DigitBoolean(ulong value) => mValue = value.ToBoolean();

        internal DigitBoolean(object value)
        {
            if (value is bool @bool)
            {
                mValue = @bool;
            }
            else if (value is byte @byte)
            {
                mValue = @byte.ToBoolean();
            }
            else if (value is sbyte @sbyte)
            {
                mValue = @sbyte.ToBoolean();
            }
            else if (value is ushort @ushort)
            {
                mValue = @ushort.ToBoolean();
            }
            else if (value is int @int)
            {
                mValue = @int.ToBoolean();
            }
            else if (value is uint @uint)
            {
                mValue = @uint.ToBoolean();
            }
            else if (value is long @long)
            {
                mValue = @long.ToBoolean();
            }
            else if (value is ulong @ulong)
            {
                mValue = @ulong.ToBoolean();
            }
            else
            {
                mValue = default;
            }
        }

        #endregion ctor

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public int ToInt32()
        {
            int p0 = this;
            return p0;
        }

        #region implicit operator base type

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator bool(DigitBoolean value) => value.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(DigitBoolean value) => value.mValue.ToInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator byte(DigitBoolean value) => value.mValue.ToByte();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator uint(DigitBoolean value) => value.mValue.ToUInt32();

        #endregion implicit operator base type

        #region implicit operator this type

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(bool value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(byte value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(sbyte value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(short value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(ushort value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(int value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(uint value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(long value) => new DigitBoolean(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator DigitBoolean(ulong value) => new DigitBoolean(value);

        #endregion implicit operator this type

        #region static Equals functions

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(DigitBoolean left, DigitBoolean right) => left.mValue == right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(DigitBoolean left, bool right) => left.mValue == right;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(bool left, DigitBoolean right) => left == right.mValue;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(DigitBoolean left, int right)
            => left.mValue && right == 1 || !left.mValue && right != 1;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(int left, DigitBoolean right) => Equals(right, left);

        #endregion static Equals functions

        #region operator ==/!=

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DigitBoolean left, DigitBoolean right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DigitBoolean left, DigitBoolean right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(bool left, DigitBoolean right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(bool left, DigitBoolean right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(int left, DigitBoolean right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(int left, DigitBoolean right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DigitBoolean left, bool right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DigitBoolean left, bool right) => !Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DigitBoolean left, int right) => Equals(left, right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DigitBoolean left, int right) => !Equals(left, right);

        #endregion operator ==/!=

        #region System.IEquatable`1

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
        public bool Equals(bool other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(DigitBoolean other) => Equals(this, other);

        #endregion System.IEquatable`1

        #region override System.Object

        /// <summary>
        /// 指示此实例与指定对象是否相等。
        /// </summary>
        /// <param name="obj">要比较的另一个对象。</param>
        /// <returns>如果 obj 和该实例具有相同的类型并表示相同的值，则为 true；否则为 false。</returns>
        public override bool Equals(object obj)
        {
            if (obj != null) return Equals(this, new DigitBoolean(obj));
            return false;
        }

        /// <summary>
        /// 将此实例的值转换为其等效字符串表示形式（“1”或“0”）。
        /// </summary>
        /// <returns>如果此实例的值为 true，则为 Boolean.TrueString；如果此实例的值为 false，则为 Boolean.FalseString。</returns>
        public override string ToString() => mValue ? TrueString : FalseString;

        /// <summary>
        /// 返回此实例的哈希代码。
        /// </summary>
        /// <returns>当前 Boolean 的哈希代码。</returns>
        public override int GetHashCode()
        {
            return -952055727 + mValue.GetHashCode();
        }

        #endregion override System.Object

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
    public sealed class DigitBooleanConverter : Converter<DigitBoolean?>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DigitBoolean ConvertFrom(object value)
        {
            return new DigitBoolean(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, DigitBoolean? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToInt32());
            else
                writer.WriteNull();
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
        public override DigitBoolean? ReadJson(JsonReader reader, Type objectType,
            object existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value;
            if (value != null)
                return ConvertFrom(value);
            else
                return null;
        }
    }
}