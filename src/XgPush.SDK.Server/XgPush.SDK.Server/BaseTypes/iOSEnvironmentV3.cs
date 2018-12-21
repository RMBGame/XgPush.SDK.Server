using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server.BaseTypes
{
#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    /// 用户指定推送环境，仅限iOS平台推送使用。
    /// <para> 默认值：<see cref="Production"/></para>
    /// <para><see cref="Production"/>(product)： 推送生产环境</para>
    /// <para><see cref="Development"/>(dev)： 推送开发环境</para>
    /// </summary>
    [JsonConverter(typeof(iOSEnvironmentV3Converter))]
    public struct iOSEnvironmentV3 : IEquatable<iOSEnvironmentV3>,
        IEquatable<iOSEnvironment>,
        IEquatable<sbyte>,
        IEquatable<byte>,
        IEquatable<short>,
        IEquatable<ushort>,
        IEquatable<int>,
        IEquatable<uint>,
        IEquatable<long>,
        IEquatable<ulong>,
        IConvertible
#pragma warning restore IDE1006 // 命名样式
    {
        /// <summary>
        /// 生产环境。
        /// </summary>
        public const string ProductionString = "product";

        /// <summary>
        /// 开发环境。
        /// </summary>
        public const string DevelopmentString = "dev";

        /// <summary>
        /// 生产环境。
        /// </summary>
        public static readonly iOSEnvironmentV3 Production = ProductionString;

        /// <summary>
        /// 开发环境。
        /// </summary>
        public static readonly iOSEnvironmentV3 Development = DevelopmentString;

        private readonly string mValue;

        internal string Value => mValue ?? DevelopmentString;

        internal iOSEnvironmentV3(byte value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(sbyte value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(short value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(ushort value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(int value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(long value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(ulong value) : this((uint)value)
        {
        }

        internal iOSEnvironmentV3(iOSEnvironment value) : this(value.Value)
        {
        }

        private static string _(uint value)
            => iOSEnvironment._(value, DevelopmentString, ProductionString);

        private static string _(string value)
            => iOSEnvironment._(value, DevelopmentString, ProductionString);

        internal iOSEnvironmentV3(uint value) => mValue = _(value);

        internal iOSEnvironmentV3(string value) => mValue = _(value);

        internal iOSEnvironmentV3(object value)
        {
            if (value == null) mValue = ProductionString;
            else
                switch (value)
                {
                    case string s:
                        mValue = _(s);
                        break;

                    case uint @uint:
                        mValue = _(@uint);
                        break;

                    case long @long:
                        mValue = _((uint)@long);
                        break;

                    case ulong @ulong:
                        mValue = _((uint)@ulong);
                        break;

                    case int @int:
                        mValue = _((uint)@int);
                        break;

                    case short @short:
                        mValue = _((uint)@short);
                        break;

                    case ushort @ushort:
                        mValue = _(@ushort);
                        break;

                    case byte @byte:
                        mValue = _(@byte);
                        break;

                    case sbyte @sbyte:
                        mValue = _((uint)@sbyte);
                        break;

                    default:
                        mValue = ProductionString;
                        break;
                }
        }

        private uint ToUInt32()
        {
            switch (Value)
            {
                case DevelopmentString:
                    return iOSEnvironment.DevelopmentUInt32;

                default:
                    return iOSEnvironment.ProductionUInt32;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(iOSEnvironmentV3 value) => value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator byte(iOSEnvironmentV3 value) => (byte)value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator sbyte(iOSEnvironmentV3 value) => (sbyte)value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator short(iOSEnvironmentV3 value) => (short)value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ushort(iOSEnvironmentV3 value) => (ushort)value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(iOSEnvironmentV3 value) => (int)value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator uint(iOSEnvironmentV3 value) => value.ToUInt32();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(iOSEnvironmentV3 value) => value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(iOSEnvironment value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(byte value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(sbyte value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(short value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(ushort value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(int value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(uint value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironmentV3(string value) => new iOSEnvironmentV3(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, iOSEnvironmentV3 right) => left.Value == right.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, iOSEnvironment right)
        {
            return Equals(left, new iOSEnvironmentV3(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, iOSEnvironmentV3 right)
        {
            return Equals(new iOSEnvironmentV3(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, byte right)
        {
            return Equals(left, new iOSEnvironmentV3(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(byte left, iOSEnvironmentV3 right)
        {
            return Equals(new iOSEnvironmentV3(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, short right)
        {
            return Equals(left, new iOSEnvironmentV3(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(short left, iOSEnvironmentV3 right)
        {
            return Equals(new iOSEnvironmentV3(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, ushort right)
        {
            return Equals(left, new iOSEnvironmentV3(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(ushort left, iOSEnvironmentV3 right)
        {
            return Equals(new iOSEnvironmentV3(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, int right)
        {
            return Equals(left, new iOSEnvironmentV3(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(int left, iOSEnvironmentV3 right)
        {
            return Equals(new iOSEnvironmentV3(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3 left, uint right)
        {
            return Equals(left, new iOSEnvironmentV3(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(uint left, iOSEnvironmentV3 right)
        {
            return Equals(new iOSEnvironmentV3(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironmentV3? left, iOSEnvironment? right)
        {
            if (!left.HasValue && !right.HasValue) return true;
            if (left.HasValue && !right.HasValue) return false;
            return Equals(left.Value, right.Value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment? left, iOSEnvironmentV3? right)
        {
            return Equals(right, left);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3? left, iOSEnvironment? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3? left, iOSEnvironment? right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment? left, iOSEnvironmentV3? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment? left, iOSEnvironmentV3? right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(byte left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(byte left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(short left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(short left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ushort left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ushort left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(int left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(int left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(uint left, iOSEnvironmentV3 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(uint left, iOSEnvironmentV3 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, byte right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, byte right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, short right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, short right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, ushort right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, ushort right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, int right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, int right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironmentV3 left, uint right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironmentV3 left, uint right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(iOSEnvironment other)
        {
            return Equals(this, new iOSEnvironmentV3(other));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(byte other)
        {
            return Equals(this, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(sbyte other)
        {
            return Equals(this, new iOSEnvironmentV3(other));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(short other)
        {
            return Equals(this, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ushort other)
        {
            return Equals(this, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(int other)
        {
            return Equals(this, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(uint other)
        {
            return Equals(this, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(long other)
        {
            return Equals(this, new iOSEnvironmentV3(other));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ulong other)
        {
            return Equals(this, new iOSEnvironmentV3(other));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(iOSEnvironmentV3 other)
        {
            return Equals(this, other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return Equals(this, new iOSEnvironmentV3(obj));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Value;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
        }

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

#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    ///
    /// </summary>
    public sealed class iOSEnvironmentV3Converter : Converter<iOSEnvironmentV3?>
#pragma warning restore IDE1006 // 命名样式
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertTo(iOSEnvironmentV3 value) => value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static iOSEnvironmentV3 ConvertFrom(object value)
        {
            return new iOSEnvironmentV3(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, iOSEnvironmentV3? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(ConvertTo(value.Value));
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
        public override iOSEnvironmentV3? ReadJson(JsonReader reader, Type objectType,
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