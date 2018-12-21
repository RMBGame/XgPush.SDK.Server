using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    /// <see cref="OR"/> || <see cref="AND"/>
    /// <para>默认值：<see cref="OR"/></para>
    /// </summary>
    [JsonConverter(typeof(OperatorConverter))]
    public struct Operator : IEquatable<Operator>,
        IEquatable<string>,
        IConvertible
    {
        /// <summary>
        /// ||
        /// </summary>
        public const string OR = nameof(OR);

        /// <summary>
        /// &amp;&amp;
        /// </summary>
        public const string AND = nameof(AND);

        private readonly string mValue;

        private string Value => mValue ?? OR;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(Operator value) => value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Operator(string value) => new Operator(value);

        private static string _(string s)
            => s?.Equals(AND, StringComparison.OrdinalIgnoreCase)
            ?? false ? AND : OR;

        internal Operator(string value) => mValue = _(value);

        internal Operator(object value)
        {
            if (value is string s) mValue = _(s);
            else mValue = OR;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Operator left, Operator right) => left.Value == right.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Operator left, Operator right) => !(left == right);

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(this, new Operator(obj));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Operator other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(string other) => Equals(this, other);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Operator left, Operator right) => left.Value == right.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(Operator left, string right) => left.Value.Equals(right, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(string left, Operator right) => right.Value.Equals(left, StringComparison.OrdinalIgnoreCase);

        #region System.IConvertible

        /// <summary>
        /// 返回此实例的 System.TypeCode。
        /// </summary>
        /// <returns>枚举常数，它是实现该接口的类或值类型的 System.TypeCode。</returns>
        public TypeCode GetTypeCode()
        {
#if NETSTANDARD1_3
            return ((IConvertible)Value).GetTypeCode();
#else
            return Value.GetTypeCode();
#endif
        }

        bool IConvertible.ToBoolean(IFormatProvider provider) => ((IConvertible)Value).ToBoolean(provider);

        byte IConvertible.ToByte(IFormatProvider provider) => ((IConvertible)Value).ToByte(provider);

        char IConvertible.ToChar(IFormatProvider provider) => ((IConvertible)Value).ToChar(provider);

        DateTime IConvertible.ToDateTime(IFormatProvider provider) => ((IConvertible)Value).ToDateTime(provider);

        decimal IConvertible.ToDecimal(IFormatProvider provider) => ((IConvertible)Value).ToDecimal(provider);

        double IConvertible.ToDouble(IFormatProvider provider) => ((IConvertible)Value).ToDouble(provider);

        short IConvertible.ToInt16(IFormatProvider provider) => ((IConvertible)Value).ToInt16(provider);

        int IConvertible.ToInt32(IFormatProvider provider) => ((IConvertible)Value).ToInt32(provider);

        long IConvertible.ToInt64(IFormatProvider provider) => ((IConvertible)Value).ToInt64(provider);

        sbyte IConvertible.ToSByte(IFormatProvider provider) => ((IConvertible)Value).ToSByte(provider);

        float IConvertible.ToSingle(IFormatProvider provider) => ((IConvertible)Value).ToSingle(provider);

        string IConvertible.ToString(IFormatProvider provider) => (Value as IConvertible).ToString(provider);

        object IConvertible.ToType(Type conversionType, IFormatProvider provider) => ((IConvertible)Value).ToType(conversionType, provider);

        ushort IConvertible.ToUInt16(IFormatProvider provider) => ((IConvertible)Value).ToUInt16(provider);

        uint IConvertible.ToUInt32(IFormatProvider provider) => ((IConvertible)Value).ToUInt32(provider);

        ulong IConvertible.ToUInt64(IFormatProvider provider) => ((IConvertible)Value).ToUInt64(provider);

        #endregion System.IConvertible
    }
}

namespace XgPush.SDK.Server.BaseTypes
{
#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    ///
    /// </summary>
    public sealed class OperatorConverter : Converter<Operator?>
#pragma warning restore IDE1006 // 命名样式
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Operator ConvertFrom(object value)
        {
            return new Operator(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, Operator? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToString());
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
        public override Operator? ReadJson(JsonReader reader, Type objectType,
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