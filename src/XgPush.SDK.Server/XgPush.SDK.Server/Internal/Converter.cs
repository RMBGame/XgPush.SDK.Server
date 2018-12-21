using Newtonsoft.Json;
using System;
using System.Globalization;

#if NETSTANDARD1_3
using System.Reflection;
#endif

namespace XgPush.SDK.Server.Internal
{
    /// <summary>Converts an object to and from JSON.</summary>
    /// <typeparam name="T">The object type to convert.</typeparam>
    public abstract class Converter<T> : JsonConverter
    {
        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override sealed void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if ((value != null ? (value is T ? 1 : 0) : (ReflectionUtils.IsNullable(typeof(T)) ? 1 : 0)) == 0)
                throw new JsonSerializationException(
                    "Converter cannot write specified value to JSON. {0} is required."
                    .FormatWith(CultureInfo.InvariantCulture, (object)typeof(T)));
            WriteJson(writer, (T)value, serializer);
        }

        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public abstract void WriteJson(JsonWriter writer, T value, JsonSerializer serializer);

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override sealed object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool flag = existingValue == null;
            if (!flag && !(existingValue is T))
                throw new JsonSerializationException(
                    "Converter cannot read JSON with the specified existing value. {0} is required."
                    .FormatWith(CultureInfo.InvariantCulture, (object)typeof(T)));
            return ReadJson(reader, objectType, existingValue, !flag, serializer);
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read. If there is no existing value then <c>null</c> will be used.</param>
        /// <param name="hasExistingValue">The existing value has a value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public abstract T ReadJson(JsonReader reader, Type objectType, object existingValue, bool hasExistingValue, JsonSerializer serializer);

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override sealed bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
    }
}

internal static class ReflectionUtils
{
    internal static bool IsNullable(Type t)
    {
        ValidationUtils.ArgumentNotNull(t, nameof(t));
        if (t.IsValueType())
            return IsNullableType(t);
        return true;
    }

    internal static bool IsNullableType(Type t)
    {
        ValidationUtils.ArgumentNotNull(t, nameof(t));
        if (t.IsGenericType())
            return t.GetGenericTypeDefinition() == typeof(Nullable<>);
        return false;
    }
}

internal static class TypeExtensions
{
    internal static bool IsGenericType(this Type type)
    {
#if NETSTANDARD1_3
        return type.GetTypeInfo().IsGenericType;
#else
        return type.IsGenericType;
#endif
    }

    internal static bool IsValueType(this Type type)
    {
#if NETSTANDARD1_3
        return type.GetTypeInfo().IsValueType;
#else
        return type.IsValueType;
#endif
    }

#if NETSTANDARD1_3
    internal static bool IsAssignableFrom(this Type type, Type c)
    {
        return type.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
    }
#endif
}

internal static class ValidationUtils
{
    internal static void ArgumentNotNull(object value, string parameterName)
    {
        if (value == null)
            throw new ArgumentNullException(parameterName);
    }
}

internal static class StringUtils
{
    internal static string FormatWith(this string format,
        IFormatProvider provider, params object[] args)
    {
        ValidationUtils.ArgumentNotNull(format, nameof(format));
        return string.Format(provider, format, args);
    }
}