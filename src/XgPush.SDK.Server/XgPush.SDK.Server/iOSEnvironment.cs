using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    /// <see cref="T:XgPush.SDK.Server.iOSEnvironment" /> <see href="http://docs.developer.qq.com/xg/push_faq/server_api/rest.html#push-api%E5%9F%BA%E7%A1%80%E5%8F%82%E6%95%B0"/>
    /// <para>参数名 environment</para>
    /// <para>类型 uint</para>
    /// <para>必需 是(仅iOS)</para>
    /// <para>默认值 1(<see cref="F:XgPush.SDK.Server.iOSEnvironment.Production" />)</para>
    /// <para>描述 此字段描述的是App的环境</para>
    /// <para>- 1，表示发布环境，对应App已经发布到AppStore</para>
    /// <para>- 2，表示开发环境，对应App仍处于调试环境</para>
    /// <para>- (对于iOS，消息推送有两种情况：开发环境、发布环境)</para>
    /// </summary>
    [JsonConverter(typeof(iOSEnvironmentConverter))]
    public struct iOSEnvironment : IEquatable<iOSEnvironment>,
        IEquatable<iOSEnvironmentV3>,
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
        public const uint ProductionUInt32 = 1;

        /// <summary>
        /// 开发环境。
        /// </summary>
        public const uint DevelopmentUInt32 = 2;

        /// <summary>
        /// 生产环境。
        /// </summary>
        public static readonly iOSEnvironment Production = ProductionUInt32;

        /// <summary>
        /// 开发环境
        /// </summary>
        public static readonly iOSEnvironment Development = ProductionUInt32;

        private static readonly Dictionary<uint, string> mDefineds = new Dictionary<uint, string>
        {
                { ProductionUInt32, nameof(Production) },
                { DevelopmentUInt32, nameof(Development) }
        };

        private readonly uint? mValue;

        internal uint Value => mValue ?? ProductionUInt32;

        internal iOSEnvironment(byte value) : this((uint)value)
        {
        }

        internal iOSEnvironment(sbyte value) : this((uint)value)
        {
        }

        internal iOSEnvironment(short value) : this((uint)value)
        {
        }

        internal iOSEnvironment(ushort value) : this((uint)value)
        {
        }

        internal iOSEnvironment(int value) : this((uint)value)
        {
        }

        internal iOSEnvironment(long value) : this((uint)value)
        {
        }

        internal iOSEnvironment(ulong value) : this((uint)value)
        {
        }

        internal static T _<T>(uint value, T development, T production)
        {
            if (value == DevelopmentUInt32)
            {
                return development;
            }
            else
            {
                return production;
            }
        }

        private static uint _(uint value) => _(value, DevelopmentUInt32, ProductionUInt32);

        internal static T _<T>(string value, T development, T production)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value.Equals(mDefineds[DevelopmentUInt32],
                    StringComparison.OrdinalIgnoreCase) ||
                    value == DevelopmentUInt32.ToString() ||
                    value.Equals(iOSEnvironmentV3.DevelopmentString,
                    StringComparison.OrdinalIgnoreCase))
                {
                    return development;
                }
            }
            return production;
        }

        private static uint _(string value) => _(value, DevelopmentUInt32, ProductionUInt32);

        internal iOSEnvironment(uint value) => mValue = _(value);

        internal iOSEnvironment(string value) => mValue = _(value);

        internal iOSEnvironment(object value)
        {
            if (value == null) mValue = ProductionUInt32;
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
                        mValue = ProductionUInt32;
                        break;
                }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator byte(iOSEnvironment value) => (byte)value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator sbyte(iOSEnvironment value) => (sbyte)value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator short(iOSEnvironment value) => (short)value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator ushort(iOSEnvironment value) => (ushort)value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator int(iOSEnvironment value) => (int)value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator uint(iOSEnvironment value) => value.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator string(iOSEnvironment value) => value.Value.ToString();

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(byte value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(sbyte value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(short value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(ushort value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(int value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(uint value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator iOSEnvironment(string value) => new iOSEnvironment(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, iOSEnvironment right) => left.Value == right.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, byte right)
        {
            return Equals(left, new iOSEnvironment(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(byte left, iOSEnvironment right)
        {
            return Equals(new iOSEnvironment(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, short right)
        {
            return Equals(left, new iOSEnvironment(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(short left, iOSEnvironment right)
        {
            return Equals(new iOSEnvironment(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, ushort right)
        {
            return Equals(left, new iOSEnvironment(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(ushort left, iOSEnvironment right)
        {
            return Equals(new iOSEnvironment(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, int right)
        {
            return Equals(left, new iOSEnvironment(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(int left, iOSEnvironment right)
        {
            return Equals(new iOSEnvironment(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(iOSEnvironment left, uint right)
        {
            return Equals(left, new iOSEnvironment(right));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Equals(uint left, iOSEnvironment right)
        {
            return Equals(new iOSEnvironment(left), right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(byte left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(byte left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(short left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(short left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(ushort left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(ushort left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(int left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(int left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(uint left, iOSEnvironment right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(uint left, iOSEnvironment right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, byte right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, byte right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, short right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, short right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, ushort right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, ushort right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, int right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, int right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(iOSEnvironment left, uint right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(iOSEnvironment left, uint right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(iOSEnvironmentV3 other)
        {
            return iOSEnvironmentV3.Equals(this, other);
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
            return Equals(this, new iOSEnvironment(other));
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
            return Equals(this, new iOSEnvironment(other));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ulong other)
        {
            return Equals(this, new iOSEnvironment(other));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(iOSEnvironment other)
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
            return Equals(this, new iOSEnvironment(obj));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return mDefineds[Value];
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
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
}

namespace XgPush.SDK.Server.BaseTypes
{
#pragma warning disable IDE1006 // 命名样式

    /// <summary>
    ///
    /// </summary>
    public sealed class iOSEnvironmentConverter : Converter<iOSEnvironment?>
#pragma warning restore IDE1006 // 命名样式
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint ConvertTo(iOSEnvironment value)
        {
            uint p0 = value;
            return p0;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static iOSEnvironment ConvertFrom(object value)
        {
            return new iOSEnvironment(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, iOSEnvironment? value, JsonSerializer serializer)
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
        public override iOSEnvironment? ReadJson(JsonReader reader, Type objectType,
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